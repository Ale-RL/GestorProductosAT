using System.Data;

namespace GestorProductosAT
{
    public partial class UcProductos : UserControl
    {
        private bool esNuevo = true;
        private const int StockBajo = 10; //por debajo de esto se marcara en rosa

        public UcProductos()
        {
            InitializeComponent();
            Load += UcProductos_Load;
            txtBuscar.TextChanged += (s, e) => CargarProductos();
            dgvProductos.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0) LlenarFormulario(dgvProductos.Rows[e.RowIndex]);
            };
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnGuardar.Click += (s, e) => Guardar();
            btnEliminar.Click += (s, e) => Eliminar();
        }

        private void UcProductos_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            //dgv es Data Grid View que funciona para conectar la base de dato:
            //sirve para mostrar o editar datos (osea administra info)
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cmbCategoria.DataSource = BaseDatos.Consultar("SELECT id_categoria, nombre FROM Categoria ORDER BY nombre");
            cmbCategoria.DisplayMember = "nombre";
            cmbCategoria.ValueMember = "id_categoria";

            cmbProveedor.DataSource = BaseDatos.Consultar("SELECT id_proveedor, nombre FROM Proveedor ORDER BY nombre");
            cmbProveedor.DisplayMember = "nombre";
            cmbProveedor.ValueMember = "id_proveedor";

            CargarProductos();
            LimpiarFormulario();
        }

        private void CargarProductos()
        {
            var tabla = BaseDatos.Consultar(@"SELECT p.codigo_barras, p.nombre, p.precio_compra, p.precio_venta, p.stock,
            c.nombre AS categoria, pr.nombre AS proveedor, p.id_categoria, p.id_proveedor
            FROM Producto p
            LEFT JOIN Categoria c ON c.id_categoria = p.id_categoria
            LEFT JOIN Proveedor pr ON pr.id_proveedor = p.id_proveedor
            WHERE p.nombre LIKE @f OR p.codigo_barras LIKE @f 
            ORDER BY p.nombre",
            ("@f", "%" + txtBuscar.Text.Trim() + "%"));
            //Me gustario no tener que explicar esto
            //En general esto busca productos por nombre o codigos de barra, por ejemplo si escribes 
            //"leche" puedes encontrar: leche deslactosada, leche entera, leche en polvo, etc.

            //Los "@" indica que estoy usando una cadena de texto literal, deja escribir una cadena de
            //varias lineas sin tener que concatenar ni usar \n para saltos de linea.
            //El p, c, pr significan producto, categoria y proveedor, c.nombre significa 
            //"la columna nombre de la tabla categoria", lo demas son funciones de bases de datos.

            dgvProductos.DataSource = tabla;

            // Columnas que se usan internamente pero no son visibles
            dgvProductos.Columns["id_categoria"]!.Visible = false;
            dgvProductos.Columns["id_proveedor"]!.Visible = false;

            dgvProductos.Columns["codigo_barras"]!.HeaderText = "Código";
            dgvProductos.Columns["nombre"]!.HeaderText = "Nombre";
            dgvProductos.Columns["precio_compra"]!.HeaderText = "P. compra";
            dgvProductos.Columns["precio_venta"]!.HeaderText = "P. venta";
            dgvProductos.Columns["stock"]!.HeaderText = "Stock";
            dgvProductos.Columns["categoria"]!.HeaderText = "Categoría";
            dgvProductos.Columns["proveedor"]!.HeaderText = "Proveedor";

            // Ahora el aviso de que algo se acaba en fila color rosa
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                if (int.TryParse(fila.Cells["stock"].Value?.ToString(), out int s) && s <= StockBajo)
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
            }
        }

        private void LlenarFormulario(DataGridViewRow fila)
        {
            esNuevo = false;
            txtCodigo.ReadOnly = true; // la llave primaria no se edita
            txtCodigo.Text = fila.Cells["codigo_barras"].Value?.ToString();
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtPrecioCompra.Text = fila.Cells["precio_compra"].Value.ToString();
            txtPrecioVenta.Text = fila.Cells["precio_venta"].Value.ToString();
            txtStock.Text = fila.Cells["stock"].Value?.ToString();
            cmbCategoria.SelectedValue = Convert.ToInt64(fila.Cells["id_categoria"].Value);
            cmbProveedor.SelectedValue = Convert.ToInt64(fila.Cells["id_proveedor"].Value);
        }

        private void LimpiarFormulario()
        {
            esNuevo = true;
            txtCodigo.ReadOnly = false;
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
            dgvProductos.ClearSelection();
            txtCodigo.Focus();
        }

        private void Guardar()
        {
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();

            if (codigo == "" || nombre == "")
            {
                MessageBox.Show("El código y el nombre son obligatorios.");
                return;
            }

            if(!double.TryParse(txtPrecioCompra.Text, out double precioCompra) ||
                !double.TryParse(txtPrecioVenta.Text, out double precioVenta) ||
                !int.TryParse(txtStock.Text, out int stock) ||
                precioCompra < 0 || precioVenta < 0 || stock < 0)
            {
                MessageBox.Show("Revisa que los precios y el stock sean números válidos");
                return;
            }

            try
            {
                if (esNuevo)
                {
                    BaseDatos.Ejecutar(
                        @"INSERT INTO Producto (codigo_barras, nombre, precio_compra, precio_venta, stock, id_categoria, id_proveedor)
                        VALUES (@c, @n, @pc, @pv, @s, @cat, @prov)",
                        ("@c", codigo), ("@n", nombre), ("@pc", precioCompra), ("@pv", precioVenta),
                        ("@s", stock), ("@cat", cmbCategoria.SelectedValue!), ("@prov", cmbProveedor.SelectedValue!)
                            );
                }
                else
                {
                    BaseDatos.Ejecutar(
                        @"UPDATE Producto
                            SET nombre = @n, precio_compra = @pc, precio_venta = @pv, stock = @s,
                                id_categoria = @cat, id_proveedor = @prov
                        WHERE codigo_barras = @c",
                        ("@c", codigo), ("@n", nombre), ("@pc", precioCompra), ("@pv", precioVenta),
                        ("@s", stock), ("@cat", cmbCategoria.SelectedValue!), ("@prov", cmbProveedor.SelectedValue!)
                        );
                }

                CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar (¿ya existe ese código?): \n" + ex.Message  );
            }
        }

        private void Eliminar()
        {
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona un producto de la lista.");
                return;
            }

            if (MessageBox.Show($"¿Eliminar \"{txtNombre.Text}\"?", "Confirmar",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                BaseDatos.Ejecutar("DELETE FROM Producto WHERE codigo_barras = @c", ("@c", txtCodigo.Text));
                CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar (probablemente ya tiene ventas registradas):\n" + ex.Message); 
            }
        }

        private void pnlFormulario_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
