
using System.Data;

namespace GestorProductosAT
{
    public partial class UcProveedores : UserControl
    {
        // Indica si se está registrando un proveedor nuevo o editando uno existente.
        private bool esNuevo = true;

        // Guarda el ID del proveedor que se está modificando.
        private long idActual = 0;

        public UcProveedores()
        {
            InitializeComponent();

            // Conecta los eventos de los controles con sus respectivos métodos.
            Load += UcProveedores_Load;
            txtBuscar.TextChanged += (s, e) => CargarProveedores();

            // Al seleccionar una fila, muestra los datos del proveedor en el formulario.
            dgvProveedores.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0) LlenarFormulario(dgvProveedores.Rows[e.RowIndex]);
            };

            // Configura las acciones de los botones.
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnGuardar.Click += (s, e) => Guardar();
            btnEliminar.Click += (s, e) => Eliminar();
        }

        private void UcProveedores_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar este código mientras se diseña la interfaz.
            if (DesignMode) return;

            // Aplica el estilo visual a los controles.
            Estilo.Aplicar(this);

            // Configura la tabla para mostrar los proveedores.
            dgvProveedores.ReadOnly = true;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.RowHeadersVisible = false;
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Carga los proveedores y deja el formulario listo para un registro nuevo.
            CargarProveedores();
            LimpiarFormulario();
        }

        private void CargarProveedores()
        {
            // Consulta los proveedores registrados y permite buscarlos por nombre,
            // teléfono o correo electrónico.
            var tabla = BaseDatos.Consultar(@"
                SELECT id_proveedor, nombre, telefono, email, direccion
                FROM Proveedor
                WHERE nombre LIKE @f OR telefono LIKE @f OR email LIKE @f
                ORDER BY nombre",
                ("@f", "%" + txtBuscar.Text.Trim() + "%"));

            // Muestra los resultados de la consulta en la tabla.
            dgvProveedores.DataSource = tabla;

            // Cambia los nombres de las columnas para facilitar su lectura.
            dgvProveedores.Columns["id_proveedor"]!.HeaderText = "ID";
            dgvProveedores.Columns["nombre"]!.HeaderText = "Nombre";
            dgvProveedores.Columns["telefono"]!.HeaderText = "Teléfono";
            dgvProveedores.Columns["email"]!.HeaderText = "Correo";
            dgvProveedores.Columns["direccion"]!.HeaderText = "Dirección";
        }

        private void LlenarFormulario(DataGridViewRow fila)
        {
            // Cambia el formulario al modo de edición.
            esNuevo = false;

            // Guarda el ID del proveedor seleccionado.
            idActual = Convert.ToInt64(fila.Cells["id_proveedor"].Value);

            // Coloca los datos del proveedor en los campos correspondientes.
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtTelefono.Text = fila.Cells["telefono"].Value?.ToString();
            txtEmail.Text = fila.Cells["email"].Value?.ToString();
            txtDireccion.Text = fila.Cells["direccion"].Value?.ToString();
        }

        private void LimpiarFormulario()
        {
            // Regresa el formulario al modo de registro de un proveedor nuevo.
            esNuevo = true;
            idActual = 0;

            // Limpia todos los campos de texto.
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();

            // Quita la selección de la tabla y coloca el cursor en el nombre.
            dgvProveedores.ClearSelection();
            txtNombre.Focus();
        }

        private void Guardar()
        {
            // Obtiene los datos escritos y elimina espacios innecesarios.
            string nombre = txtNombre.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string email = txtEmail.Text.Trim();
            string direccion = txtDireccion.Text.Trim();

            // Comprueba que el nombre del proveedor no esté vacío.
            if (nombre == "")
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            // Si se escribió un correo, comprueba que contenga el símbolo @.
            if (email != "" && !email.Contains('@'))
            {
                MessageBox.Show("El correo no parece válido (falta la @).");
                return;
            }

            try
            {
                // Si es un proveedor nuevo, lo registra en la base de datos.
                if (esNuevo)
                {
                    // El ID se genera automáticamente al insertar el proveedor.
                    BaseDatos.Ejecutar(
                        @"INSERT INTO Proveedor (nombre, telefono, email, direccion)
                          VALUES (@n, @t, @e, @d)",
                        ("@n", nombre), ("@t", telefono), ("@e", email), ("@d", direccion));
                }
                // Si ya existe, actualiza sus datos utilizando su ID.
                else
                {
                    BaseDatos.Ejecutar(
                        @"UPDATE Proveedor
                          SET nombre = @n, telefono = @t, email = @e, direccion = @d
                          WHERE id_proveedor = @id",
                        ("@n", nombre), ("@t", telefono), ("@e", email), ("@d", direccion),
                        ("@id", idActual));
                }

                // Actualiza la tabla y limpia el formulario después de guardar.
                CargarProveedores();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Muestra un mensaje si ocurre algún error al guardar.
                MessageBox.Show("No se pudo guardar:\n" + ex.Message);
            }
        }

        private void Eliminar()
        {
            // Comprueba que se haya seleccionado un proveedor.
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona un proveedor de la lista.");
                return;
            }

            // Solicita confirmación antes de eliminar el proveedor.
            if (MessageBox.Show($"¿Eliminar al proveedor \"{txtNombre.Text}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                // Elimina el proveedor utilizando su ID.
                BaseDatos.Ejecutar("DELETE FROM Proveedor WHERE id_proveedor = @id", ("@id", idActual));

                // Actualiza la tabla y limpia los campos.
                CargarProveedores();
                LimpiarFormulario();
            }
            catch (Exception)
            {
                // Informa si no es posible eliminarlo porque está relacionado con otros registros.
                MessageBox.Show("No se puede eliminar: hay productos o movimientos que usan este proveedor.");
            }
        }
    }
}
