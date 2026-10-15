namespace GestorProductosAT
{
    partial class UcProveedores
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
            dgvProveedores = new DataGridView();
            pnlFormulario = new Panel();
            txtNombre = new TextBox();
            txtTelefono = new TextBox();
            txtEmail = new TextBox();
            txtDireccion = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).BeginInit();
            pnlFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(15, 18);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 0;
            label1.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(77, 18);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(300, 27);
            txtBuscar.TabIndex = 1;
            // 
            // dgvProveedores
            // 
            dgvProveedores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedores.Location = new Point(15, 55);
            dgvProveedores.Name = "dgvProveedores";
            dgvProveedores.RowHeadersWidth = 51;
            dgvProveedores.Size = new Size(640, 560);
            dgvProveedores.TabIndex = 2;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.White;
            pnlFormulario.Controls.Add(btnEliminar);
            pnlFormulario.Controls.Add(btnGuardar);
            pnlFormulario.Controls.Add(btnNuevo);
            pnlFormulario.Controls.Add(label5);
            pnlFormulario.Controls.Add(label4);
            pnlFormulario.Controls.Add(label3);
            pnlFormulario.Controls.Add(label2);
            pnlFormulario.Controls.Add(txtDireccion);
            pnlFormulario.Controls.Add(txtEmail);
            pnlFormulario.Controls.Add(txtTelefono);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Dock = DockStyle.Right;
            pnlFormulario.Location = new Point(680, 0);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(320, 650);
            pnlFormulario.TabIndex = 3;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(15, 40);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(280, 27);
            txtNombre.TabIndex = 0;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(15, 100);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(280, 27);
            txtTelefono.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(15, 160);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(280, 27);
            txtEmail.TabIndex = 2;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(15, 220);
            txtDireccion.Multiline = true;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(280, 70);
            txtDireccion.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 18);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 4;
            label2.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 78);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 5;
            label3.Text = "Teléfono";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 138);
            label4.Name = "label4";
            label4.Size = new Size(132, 20);
            label4.TabIndex = 6;
            label4.Text = "Correo Electrónico";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 198);
            label5.Name = "label5";
            label5.Size = new Size(72, 20);
            label5.TabIndex = 7;
            label5.Text = "Dirección";
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(15, 320);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(85, 28);
            btnNuevo.TabIndex = 8;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(110, 320);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(85, 28);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(205, 320);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(85, 28);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // UcProveedores
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 253, 225);
            Controls.Add(pnlFormulario);
            Controls.Add(dgvProveedores);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Name = "UcProveedores";
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).EndInit();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscar;
        private DataGridView dgvProveedores;
        private Panel pnlFormulario;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtDireccion;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private TextBox txtNombre;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNuevo;
    }
}
