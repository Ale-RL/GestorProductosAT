using System.Data;

namespace GestorProductosAT
{
    public partial class UcUsuarios : UserControl
    {
        private bool esNuevo = true;
        private long idActual = 0; // id del usuario que se está editando

        public UcUsuarios()
        {
            InitializeComponent();
            Load += UcUsuarios_Load;
            txtBuscar.TextChanged += (s, e) => CargarUsuarios();
            dgvUsuarios.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0) LlenarFormulario(dgvUsuarios.Rows[e.RowIndex]);
            };
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnGuardar.Click += (s, e) => Guardar();
            btnEliminar.Click += (s, e) => Eliminar();
        }

        private void UcUsuarios_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cmbRol.Items.Clear();
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Cajero");

            CargarUsuarios();
            LimpiarFormulario();
        }

        private void CargarUsuarios()
        {
            var tabla = BaseDatos.Consultar(@"
                SELECT id_usuario, nombre, usuario, rol
                FROM Usuario
                WHERE nombre LIKE @f OR usuario LIKE @f
                ORDER BY nombre",
                ("@f", "%" + txtBuscar.Text.Trim() + "%"));

            dgvUsuarios.DataSource = tabla;

            dgvUsuarios.Columns["id_usuario"]!.HeaderText = "ID";
            dgvUsuarios.Columns["nombre"]!.HeaderText = "Nombre";
            dgvUsuarios.Columns["usuario"]!.HeaderText = "Usuario";
            dgvUsuarios.Columns["rol"]!.HeaderText = "Rol";
        }

        private void LlenarFormulario(DataGridViewRow fila)
        {
            esNuevo = false;
            idActual = Convert.ToInt64(fila.Cells["id_usuario"].Value);
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtUsuario.Text = fila.Cells["usuario"].Value?.ToString();
            cmbRol.SelectedItem = fila.Cells["rol"].Value?.ToString();
            txtContrasena.Clear(); // la contraseña vieja no se puede mostrar: no existe en texto
        }

        private void LimpiarFormulario()
        {
            esNuevo = true;
            idActual = 0;
            txtNombre.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
            cmbRol.SelectedIndex = 1; // por defecto "Cajero": es el rol con menos permisos
            dgvUsuarios.ClearSelection();
            txtNombre.Focus();
        }

        private void Guardar()
        {
            string nombre = txtNombre.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;
            string rol = cmbRol.Text;

            if (nombre == "" || usuario == "")
            {
                MessageBox.Show("El nombre y el usuario son obligatorios.");
                return;
            }

            if (esNuevo && contrasena == "")
            {
                MessageBox.Show("La contraseña es obligatoria para un usuario nuevo.");
                return;
            }

            if (contrasena != "" && contrasena.Length < 4)
            {
                MessageBox.Show("La contraseña debe tener al menos 4 caracteres.");
                return;
            }

            // Evita que el administrador se quite sus propios permisos y deje el sistema sin admin
            // pendejin por que harias eso blo
            if (!esNuevo && idActual == Sesion.Id && rol != "Administrador")
            {
                MessageBox.Show("No puedes quitarte el rol de administrador a ti mismo.");
                return;
            }

            try
            {
                if (esNuevo)
                {
                    BaseDatos.Ejecutar(
                        "INSERT INTO Usuario (nombre, usuario, contrasena, rol) VALUES (@n, @u, @c, @r)",
                        ("@n", nombre), ("@u", usuario),
                        ("@c", Seguridad.Hashear(contrasena)), ("@r", rol));
                }
                else if (contrasena == "")
                {
                    // Contraseña vacía al editar = no se cambia
                    BaseDatos.Ejecutar(
                        "UPDATE Usuario SET nombre = @n, usuario = @u, rol = @r WHERE id_usuario = @id",
                        ("@n", nombre), ("@u", usuario), ("@r", rol), ("@id", idActual));
                }
                else
                {
                    BaseDatos.Ejecutar(
                        "UPDATE Usuario SET nombre = @n, usuario = @u, contrasena = @c, rol = @r WHERE id_usuario = @id",
                        ("@n", nombre), ("@u", usuario),
                        ("@c", Seguridad.Hashear(contrasena)), ("@r", rol), ("@id", idActual));
                }

                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // La columna usuario es UNIQUE: si ya existe, la base lo rechaza
                MessageBox.Show("No se pudo guardar (¿ya existe ese usuario?):\n" + ex.Message);
            }
        }

        private void Eliminar()
        {
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona un usuario de la lista.");
                return;
            }

            if (idActual == Sesion.Id)
            {
                MessageBox.Show("No puedes eliminar tu propio usuario mientras tienes la sesión abierta.");
                return;
            }

            if (MessageBox.Show($"¿Eliminar al usuario \"{txtUsuario.Text}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                BaseDatos.Ejecutar("DELETE FROM Usuario WHERE id_usuario = @id", ("@id", idActual));
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar:\n" + ex.Message);
            }
        }
    }
}