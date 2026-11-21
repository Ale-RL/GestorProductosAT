namespace GestorProductosAT
{
    partial class UcEntradasSalidas
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
            lblfiltro = new Label();
            txtbuscar = new TextBox();
            cmbfiltro = new ComboBox();
            dgvmovimientos = new DataGridView();
            pnlformulario = new Panel();
            lblStockActual = new Label();
            label2 = new Label();
            nudCantidad = new NumericUpDown();
            cmbProveedor = new ComboBox();
            lblCantidad = new Label();
            comboBox1 = new ComboBox();
            lblProducto = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            lbltitulo = new Label();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvmovimientos).BeginInit();
            pnlformulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
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
            // lblfiltro
            // 
            lblfiltro.AutoSize = true;
            lblfiltro.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblfiltro.Location = new Point(350, 18);
            lblfiltro.Name = "lblfiltro";
            lblfiltro.Size = new Size(70, 20);
            lblfiltro.TabIndex = 1;
            lblfiltro.Text = "Mostrar:";
            // 
            // txtbuscar
            // 
            txtbuscar.Location = new Point(85, 15);
            txtbuscar.Name = "txtbuscar";
            txtbuscar.Size = new Size(250, 27);
            txtbuscar.TabIndex = 2;
            // 
            // cmbfiltro
            // 
            cmbfiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbfiltro.FormattingEnabled = true;
            cmbfiltro.Location = new Point(426, 15);
            cmbfiltro.Name = "cmbfiltro";
            cmbfiltro.Size = new Size(130, 28);
            cmbfiltro.TabIndex = 3;
            // 
            // dgvmovimientos
            // 
            dgvmovimientos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvmovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvmovimientos.Location = new Point(15, 65);
            dgvmovimientos.Name = "dgvmovimientos";
            dgvmovimientos.RowHeadersWidth = 51;
            dgvmovimientos.Size = new Size(640, 550);
            dgvmovimientos.TabIndex = 4;
            // 
            // pnlformulario
            // 
            pnlformulario.BackColor = Color.White;
            pnlformulario.Controls.Add(btnLimpiar);
            pnlformulario.Controls.Add(btnGuardar);
            pnlformulario.Controls.Add(lblStockActual);
            pnlformulario.Controls.Add(label2);
            pnlformulario.Controls.Add(nudCantidad);
            pnlformulario.Controls.Add(cmbProveedor);
            pnlformulario.Controls.Add(lblCantidad);
            pnlformulario.Controls.Add(comboBox1);
            pnlformulario.Controls.Add(lblProducto);
            pnlformulario.Controls.Add(cmbTipo);
            pnlformulario.Controls.Add(lblTipo);
            pnlformulario.Controls.Add(lbltitulo);
            pnlformulario.Dock = DockStyle.Right;
            pnlformulario.Location = new Point(674, 0);
            pnlformulario.Name = "pnlformulario";
            pnlformulario.Size = new Size(326, 650);
            pnlformulario.TabIndex = 5;
            // 
            // lblStockActual
            // 
            lblStockActual.AutoSize = true;
            lblStockActual.Location = new Point(24, 405);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(104, 20);
            lblStockActual.TabIndex = 9;
            lblStockActual.Text = "Stock Actual: -";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 318);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 8;
            label2.Text = "Provedor:";
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(24, 268);
            nudCantidad.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(280, 27);
            nudCantidad.TabIndex = 2;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(22, 353);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(280, 28);
            cmbProveedor.TabIndex = 3;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(24, 234);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(72, 20);
            lblCantidad.TabIndex = 5;
            lblCantidad.Text = "Cantidad:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(24, 184);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(280, 28);
            comboBox1.TabIndex = 1;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(24, 151);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(72, 20);
            lblProducto.TabIndex = 3;
            lblProducto.Text = "Producto:";
            lblProducto.Click += lblProducto_Click;
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(24, 101);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(280, 28);
            cmbTipo.TabIndex = 0;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTipo.Location = new Point(22, 65);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(147, 20);
            lblTipo.TabIndex = 1;
            lblTipo.Text = "Tipo de movimiento:";
            // 
            // lbltitulo
            // 
            lbltitulo.AutoSize = true;
            lbltitulo.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbltitulo.Location = new Point(56, 18);
            lbltitulo.Name = "lbltitulo";
            lbltitulo.Size = new Size(216, 25);
            lbltitulo.TabIndex = 0;
            lbltitulo.Text = "Registro de movimiento";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.ForestGreen;
            btnGuardar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.Transparent;
            btnGuardar.Location = new Point(34, 448);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(108, 39);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = SystemColors.HotTrack;
            btnLimpiar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.ForeColor = Color.Transparent;
            btnLimpiar.Location = new Point(184, 448);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(108, 39);
            btnLimpiar.TabIndex = 5;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // UcEntradasSalidas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 253, 225);
            Controls.Add(pnlformulario);
            Controls.Add(dgvmovimientos);
            Controls.Add(cmbfiltro);
            Controls.Add(txtbuscar);
            Controls.Add(lblfiltro);
            Controls.Add(label1);
            Name = "UcEntradasSalidas";
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)dgvmovimientos).EndInit();
            pnlformulario.ResumeLayout(false);
            pnlformulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblfiltro;
        private TextBox txtbuscar;
        private ComboBox cmbfiltro;
        private DataGridView dgvmovimientos;
        private Panel pnlformulario;
        private Label lblTipo;
        private Label lbltitulo;
        private Label lblProducto;
        private ComboBox cmbTipo;
        private ComboBox comboBox1;
        private ComboBox cmbProveedor;
        private Label lblCantidad;
        private NumericUpDown nudCantidad;
        private Label label2;
        private Label lblStockActual;
        private Button btnGuardar;
        private Button btnLimpiar;
    }
}
