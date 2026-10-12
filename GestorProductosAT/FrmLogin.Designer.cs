namespace GestorProductosAT
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            label1 = new Label();
            txtUsuario = new TextBox();
            label2 = new Label();
            txtContrasena = new TextBox();
            btnEntrar = new Button();
            pictureBox1 = new PictureBox();
            btnSalirse = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            label1.Location = new Point(45, 90);
            label1.Name = "label1";
            label1.Size = new Size(80, 25);
            label1.TabIndex = 0;
            label1.Text = "Usuario:";
            label1.Click += label1_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 10.8F);
            txtUsuario.ForeColor = Color.FromArgb(36, 59, 83);
            txtUsuario.Location = new Point(94, 139);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(350, 31);
            txtUsuario.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            label2.Location = new Point(45, 194);
            label2.Name = "label2";
            label2.Size = new Size(110, 25);
            label2.TabIndex = 2;
            label2.Text = "Contraseña:";
            // 
            // txtContrasena
            // 
            txtContrasena.Font = new Font("Segoe UI", 10.8F);
            txtContrasena.ForeColor = Color.FromArgb(36, 59, 83);
            txtContrasena.Location = new Point(94, 240);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(350, 31);
            txtContrasena.TabIndex = 3;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // btnEntrar
            // 
            btnEntrar.BackColor = Color.FromArgb(36, 92, 69);
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.Location = new Point(307, 298);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(137, 43);
            btnEntrar.TabIndex = 0;
            btnEntrar.Text = "Iniciar sesión";
            btnEntrar.UseVisualStyleBackColor = false;
            btnEntrar.Click += btnEntrar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(259, -7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(298, 177);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // btnSalirse
            // 
            btnSalirse.BackColor = Color.SaddleBrown;
            btnSalirse.Cursor = Cursors.Hand;
            btnSalirse.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalirse.ForeColor = Color.White;
            btnSalirse.Location = new Point(407, 412);
            btnSalirse.Name = "btnSalirse";
            btnSalirse.Size = new Size(90, 40);
            btnSalirse.TabIndex = 5;
            btnSalirse.Text = "Salir";
            btnSalirse.UseVisualStyleBackColor = false;
            btnSalirse.Click += btnSalirse_Click;
            // 
            // FrmLogin
            // 
            AcceptButton = btnEntrar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(540, 488);
            Controls.Add(btnSalirse);
            Controls.Add(btnEntrar);
            Controls.Add(txtContrasena);
            Controls.Add(label2);
            Controls.Add(txtUsuario);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestor de Productos AT";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUsuario;
        private Label label2;
        private TextBox txtContrasena;
        private Button btnEntrar;
        private PictureBox pictureBox1;
        private Button btnSalirse;
    }
}