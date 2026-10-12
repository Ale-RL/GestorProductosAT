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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Enter estando en Usuario: borra la contraseña y pasa a escribirla
            if (keyData == Keys.Enter && ActiveControl == txtUsuario)
            {
                txtContrasena.Clear();
                txtContrasena.Focus();
                return true; //Le dice a windows que ya se atendio esa tecla
            }
            //Cualquier otro caso sigue normal
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                var tabla = BaseDatos.Consultar(
                    "SELECT nombre, rol FROM Usuario WHERE usuario = @u AND contrasena = @c",
                    ("@u", txtUsuario.Text.Trim()),
                    ("@c", txtContrasena.Text));

                if (tabla.Rows.Count == 0)
                {
                  MessageBox.Show("Usuario o contraseña Incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Sesion.Nombre = tabla.Rows[0]["nombre"].ToString();
                Sesion.Rol = tabla.Rows[0]["rol"].ToString();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos:\n" + ex.Message);
            }
        }

        private void btnSalirse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
