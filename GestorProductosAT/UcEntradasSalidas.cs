using System.Data;
using System.Globalization;

namespace GestorProductosAT
{
    public partial class UcEntradasSalidas : UserControl
    {
        public UcEntradasSalidas()
        {
            InitializeComponent();
            Load += UcEntradasSalidas_Load;

            txtBuscar.TextChanged += (s, e) => CargarMovimientos();
            btnGuardar.Click += (s, e) => Guardar();
            btnLimpiar.Click += (s, e) => LimpiarFormulario();

            cmbProducto.SelectedIndexChanged += (s, e) => ActualizarStockActual();

            cmbTipo.SelectedIndexChanged += (s, e) => cmbProveedor.Enabled = cmbTipo.Text == "Entrada";
        }

        private void UcEntradasSalidas_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;
            Estilo.Aplicar(this);

            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Entrada");
            cmbTipo.Items.Add("Salida");
            cmbTipo.SelectedIndex = 0;

            cmbFiltro.Items.Clear();
            cmbFiltro.Items.Add("Todos");
            cmbFiltro.Items.Add("Entradas");
            cmbFiltro.Items.Add("Salidas");
            cmbFiltro.SelectedIndex = 0;

            cmbProducto.DataSource = BaseDatos.Consultar("SELECT codigo_barras, nombre FROM Producto ORDER BY nombre");
            cmbProducto.DisplayMember = "nombre";
            cmbProducto.ValueMember = "codigo_barras";

            cmbProveedor.DataSource = BaseDatos.Consultar("SELECT id_proveedor, nombre FROM Proveedor ORDER BY nombre");
            cmbProveedor.DisplayMember = "nombre";
            cmbProveedor.ValueMember = "id_proveedor";

            cmbFiltro.SelectedIndexChanged += (s, ev) => CargarMovimientos();

            bool puedeRegistrar = Sesion.Rol == "Administrador";
            pnlFormulario.Enabled = puedeRegistrar;
            if (!puedeRegistrar) lblTitulo.Text = "Solo consulta";

            CargarMovimientos();
            ActualizarStockActual();
        }

        private void CargarMovimientos()
        {
            string tipo = cmbFiltro.Text switch
            {
                "Entradas" => "ENTRADA",
                "Salidas" => "SALIDA",
                _ => ""
            };

            var tabla = BaseDatos.Consultar(@"
                SELECT m.fecha, p.nombre AS producto, m.tipo_movimiento AS tipo, m.cantidad,
                       m.inventario, pr.nombre AS proveedor,
                       CASE WHEN m.id_venta IS NULL THEN 'Manual'
                            ELSE 'Venta #' || m.id_venta END AS origen
                FROM Movimiento m
                LEFT JOIN Producto p ON p.codigo_barras = m.codigo_barras
                LEFT JOIN Proveedor pr ON pr.id_proveedor = m.id_proveedor
                WHERE (p.nombre LIKE @f OR m.codigo_barras LIKE @f)
                  AND (@t = '' OR m.tipo_movimiento = @t)
                ORDER BY m.id_movimiento DESC",
                ("@f", "%" + txtBuscar.Text.Trim() + "%"),
                ("@t", tipo));

            dgvMovimientos.DataSource = null;
            dgvMovimientos.Columns.Clear(); // evita que se queden columnas de la consulta anterior
            dgvMovimientos.DataSource = tabla;

            dgvMovimientos.Columns["fecha"]!.HeaderText = "Fecha";
            dgvMovimientos.Columns["producto"]!.HeaderText = "Producto";
            dgvMovimientos.Columns["tipo"]!.HeaderText = "Tipo";
            dgvMovimientos.Columns["cantidad"]!.HeaderText = "Cantidad";
            dgvMovimientos.Columns["inventario"]!.HeaderText = "Stock resultante";
            dgvMovimientos.Columns["proveedor"]!.HeaderText = "Proveedor";
            dgvMovimientos.Columns["origen"]!.HeaderText = "Origen";

            foreach (DataGridViewRow fila in dgvMovimientos.Rows)
            {
                fila.DefaultCellStyle.BackColor =
                    fila.Cells["tipo"].Value?.ToString() == "ENTRADA" ? Color.Honeydew : Color.MistyRose;
            }
        }

        private void ActualizarStockActual()
        {
            if (cmbProducto.SelectedValue is not string codigo)
            {
                lblStockActual.Text = "Stock actual: —";
                return;
            }

            var tabla = BaseDatos.Consultar(
                "SELECT stock FROM Producto WHERE codigo_barras = @c", ("@c", codigo));

            lblStockActual.Text = tabla.Rows.Count == 0
                ? "Stock actual: —"
                : "Stock actual: " + tabla.Rows[0]["stock"];
        }

        private void LimpiarFormulario()
        {
            cmbTipo.SelectedIndex = 0;
            if (cmbProducto.Items.Count > 0) cmbProducto.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
            nudCantidad.Value = 1;
            ActualizarStockActual();
        }

        private void Guardar()
        {
            if (cmbProducto.SelectedValue is not string codigo)
            {
                MessageBox.Show("Selecciona un producto.");
                return;
            }

            bool esEntrada = cmbTipo.Text == "Entrada";
            int cantidad = (int)nudCantidad.Value;

            if (esEntrada && cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Selecciona el proveedor de la entrada.");
                return;
            }

            string accion = esEntrada ? "entrada" : "salida";
            if (MessageBox.Show($"¿Registrar {accion} de {cantidad} x {cmbProducto.Text}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string tipo = esEntrada ? "ENTRADA" : "SALIDA";
            object proveedor = esEntrada ? cmbProveedor.SelectedValue! : DBNull.Value;

            try
            {
                BaseDatos.EnTransaccion(tx =>
                {
                    if (esEntrada)
                    {
                        tx.Ejecutar(
                            "UPDATE Producto SET stock = stock + @q WHERE codigo_barras = @c",
                            ("@q", cantidad), ("@c", codigo));
                    }
                    else
                    {
                        int afectadas = tx.Ejecutar(
                            "UPDATE Producto SET stock = stock - @q WHERE codigo_barras = @c AND stock >= @q",
                            ("@q", cantidad), ("@c", codigo));
                        if (afectadas == 0)
                            throw new Exception("No hay stock suficiente para esa salida.");
                    }

                    int stockNuevo = Convert.ToInt32(tx.Valor(
                        "SELECT stock FROM Producto WHERE codigo_barras = @c", ("@c", codigo)));

                    tx.Ejecutar(
                        @"INSERT INTO Movimiento (id_venta, id_proveedor, codigo_barras, tipo_movimiento, cantidad, fecha, inventario)
                          VALUES (NULL, @p, @c, @t, @q, @f, @i)",
                        ("@p", proveedor), ("@c", codigo), ("@t", tipo),
                        ("@q", cantidad), ("@f", ahora), ("@i", stockNuevo));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar el movimiento. No se guardó ningún cambio.\n" + ex.Message);
                return;
            }

            CargarMovimientos();
            LimpiarFormulario();
        }
    }
}