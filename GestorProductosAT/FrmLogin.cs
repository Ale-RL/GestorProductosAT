using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GestorProductosAT
{
    public partial class FrmLogin : Form
    {
        // Permite controlar algunas teclas antes de que se procesen normalmente
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Enter estando en Usuario: borra la contraseña y pasa a escribirla
            if (keyData == Keys.Enter && ActiveControl == txtUsuario)
            {
                txtContrasena.Clear();
                txtContrasena.Focus();
                return true; // Le dice a Windows que ya se atendió esa tecla
            }

            // Cualquier otro caso sigue el comportamiento normal
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Constructor del formulario de inicio de sesión
        public FrmLogin()
        {
            InitializeComponent(); // Carga los controles del formulario
            Estilo.Aplicar(this);  // Aplica el diseño personalizado
        }

        // Evento que se ejecuta al hacer clic en la etiqueta
        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al presionar el botón Entrar
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca al usuario en la base de datos utilizando el nombre ingresado
                var tabla = BaseDatos.Consultar(
                    "SELECT id_usuario, nombre, rol, contrasena FROM Usuario WHERE usuario = @u",
                    ("@u", txtUsuario.Text.Trim()));

                // Se busca solo por usuario; la contraseña se compara aparte porque está protegida
                if (tabla.Rows.Count == 0 ||
                    !Seguridad.Verificar(txtContrasena.Text, tabla.Rows[0]["contrasena"].ToString()!))
                {
                    // Muestra un mensaje si el usuario no existe o la contraseña es incorrecta
                    MessageBox.Show("Usuario o contraseña Incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Detiene el proceso de inicio de sesión
                }

                // Guarda el identificador del usuario que inició sesión
                Sesion.Id = Convert.ToInt64(tabla.Rows[0]["id_usuario"]);

                // Guarda el nombre del usuario
                Sesion.Nombre = tabla.Rows[0]["nombre"].ToString();

                // Guarda el rol del usuario para identificar sus permisos
                Sesion.Rol = tabla.Rows[0]["rol"].ToString();

                // Indica que el inicio de sesión fue exitoso
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                // Muestra un mensaje si ocurre algún problema al consultar la base de datos
                MessageBox.Show("No se pudo conectar a la base de datos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta al presionar el botón para salir
        private void btnSalirse_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario de inicio de sesión
        }
    }
}