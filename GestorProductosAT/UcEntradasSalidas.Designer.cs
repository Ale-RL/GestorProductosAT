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
            txtBuscar = new TextBox();
            cmbFiltro = new ComboBox();
            dgvMovimientos = new DataGridView();
            pnlFormulario = new Panel();
            btnLimpiar = new Button();
            btnGuardar = new Button();
            lblStockActual = new Label();
            lblProveedor = new Label();
            nudCantidad = new NumericUpDown();
            cmbProveedor = new ComboBox();
            lblCantidad = new Label();
            cmbProducto = new ComboBox();
            lblProducto = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            pnlFormulario.SuspendLayout();
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
            // txtBuscar
            // 
            txtBuscar.Location = new Point(85, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(250, 27);
            txtBuscar.TabIndex = 2;
            // 
            // cmbFiltro
            // 
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.FormattingEnabled = true;
            cmbFiltro.Location = new Point(426, 15);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(130, 28);
            cmbFiltro.TabIndex = 3;
            // 
            // dgvMovimientos
            // 
            dgvMovimientos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovimientos.Location = new Point(15, 65);
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.RowHeadersWidth = 51;
            dgvMovimientos.Size = new Size(640, 550);
            dgvMovimientos.TabIndex = 4;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.White;
            pnlFormulario.Controls.Add(btnLimpiar);
            pnlFormulario.Controls.Add(btnGuardar);
            pnlFormulario.Controls.Add(lblStockActual);
            pnlFormulario.Controls.Add(lblProveedor);
            pnlFormulario.Controls.Add(nudCantidad);
            pnlFormulario.Controls.Add(cmbProveedor);
            pnlFormulario.Controls.Add(lblCantidad);
            pnlFormulario.Controls.Add(cmbProducto);
            pnlFormulario.Controls.Add(lblProducto);
            pnlFormulario.Controls.Add(cmbTipo);
            pnlFormulario.Controls.Add(lblTipo);
            pnlFormulario.Controls.Add(lblTitulo);
            pnlFormulario.Dock = DockStyle.Right;
            pnlFormulario.Location = new Point(674, 0);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(326, 650);
            pnlFormulario.TabIndex = 5;
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
            // lblStockActual
            // 
            lblStockActual.AutoSize = true;
            lblStockActual.Location = new Point(24, 405);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(104, 20);
            lblStockActual.TabIndex = 9;
            lblStockActual.Text = "Stock Actual: -";
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Location = new Point(24, 318);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(80, 20);
            lblProveedor.TabIndex = 8;
            lblProveedor.Text = "Proveedor:";
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
            // cmbProducto
            // 
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(24, 184);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(280, 28);
            cmbProducto.TabIndex = 1;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(24, 151);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(72, 20);
            lblProducto.TabIndex = 3;
            lblProducto.Text = "Producto:";
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
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(56, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(216, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registro de movimiento";
            // 
            // UcEntradasSalidas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 253, 225);
            Controls.Add(pnlFormulario);
            Controls.Add(dgvMovimientos);
            Controls.Add(cmbFiltro);
            Controls.Add(txtBuscar);
            Controls.Add(lblfiltro);
            Controls.Add(label1);
            Name = "UcEntradasSalidas";
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblfiltro;
        private TextBox txtBuscar;
        private ComboBox cmbFiltro;
        private DataGridView dgvMovimientos;
        private Panel pnlFormulario;
        private Label lblTipo;
        private Label lblTitulo;
        private Label lblProducto;
        private ComboBox cmbTipo;
        private ComboBox cmbProducto;
        private ComboBox cmbProveedor;
        private Label lblCantidad;
        private NumericUpDown nudCantidad;
        private Label lblProveedor;
        private Label lblStockActual;
        private Button btnGuardar;
        private Button btnLimpiar;
    }
}
