namespace GestorProductosAT
{
    partial class UcPuntoVenta
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
            txtCodigo = new TextBox();
            label2 = new Label();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            dgvCarrito = new DataGridView();
            btnQuitar = new Button();
            btnVaciar = new Button();
            pnlCobro = new Panel();
            btnCobrar = new Button();
            lblCambio = new Label();
            txtRecibido = new TextBox();
            label4 = new Label();
            cmbMetodo = new ComboBox();
            label3 = new Label();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).BeginInit();
            pnlCobro.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(15, 18);
            label1.Name = "label1";
            label1.Size = new Size(131, 20);
            label1.TabIndex = 0;
            label1.Text = "Código de barras:";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(150, 15);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(250, 27);
            txtCodigo.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(420, 18);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 2;
            label2.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(500, 15);
            nudCantidad.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(67, 27);
            nudCantidad.TabIndex = 3;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(580, 13);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(90, 29);
            btnAgregar.TabIndex = 4;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // dgvCarrito
            // 
            dgvCarrito.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCarrito.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCarrito.Location = new Point(15, 55);
            dgvCarrito.Name = "dgvCarrito";
            dgvCarrito.RowHeadersWidth = 51;
            dgvCarrito.Size = new Size(640, 480);
            dgvCarrito.TabIndex = 5;
            // 
            // btnQuitar
            // 
            btnQuitar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnQuitar.BackColor = Color.Red;
            btnQuitar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnQuitar.ForeColor = SystemColors.ControlLightLight;
            btnQuitar.Location = new Point(15, 550);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(160, 29);
            btnQuitar.TabIndex = 6;
            btnQuitar.Text = "Quitar seleccionado";
            btnQuitar.UseVisualStyleBackColor = false;
            // 
            // btnVaciar
            // 
            btnVaciar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVaciar.BackColor = Color.FromArgb(0, 0, 192);
            btnVaciar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnVaciar.ForeColor = SystemColors.ControlLightLight;
            btnVaciar.Location = new Point(185, 550);
            btnVaciar.Name = "btnVaciar";
            btnVaciar.Size = new Size(140, 29);
            btnVaciar.TabIndex = 7;
            btnVaciar.Text = "Vaciar carrito";
            btnVaciar.UseVisualStyleBackColor = false;
            // 
            // pnlCobro
            // 
            pnlCobro.BackColor = Color.White;
            pnlCobro.Controls.Add(btnCobrar);
            pnlCobro.Controls.Add(lblCambio);
            pnlCobro.Controls.Add(txtRecibido);
            pnlCobro.Controls.Add(label4);
            pnlCobro.Controls.Add(cmbMetodo);
            pnlCobro.Controls.Add(label3);
            pnlCobro.Controls.Add(lblTotal);
            pnlCobro.Dock = DockStyle.Right;
            pnlCobro.Location = new Point(680, 0);
            pnlCobro.Name = "pnlCobro";
            pnlCobro.Size = new Size(320, 650);
            pnlCobro.TabIndex = 8;
            // 
            // btnCobrar
            // 
            btnCobrar.BackColor = Color.ForestGreen;
            btnCobrar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnCobrar.ForeColor = SystemColors.Window;
            btnCobrar.Location = new Point(15, 320);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(280, 50);
            btnCobrar.TabIndex = 6;
            btnCobrar.Text = "COBRAR";
            btnCobrar.UseVisualStyleBackColor = false;
            // 
            // lblCambio
            // 
            lblCambio.AutoSize = true;
            lblCambio.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCambio.Location = new Point(15, 255);
            lblCambio.Name = "lblCambio";
            lblCambio.Size = new Size(131, 31);
            lblCambio.TabIndex = 5;
            lblCambio.Text = "Cambio: —";
            // 
            // txtRecibido
            // 
            txtRecibido.Location = new Point(15, 210);
            txtRecibido.Name = "txtRecibido";
            txtRecibido.Size = new Size(280, 27);
            txtRecibido.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(15, 185);
            label4.Name = "label4";
            label4.Size = new Size(125, 20);
            label4.TabIndex = 3;
            label4.Text = "Efectivo recibido";
            // 
            // cmbMetodo
            // 
            cmbMetodo.FormattingEnabled = true;
            cmbMetodo.Location = new Point(15, 135);
            cmbMetodo.Name = "cmbMetodo";
            cmbMetodo.Size = new Size(280, 28);
            cmbMetodo.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(15, 110);
            label3.Name = "label3";
            label3.Size = new Size(124, 20);
            label3.TabIndex = 1;
            label3.Text = "Método de pago";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(15, 30);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(180, 41);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total: $0.00";
            // 
            // UcPuntoVenta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 253, 225);
            Controls.Add(pnlCobro);
            Controls.Add(btnVaciar);
            Controls.Add(btnQuitar);
            Controls.Add(dgvCarrito);
            Controls.Add(btnAgregar);
            Controls.Add(nudCantidad);
            Controls.Add(label2);
            Controls.Add(txtCodigo);
            Controls.Add(label1);
            Name = "UcPuntoVenta";
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).EndInit();
            pnlCobro.ResumeLayout(false);
            pnlCobro.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtCodigo;
        private Label label2;
        private NumericUpDown nudCantidad;
        private Button btnAgregar;
        private DataGridView dgvCarrito;
        private Button btnQuitar;
        private Button btnVaciar;
        private Panel pnlCobro;
        private ComboBox cmbMetodo;
        private Label label3;
        private Label lblTotal;
        private Button btnCobrar;
        private Label lblCambio;
        private TextBox txtRecibido;
        private Label label4;
    }
}
