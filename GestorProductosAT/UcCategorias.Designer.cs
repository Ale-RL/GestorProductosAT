namespace GestorProductosAT
{
    partial class UcCategorias
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
            dgvCategorias = new DataGridView();
            pnlFormulario = new Panel();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNuevo = new Button();
            label3 = new Label();
            label2 = new Label();
            txtDescripcion = new TextBox();
            txtNombre = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
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
            txtBuscar.Location = new Point(75, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(300, 27);
            txtBuscar.TabIndex = 1;
            // 
            // dgvCategorias
            // 
            dgvCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Location = new Point(15, 55);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.Size = new Size(640, 560);
            dgvCategorias.TabIndex = 2;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.White;
            pnlFormulario.Controls.Add(btnEliminar);
            pnlFormulario.Controls.Add(btnGuardar);
            pnlFormulario.Controls.Add(btnNuevo);
            pnlFormulario.Controls.Add(label3);
            pnlFormulario.Controls.Add(label2);
            pnlFormulario.Controls.Add(txtDescripcion);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Dock = DockStyle.Right;
            pnlFormulario.Location = new Point(680, 0);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(320, 650);
            pnlFormulario.TabIndex = 3;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(205, 220);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(85, 28);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(110, 220);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(85, 28);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(15, 220);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(85, 28);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 78);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 3;
            label3.Text = "Descripción";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 18);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 2;
            label2.Text = "Nombre";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(15, 100);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(280, 90);
            txtDescripcion.TabIndex = 1;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(15, 40);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(280, 27);
            txtNombre.TabIndex = 0;
            // 
            // UcCategorias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 254, 225);
            Controls.Add(pnlFormulario);
            Controls.Add(dgvCategorias);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Name = "UcCategorias";
            Size = new Size(1000, 650);
            Load += UcCategorias_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscar;
        private DataGridView dgvCategorias;
        private Panel pnlFormulario;
        private Label label2;
        private TextBox txtDescripcion;
        private TextBox txtNombre;
        private Label label3;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNuevo;
    }
}
