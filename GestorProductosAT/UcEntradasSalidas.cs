using System.Data;
using System.Globalization;

namespace GestorProductosAT
{
    public partial class UcEntradasSalidas : UserControl
    {
        // Constructor del control de entradas y salidas
        public UcEntradasSalidas()
        {
            InitializeComponent();

            // Ejecuta la configuración inicial cuando se carga el control
            Load += UcEntradasSalidas_Load;

            // Actualiza los movimientos cuando cambia el texto de búsqueda
            txtBuscar.TextChanged += (s, e) => CargarMovimientos();

            // Ejecuta el método para guardar un movimiento
            btnGuardar.Click += (s, e) => Guardar();

            // Limpia los campos del formulario
            btnLimpiar.Click += (s, e) => LimpiarFormulario();

            // Actualiza el stock cuando se selecciona otro producto
            cmbProducto.SelectedIndexChanged += (s, e) => ActualizarStockActual();

            // Habilita el proveedor únicamente cuando el movimiento es una entrada
            cmbTipo.SelectedIndexChanged += (s, e) => cmbProveedor.Enabled = cmbTipo.Text == "Entrada";
        }

        // Configura el control cuando termina de cargarse
        private void UcEntradasSalidas_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar estas instrucciones mientras se utiliza el diseñador
            if (DesignMode) return;

            // Aplica el diseño personalizado
            Estilo.Aplicar(this);

            // Configura las propiedades de la tabla de movimientos
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Agrega las opciones para seleccionar el tipo de movimiento
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Entrada");
            cmbTipo.Items.Add("Salida");
            cmbTipo.SelectedIndex = 0;

            // Agrega las opciones para filtrar el historial de movimientos
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.Add("Todos");
            cmbFiltro.Items.Add("Entradas");
            cmbFiltro.Items.Add("Salidas");
            cmbFiltro.SelectedIndex = 0;

            // Carga los productos disponibles en el selector
            cmbProducto.DataSource = BaseDatos.Consultar("SELECT codigo_barras, nombre FROM Producto ORDER BY nombre");

            // Define qué columna se muestra y cuál se utiliza como valor
            cmbProducto.DisplayMember = "nombre";
            cmbProducto.ValueMember = "codigo_barras";

            // Carga los proveedores disponibles en el selector
            cmbProveedor.DataSource = BaseDatos.Consultar("SELECT id_proveedor, nombre FROM Proveedor ORDER BY nombre");

            // Configura el nombre visible y el identificador de cada proveedor
            cmbProveedor.DisplayMember = "nombre";
            cmbProveedor.ValueMember = "id_proveedor";

            // Actualiza el historial cuando cambia el filtro seleccionado
            cmbFiltro.SelectedIndexChanged += (s, ev) => CargarMovimientos();

            // Comprueba si el usuario tiene permisos para registrar movimientos
            bool puedeRegistrar = Sesion.Rol == "Administrador";

            // Habilita o deshabilita el formulario según el rol del usuario
            pnlFormulario.Enabled = puedeRegistrar;

            // Cambia el título si el usuario solamente tiene permisos de consulta
            if (!puedeRegistrar) lblTitulo.Text = "Solo consulta";

            // Carga el historial y muestra las existencias del producto seleccionado
            CargarMovimientos();
            ActualizarStockActual();
        }

        // Consulta los movimientos registrados y los muestra en la tabla
        private void CargarMovimientos()
        {
            // Determina el tipo de movimiento según el filtro seleccionado
            string tipo = cmbFiltro.Text switch
            {
                "Entradas" => "ENTRADA",
                "Salidas" => "SALIDA",
                _ => ""
            };

            // Consulta los movimientos junto con la información de productos y proveedores
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

            // Limpia el origen de datos y las columnas anteriores de la tabla
            dgvMovimientos.DataSource = null;
            dgvMovimientos.Columns.Clear(); // Evita que se queden columnas de la consulta anterior

            // Asigna los resultados de la consulta a la tabla visual
            dgvMovimientos.DataSource = tabla;

            // Cambia los encabezados de las columnas para mostrarlos de forma más clara
            dgvMovimientos.Columns["fecha"]!.HeaderText = "Fecha";
            dgvMovimientos.Columns["producto"]!.HeaderText = "Producto";
            dgvMovimientos.Columns["tipo"]!.HeaderText = "Tipo";
            dgvMovimientos.Columns["cantidad"]!.HeaderText = "Cantidad";
            dgvMovimientos.Columns["inventario"]!.HeaderText = "Stock resultante";
            dgvMovimientos.Columns["proveedor"]!.HeaderText = "Proveedor";
            dgvMovimientos.Columns["origen"]!.HeaderText = "Origen";

            // Recorre las filas para asignar un color según el tipo de movimiento
            foreach (DataGridViewRow fila in dgvMovimientos.Rows)
            {
                fila.DefaultCellStyle.BackColor =
                    fila.Cells["tipo"].Value?.ToString() == "ENTRADA" ? Color.Honeydew : Color.MistyRose;
            }
        }

