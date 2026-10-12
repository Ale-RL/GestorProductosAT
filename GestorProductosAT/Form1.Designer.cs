namespace GestorProductosAT
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlMenu = new Panel();
            pnlSuperior = new Panel();
            lblBienvenida = new Label();
            pnlContenido = new Panel();
            pnlSuperior.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(31, 92, 69);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(230, 451);
            pnlMenu.TabIndex = 0;
            // 
            // pnlSuperior
            // 
            pnlSuperior.BackColor = Color.White;
            pnlSuperior.Controls.Add(lblBienvenida);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(230, 0);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(671, 60);
            pnlSuperior.TabIndex = 1;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenida.Location = new Point(15, 15);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(63, 31);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Hola";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.White;
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(230, 60);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(671, 391);
            pnlContenido.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 451);
            Controls.Add(pnlContenido);
            Controls.Add(pnlSuperior);
            Controls.Add(pnlMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Gestor de Productos AT";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Panel pnlSuperior;
        private Label lblBienvenida;
        private Panel pnlContenido;
    }
}
