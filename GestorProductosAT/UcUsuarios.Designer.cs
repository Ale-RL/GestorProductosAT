namespace GestorProductosAT
{
    partial class UcUsuarios
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtBuscar = new TextBox();
            dgvUsuarios = new DataGridView();
            panel1 = new Panel();
            label2 = new Label();
            txtNombre = new TextBox();
            label3 = new Label();
            txtUsuario = new TextBox();
            label4 = new Label();
            txtContrasena = new TextBox();
            label5 = new Label();
            label6 = new Label();
            cmbRol = new ComboBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 18);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 0;
            label1.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(85, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(250, 27);
            txtBuscar.TabIndex = 1;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(15, 55);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.Size = new Size(640, 560);
            dgvUsuarios.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnEliminar);
            panel1.Controls.Add(btnGuardar);
            panel1.Controls.Add(btnNuevo);
            panel1.Controls.Add(cmbRol);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtContrasena);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtUsuario);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtNombre);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(674, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(326, 650);
            panel1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(14, 65);
            label2.Name = "label2";
            label2.Size = new Size(71, 20);
            label2.TabIndex = 0;
            label2.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(90, 62);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(216, 27);
            txtNombre.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(14, 140);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 2;
            label3.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(90, 137);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(216, 27);
            txtUsuario.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(14, 201);
            label4.Name = "label4";
            label4.Size = new Size(92, 20);
            label4.TabIndex = 4;
            label4.Text = "Contraseña:";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(30, 247);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(276, 27);
            txtContrasena.TabIndex = 2;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.DimGray;
            label5.Location = new Point(170, 311);
            label5.Name = "label5";
            label5.Size = new Size(0, 17);
            label5.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(46, 334);
            label6.Name = "label6";
            label6.Size = new Size(35, 20);
            label6.TabIndex = 7;
            label6.Text = "Rol:";
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(90, 331);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(184, 28);
            cmbRol.TabIndex = 3;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.Blue;
            btnNuevo.ForeColor = Color.Transparent;
            btnNuevo.Location = new Point(30, 427);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(266, 29);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(0, 192, 0);
            btnGuardar.ForeColor = Color.Transparent;
            btnGuardar.Location = new Point(30, 505);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(266, 29);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Red;
            btnEliminar.ForeColor = Color.Transparent;
            btnEliminar.Location = new Point(30, 577);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(266, 29);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // UcUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 253, 225);
            Controls.Add(panel1);
            Controls.Add(dgvUsuarios);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Name = "UcUsuarios";
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscar;
        private DataGridView dgvUsuarios;
        private Panel panel1;
        private Label label2;
        private TextBox txtUsuario;
        private Label label3;
        private TextBox txtNombre;
        private Label label5;
        private TextBox txtContrasena;
        private Label label4;
        private Label label6;
        private ComboBox cmbRol;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNuevo;
    }
}
