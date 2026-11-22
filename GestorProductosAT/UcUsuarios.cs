
using System.Data;

namespace GestorProductosAT
{
    public partial class UcUsuarios : UserControl
    {
        // Indica si se está registrando un usuario nuevo o editando uno existente.
        private bool esNuevo = true;

        // Guarda el ID del usuario que se está editando.
        private long idActual = 0;

        // Constructor del control de usuarios.
        public UcUsuarios()
        {
            InitializeComponent();

            // Ejecuta la configuración inicial cuando se carga el control.
            Load += UcUsuarios_Load;

            // Actualiza la lista cada vez que cambia el texto de búsqueda.
            txtBuscar.TextChanged += (s, e) => CargarUsuarios();

            // Al seleccionar una fila, muestra sus datos en el formulario.
            dgvUsuarios.CellClick += (s, e) =>
            {
                // Comprueba que se haya seleccionado una fila válida.
                if (e.RowIndex >= 0)
                    LlenarFormulario(dgvUsuarios.Rows[e.RowIndex]);
            };

            // El botón Nuevo limpia los campos para registrar otra cuenta.
            btnNuevo.Click += (s, e) => LimpiarFormulario();

            // El botón Guardar registra o actualiza los datos del usuario.
            btnGuardar.Click += (s, e) => Guardar();

            // El botón Eliminar ejecuta el proceso para borrar una cuenta.
            btnEliminar.Click += (s, e) => Eliminar();
        }

        // Configura los elementos del control cuando se carga.
        private void UcUsuarios_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar esta configuración desde el diseñador de Visual Studio.
            if (DesignMode) return;

            // Aplica el estilo visual general del sistema.
            Estilo.Aplicar(this);

            // Configura el DataGridView para mostrar únicamente los datos.
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Agrega los roles disponibles para las cuentas.
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Cajero");

            // Carga los usuarios registrados y limpia el formulario.
            CargarUsuarios();
            LimpiarFormulario();
        }

        // Consulta los usuarios de la base de datos y los muestra en la tabla.
        private void CargarUsuarios()
        {
            // Busca coincidencias en el nombre o nombre de usuario.
            // El símbolo % permite encontrar resultados que contengan el texto escrito.
            var tabla = BaseDatos.Consultar(@"
                SELECT id_usuario, nombre, usuario, rol
                FROM Usuario
                WHERE nombre LIKE @f OR usuario LIKE @f
                ORDER BY nombre",
                ("@f", "%" + txtBuscar.Text.Trim() + "%"));

            // Asigna los resultados de la consulta al DataGridView.
            dgvUsuarios.DataSource = tabla;

            // Cambia los encabezados de las columnas para mostrarlos en español.
            dgvUsuarios.Columns["id_usuario"]!.HeaderText = "ID";
            dgvUsuarios.Columns["nombre"]!.HeaderText = "Nombre";
            dgvUsuarios.Columns["usuario"]!.HeaderText = "Usuario";
            dgvUsuarios.Columns["rol"]!.HeaderText = "Rol";
        }

        // Coloca los datos del usuario seleccionado en los campos del formulario.
        private void LlenarFormulario(DataGridViewRow fila)
        {
            // Indica que se está editando un usuario existente.
            esNuevo = false;

            // Recupera el ID de la fila seleccionada.
            idActual = Convert.ToInt64(fila.Cells["id_usuario"].Value);

            // Copia los datos visibles a sus respectivos controles.
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtUsuario.Text = fila.Cells["usuario"].Value?.ToString();
            cmbRol.SelectedItem = fila.Cells["rol"].Value?.ToString();

            // Limpia el campo de contraseña porque no se guarda en texto legible.
            txtContrasena.Clear();
        }

        // Limpia los campos y prepara el formulario para un usuario nuevo.
        private void LimpiarFormulario()
        {
            // Activa el modo de registro y reinicia el ID actual.
            esNuevo = true;
            idActual = 0;

            // Borra el contenido de los campos.
            txtNombre.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();

            // Selecciona Cajero como rol predeterminado porque tiene menos permisos.
            cmbRol.SelectedIndex = 1;

            // Quita la selección de la tabla y coloca el cursor en el nombre.
            dgvUsuarios.ClearSelection();
            txtNombre.Focus();
        }

