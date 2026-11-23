
using System.Data;

namespace GestorProductosAT
{
    public partial class UcProductos : UserControl
    {
        // Indica si se está registrando un producto nuevo o editando uno existente
        private bool esNuevo = true;

        public UcProductos()
        {
            InitializeComponent();

            // Conecta los eventos de los controles con sus respectivos métodos
            Load += UcProductos_Load;
            txtBuscar.TextChanged += (s, e) => CargarProductos();

            // Al seleccionar una fila, muestra sus datos en el formulario
            dgvProductos.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0) LlenarFormulario(dgvProductos.Rows[e.RowIndex]);
            };

            // Configura las acciones de los botones
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnGuardar.Click += (s, e) => Guardar();
            btnEliminar.Click += (s, e) => Eliminar();
        }

        private void UcProductos_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar este código mientras se diseña la interfaz
            if (DesignMode) return;

            Estilo.Aplicar(this);

            // Configura la tabla para mostrar los productos
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Carga las categorías disponibles en el ComboBox
            cmbCategoria.DataSource = BaseDatos.Consultar("SELECT id_categoria, nombre FROM Categoria ORDER BY nombre");
            cmbCategoria.DisplayMember = "nombre";
            cmbCategoria.ValueMember = "id_categoria";

            // Carga los proveedores disponibles en el ComboBox
            cmbProveedor.DataSource = BaseDatos.Consultar("SELECT id_proveedor, nombre FROM Proveedor ORDER BY nombre");
            cmbProveedor.DisplayMember = "nombre";
            cmbProveedor.ValueMember = "id_proveedor";

            // Muestra los productos y deja el formulario listo para un registro nuevo
            CargarProductos();
            LimpiarFormulario();
        }

        private void CargarProductos()
        {
            // Consulta los productos y obtiene también sus categorías y proveedores
            // El filtro permite buscar por nombre o código de barras
            var tabla = BaseDatos.Consultar(@"SELECT p.codigo_barras, p.nombre, p.precio_compra, p.precio_venta, p.stock,
            c.nombre AS categoria, pr.nombre AS proveedor, p.id_categoria, p.id_proveedor
            FROM Producto p
            LEFT JOIN Categoria c ON c.id_categoria = p.id_categoria
            LEFT JOIN Proveedor pr ON pr.id_proveedor = p.id_proveedor
            WHERE p.nombre LIKE @f OR p.codigo_barras LIKE @f 
            ORDER BY p.nombre",
            ("@f", "%" + txtBuscar.Text.Trim() + "%"));

            // Coloca los resultados de la consulta en la tabla
            dgvProductos.DataSource = tabla;

            // Oculta los identificadores que se utilizan internamente
            dgvProductos.Columns["id_categoria"]!.Visible = false;
            dgvProductos.Columns["id_proveedor"]!.Visible = false;

            // Cambia los nombres de las columnas para mostrarlos de forma más clara
            dgvProductos.Columns["codigo_barras"]!.HeaderText = "Código";
            dgvProductos.Columns["nombre"]!.HeaderText = "Nombre";
            dgvProductos.Columns["precio_compra"]!.HeaderText = "P. compra";
            dgvProductos.Columns["precio_venta"]!.HeaderText = "P. venta";
            dgvProductos.Columns["stock"]!.HeaderText = "Stock";
            dgvProductos.Columns["categoria"]!.HeaderText = "Categoría";
            dgvProductos.Columns["proveedor"]!.HeaderText = "Proveedor";

            // Revisa el stock de cada producto y marca en rosa los que tienen 10 unidades o menos
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                if (int.TryParse(fila.Cells["stock"].Value?.ToString(), out int s) && s <= Config.StockBajo)
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
            }
        }

        private void LlenarFormulario(DataGridViewRow fila)
        {
            // Cambia el formulario al modo de edición de un producto existente
            esNuevo = false;

            // La llave primaria no se edita
            txtCodigo.ReadOnly = true;

            // El stock no se edita aquí; se modifica con las ventas o en Entradas y Salidas
            txtStock.ReadOnly = true;

            // Pasa los datos de la fila seleccionada a los campos del formulario
            txtCodigo.Text = fila.Cells["codigo_barras"].Value?.ToString();
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtPrecioCompra.Text = fila.Cells["precio_compra"].Value.ToString();
            txtPrecioVenta.Text = fila.Cells["precio_venta"].Value.ToString();
            txtStock.Text = fila.Cells["stock"].Value?.ToString();

            // Selecciona la categoría y el proveedor solo si el producto los tiene asignados
            if (fila.Cells["id_categoria"].Value is not DBNull)
                cmbCategoria.SelectedValue = Convert.ToInt64(fila.Cells["id_categoria"].Value);
            if (fila.Cells["id_proveedor"].Value is not DBNull)
                cmbProveedor.SelectedValue = Convert.ToInt64(fila.Cells["id_proveedor"].Value);
        }

        private void LimpiarFormulario()
        {
            // Regresa el formulario al modo de registro de un producto nuevo
            esNuevo = true;

            // En un producto nuevo sí se escribe el stock inicial
            txtStock.ReadOnly = false; 

            // Habilita nuevamente la edición del código y limpia los campos
            txtCodigo.ReadOnly = false;
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();

            // Selecciona la primera opción de cada lista, si existen elementos
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;

            // Quita la selección de la tabla y coloca el cursor en el código
            dgvProductos.ClearSelection();
            txtCodigo.Focus();
        }

        private void Guardar()
        {
            // Obtiene los datos escritos y elimina espacios innecesarios
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();

            // Comprueba que los campos obligatorios no estén vacíos
            if (codigo == "" || nombre == "")
            {
                MessageBox.Show("El código y el nombre son obligatorios.");
                return;
            }

            // Comprueba que los precios y el stock sean números válidos y no negativos
            if (!double.TryParse(txtPrecioCompra.Text, out double precioCompra) ||
                !double.TryParse(txtPrecioVenta.Text, out double precioVenta) ||
                !int.TryParse(txtStock.Text, out int stock) ||
                precioCompra < 0 || precioVenta < 0 || stock < 0)
            {
                MessageBox.Show("Revisa que los precios y el stock sean números válidos");
                return;
            }

            // Avisa si el precio de venta es menor al de compra y permite decidir si se guarda
            if (precioVenta < precioCompra)
            {
                var respuesta = MessageBox.Show(
                    "El precio de venta es menor al de compra: se vendería con pérdida.\n¿Guardar de todos modos?",
                    "Revisa el precio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta != DialogResult.Yes) return;
            }

            try
            {
                // Si es un producto nuevo, realiza un INSERT en la base de datos
                if (esNuevo)
                {
                    BaseDatos.Ejecutar(
                        @"INSERT INTO Producto (codigo_barras, nombre, precio_compra, precio_venta, stock, id_categoria, id_proveedor)
                        VALUES (@c, @n, @pc, @pv, @s, @cat, @prov)",
                        ("@c", codigo), ("@n", nombre), ("@pc", precioCompra), ("@pv", precioVenta),
                        ("@s", stock), ("@cat", cmbCategoria.SelectedValue!), ("@prov", cmbProveedor.SelectedValue!)
                        );
                }
                // Si ya existe, actualiza sus datos mediante UPDATE, sin modificar el stock
                else
                {
                    BaseDatos.Ejecutar(
                        @"UPDATE Producto
                            SET nombre = @n, precio_compra = @pc, precio_venta = @pv,
                                id_categoria = @cat, id_proveedor = @prov
                        WHERE codigo_barras = @c",
                        ("@c", codigo), ("@n", nombre), ("@pc", precioCompra), ("@pv", precioVenta),
                        ("@cat", cmbCategoria.SelectedValue!), ("@prov", cmbProveedor.SelectedValue!)
                        );
                }

                // Actualiza la tabla y limpia el formulario después de guardar
                CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Muestra un mensaje si ocurre algún error al guardar
                MessageBox.Show("No se pudo guardar (¿ya existe ese código?): \n" + ex.Message);
            }
        }

        private void Eliminar()
        {
            // Comprueba que se haya seleccionado un producto
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona un producto de la lista.");
                return;
            }

            // Solicita confirmación antes de eliminar el registro
            if (MessageBox.Show($"¿Eliminar \"{txtNombre.Text}\"?", "Confirmar",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                // Elimina el producto utilizando su código de barras
                BaseDatos.Ejecutar("DELETE FROM Producto WHERE codigo_barras = @c", ("@c", txtCodigo.Text));

                // Actualiza la tabla y limpia los campos
                CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Informa si no fue posible eliminar el producto
                MessageBox.Show("No se pudo eliminar (probablemente ya tiene ventas registradas):\n" + ex.Message);
            }
        }

        // Evento de dibujo del panel. Actualmente no contiene instrucciones
        private void pnlFormulario_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
