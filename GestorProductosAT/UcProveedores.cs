using System.Data;

namespace GestorProductosAT
{
    public partial class UcProveedores : UserControl
    {
        private bool esNuevo = true;
        private long idActual = 0; // id del proveedor que se está editando

        public UcProveedores()
        {
            InitializeComponent();
            Load += UcProveedores_Load;
            txtBuscar.TextChanged += (s, e) => CargarProveedores();
            dgvProveedores.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0) LlenarFormulario(dgvProveedores.Rows[e.RowIndex]);
            };
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnGuardar.Click += (s, e) => Guardar();
            btnEliminar.Click += (s, e) => Eliminar();
        }

        private void UcProveedores_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            dgvProveedores.ReadOnly = true;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.RowHeadersVisible = false;
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            CargarProveedores();
            LimpiarFormulario();
        }

        private void CargarProveedores()
        {
            var tabla = BaseDatos.Consultar(@"
                SELECT id_proveedor, nombre, telefono, email, direccion
                FROM Proveedor
                WHERE nombre LIKE @f OR telefono LIKE @f OR email LIKE @f
                ORDER BY nombre",
                ("@f", "%" + txtBuscar.Text.Trim() + "%"));

            dgvProveedores.DataSource = tabla;

            dgvProveedores.Columns["id_proveedor"]!.HeaderText = "ID";
            dgvProveedores.Columns["nombre"]!.HeaderText = "Nombre";
            dgvProveedores.Columns["telefono"]!.HeaderText = "Teléfono";
            dgvProveedores.Columns["email"]!.HeaderText = "Correo";
            dgvProveedores.Columns["direccion"]!.HeaderText = "Dirección";
        }

        private void LlenarFormulario(DataGridViewRow fila)
        {
            esNuevo = false;
            idActual = Convert.ToInt64(fila.Cells["id_proveedor"].Value);
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtTelefono.Text = fila.Cells["telefono"].Value?.ToString();
            txtEmail.Text = fila.Cells["email"].Value?.ToString();
            txtDireccion.Text = fila.Cells["direccion"].Value?.ToString();
        }

        private void LimpiarFormulario()
        {
            esNuevo = true;
            idActual = 0;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            dgvProveedores.ClearSelection();
            txtNombre.Focus();
        }

        private void Guardar()
        {
            string nombre = txtNombre.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string email = txtEmail.Text.Trim();
            string direccion = txtDireccion.Text.Trim();

            if (nombre == "")
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            // Si escribió correo, debe tener al menos una @
            // para que funcione
            if (email != "" && !email.Contains('@'))
            {
                MessageBox.Show("El correo no parece válido (falta la @).");
                return;
            }

            try
            {
                if (esNuevo)
                {
                    // El id se genera solo tampoco hay necesidad de escribirlo
                    BaseDatos.Ejecutar(
                        @"INSERT INTO Proveedor (nombre, telefono, email, direccion)
                          VALUES (@n, @t, @e, @d)",
                        ("@n", nombre), ("@t", telefono), ("@e", email), ("@d", direccion));
                }
                else
                {
                    BaseDatos.Ejecutar(
                        @"UPDATE Proveedor
                          SET nombre = @n, telefono = @t, email = @e, direccion = @d
                          WHERE id_proveedor = @id",
                        ("@n", nombre), ("@t", telefono), ("@e", email), ("@d", direccion),
                        ("@id", idActual));
                }

                CargarProveedores();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar:\n" + ex.Message);
            }
        }

        private void Eliminar()
        {
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona un proveedor de la lista.");
                return;
            }

            if (MessageBox.Show($"¿Eliminar al proveedor \"{txtNombre.Text}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                BaseDatos.Ejecutar("DELETE FROM Proveedor WHERE id_proveedor = @id", ("@id", idActual));
                CargarProveedores();
                LimpiarFormulario();
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar: hay productos o movimientos que usan este proveedor.");
            }
        }
    }
}