        // Consulta y muestra las existencias actuales del producto seleccionado
        private void ActualizarStockActual()
        {
            // Comprueba que exista un producto seleccionado y obtiene su código de barras
            if (cmbProducto.SelectedValue is not string codigo)
            {
                lblStockActual.Text = "Stock actual: —";
                return;
            }

            // Consulta el stock del producto en la base de datos
            var tabla = BaseDatos.Consultar(
                "SELECT stock FROM Producto WHERE codigo_barras = @c", ("@c", codigo));

            // Muestra las existencias encontradas o un guion si no hay resultados
            lblStockActual.Text = tabla.Rows.Count == 0
                ? "Stock actual: —"
                : "Stock actual: " + tabla.Rows[0]["stock"];
        }

        // Restablece los valores del formulario a sus opciones iniciales
        private void LimpiarFormulario()
        {
            // Selecciona el tipo de movimiento Entrada
            cmbTipo.SelectedIndex = 0;

            // Selecciona el primer producto disponible
            if (cmbProducto.Items.Count > 0) cmbProducto.SelectedIndex = 0;

            // Selecciona el primer proveedor disponible
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;

            // Establece la cantidad inicial en una unidad
            nudCantidad.Value = 1;

            // Actualiza el stock mostrado en pantalla
            ActualizarStockActual();
        }

        // Registra una entrada o salida de productos en el inventario
        private void Guardar()
        {
            // Comprueba que se haya seleccionado un producto válido
            if (cmbProducto.SelectedValue is not string codigo)
            {
                MessageBox.Show("Selecciona un producto.");
                return;
            }

            // Determina si el movimiento corresponde a una entrada
            bool esEntrada = cmbTipo.Text == "Entrada";

            // Obtiene la cantidad seleccionada en el control numérico
            int cantidad = (int)nudCantidad.Value;

            // Comprueba que se haya seleccionado un proveedor para la entrada
            if (esEntrada && cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Selecciona el proveedor de la entrada.");
                return;
            }

            // Prepara el mensaje de confirmación según el tipo de movimiento
            string accion = esEntrada ? "entrada" : "salida";

            // Solicita confirmación antes de registrar el movimiento
            if (MessageBox.Show($"¿Registrar {accion} de {cantidad} x {cmbProducto.Text}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            // Obtiene la fecha y hora actuales con un formato específico
            string ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            // Convierte el tipo de movimiento al formato utilizado en la base de datos
            string tipo = esEntrada ? "ENTRADA" : "SALIDA";

            // Obtiene el proveedor seleccionado o guarda un valor nulo si es una salida
            object proveedor = esEntrada ? cmbProveedor.SelectedValue! : DBNull.Value;

            try
            {
                // Ejecuta todas las operaciones dentro de una transacción
                BaseDatos.EnTransaccion(tx =>
                {
                    if (esEntrada)
                    {
                        // Aumenta el stock del producto según la cantidad recibida
                        tx.Ejecutar(
                            "UPDATE Producto SET stock = stock + @q WHERE codigo_barras = @c",
                            ("@q", cantidad), ("@c", codigo));
                    }
                    else
                    {
                        // Reduce el stock solamente si existen suficientes unidades disponibles
                        int afectadas = tx.Ejecutar(
                            "UPDATE Producto SET stock = stock - @q WHERE codigo_barras = @c AND stock >= @q",
                            ("@q", cantidad), ("@c", codigo));

                        // Si no se modificó ningún registro, significa que no hay stock suficiente
                        if (afectadas == 0)
                            throw new Exception("No hay stock suficiente para esa salida.");
                    }

                    // Consulta las existencias después de actualizar el inventario
                    int stockNuevo = Convert.ToInt32(tx.Valor(
                        "SELECT stock FROM Producto WHERE codigo_barras = @c", ("@c", codigo)));

                    // Registra el movimiento con sus datos y el stock resultante
                    tx.Ejecutar(
                        @"INSERT INTO Movimiento (id_venta, id_proveedor, codigo_barras, tipo_movimiento, cantidad, fecha, inventario)
                          VALUES (NULL, @p, @c, @t, @q, @f, @i)",
                        ("@p", proveedor), ("@c", codigo), ("@t", tipo),
                        ("@q", cantidad), ("@f", ahora), ("@i", stockNuevo));
                });
            }
            catch (Exception ex)
            {
                // Muestra el error si ocurre algún problema durante el registro
                // La transacción evita que se conserve solamente una parte de los cambios
                MessageBox.Show("No se pudo registrar el movimiento. No se guardó ningún cambio.\n" + ex.Message);
                return;
            }

            // Actualiza el historial y limpia el formulario después de guardar correctamente
            CargarMovimientos();
            LimpiarFormulario();
        }
    }
}