        // Valida y guarda los datos de un usuario nuevo o existente.
        private void Guardar()
        {
            // Obtiene los valores escritos en el formulario.
            // Trim elimina los espacios al principio y al final.
            string nombre = txtNombre.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;
            string rol = cmbRol.Text;

            // Comprueba que los campos obligatorios tengan información.
            if (nombre == "" || usuario == "")
            {
                MessageBox.Show("El nombre y el usuario son obligatorios.");
                return;
            }

            // Cuando se crea una cuenta, es obligatorio establecer una contraseña.
            if (esNuevo && contrasena == "")
            {
                MessageBox.Show("La contraseña es obligatoria para un usuario nuevo.");
                return;
            }

            // Si se escribió una contraseña, comprueba que tenga al menos 4 caracteres.
            if (contrasena != "" && contrasena.Length < 4)
            {
                MessageBox.Show("La contraseña debe tener al menos 4 caracteres.");
                return;
            }

            // Evita que el administrador se quite sus propios permisos y deje el sistema sin admin.
            // pendejin por que harias eso blo
            if (!esNuevo && idActual == Sesion.Id && rol != "Administrador")
            {
                MessageBox.Show("No puedes quitarte el rol de administrador a ti mismo.");
                return;
            }

            try
            {
                // Si es un usuario nuevo, inserta un registro en la tabla Usuario.
                if (esNuevo)
                {
                    BaseDatos.Ejecutar(
                        "INSERT INTO Usuario (nombre, usuario, contrasena, rol) VALUES (@n, @u, @c, @r)",
                        ("@n", nombre), ("@u", usuario),
                        // La contraseña se convierte en un hash antes de guardarse.
                        ("@c", Seguridad.Hashear(contrasena)), ("@r", rol));
                }
                // Si se está editando y la contraseña está vacía, conserva la anterior.
                else if (contrasena == "")
                {
                    // Contraseña vacía al editar = no se cambia.
                    BaseDatos.Ejecutar(
                        "UPDATE Usuario SET nombre = @n, usuario = @u, rol = @r WHERE id_usuario = @id",
                        ("@n", nombre), ("@u", usuario), ("@r", rol), ("@id", idActual));
                }
                // Si se escribió una contraseña nueva, también actualiza ese campo.
                else
                {
                    BaseDatos.Ejecutar(
                        "UPDATE Usuario SET nombre = @n, usuario = @u, contrasena = @c, rol = @r WHERE id_usuario = @id",
                        ("@n", nombre), ("@u", usuario),
                        // Guarda el hash de la nueva contraseña, no el texto original.
                        ("@c", Seguridad.Hashear(contrasena)), ("@r", rol), ("@id", idActual));
                }

                // Actualiza la tabla y limpia el formulario después de guardar.
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // La columna usuario es UNIQUE: si ya existe, la base lo rechaza.
                MessageBox.Show("No se pudo guardar (¿ya existe ese usuario?):\n" + ex.Message);
            }
        }

        // Elimina el usuario seleccionado de la base de datos.
        private void Eliminar()
        {
            // Comprueba que se haya seleccionado un usuario.
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona un usuario de la lista.");
                return;
            }

            // Impide eliminar la cuenta que tiene la sesión abierta.
            if (idActual == Sesion.Id)
            {
                MessageBox.Show("No puedes eliminar tu propio usuario mientras tienes la sesión abierta.");
                return;
            }

            // Solicita confirmación antes de borrar el registro.
            if (MessageBox.Show($"¿Eliminar al usuario \"{txtUsuario.Text}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                // Elimina el registro que coincide con el ID seleccionado.
                BaseDatos.Ejecutar("DELETE FROM Usuario WHERE id_usuario = @id", ("@id", idActual));

                // Actualiza la lista y limpia los campos.
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Muestra el error si no se pudo completar la eliminación.
                MessageBox.Show("No se pudo eliminar:\n" + ex.Message);
            }
        }
    }
}
