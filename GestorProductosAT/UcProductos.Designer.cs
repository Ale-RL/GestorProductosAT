namespace GestorProductosAT
{
    partial class UcProductos
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
            dgvProductos = new DataGridView();
            pnlFormulario = new Panel();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNuevo = new Button();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            txtPrecioVenta = new TextBox();
            txtStock = new TextBox();
            txtPrecioCompra = new TextBox();
            txtNombre = new TextBox();
            txtCodigo = new TextBox();
            label2 = new Label();
            cmbCategoria = new ComboBox();
            cmbProveedor = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            pnlFormulario.SuspendLayout();
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
            txtBuscar.Size = new Size(300, 27);
            txtBuscar.TabIndex = 1;
            // 
            // dgvProductos
            // 
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(15, 55);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.Size = new Size(640, 560);
            dgvProductos.TabIndex = 2;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.White;
            pnlFormulario.Controls.Add(cmbProveedor);
            pnlFormulario.Controls.Add(cmbCategoria);
            pnlFormulario.Controls.Add(btnEliminar);
            pnlFormulario.Controls.Add(btnGuardar);
            pnlFormulario.Controls.Add(btnNuevo);
            pnlFormulario.Controls.Add(label8);
            pnlFormulario.Controls.Add(label7);
            pnlFormulario.Controls.Add(label6);
            pnlFormulario.Controls.Add(label5);
            pnlFormulario.Controls.Add(label4);
            pnlFormulario.Controls.Add(label3);
            pnlFormulario.Controls.Add(txtPrecioVenta);
            pnlFormulario.Controls.Add(txtStock);
            pnlFormulario.Controls.Add(txtPrecioCompra);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(txtCodigo);
            pnlFormulario.Controls.Add(label2);
            pnlFormulario.Dock = DockStyle.Right;
            pnlFormulario.Location = new Point(680, 0);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(320, 650);
            pnlFormulario.TabIndex = 3;
            pnlFormulario.Paint += pnlFormulario_Paint;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(205, 460);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(85, 28);
            btnEliminar.TabIndex = 16;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(110, 460);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(85, 28);
            btnGuardar.TabIndex = 15;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(15, 460);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(85, 28);
            btnNuevo.TabIndex = 14;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 378);
            label8.Name = "label8";
            label8.Size = new Size(77, 20);
            label8.TabIndex = 13;
            label8.Text = "Proveedor";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 318);
            label7.Name = "label7";
            label7.Size = new Size(74, 20);
            label7.TabIndex = 12;
            label7.Text = "Categoría";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 258);
            label6.Name = "label6";
            label6.Size = new Size(45, 20);
            label6.TabIndex = 11;
            label6.Text = "Stock";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 198);
            label5.Name = "label5";
            label5.Size = new Size(111, 20);
            label5.TabIndex = 10;
            label5.Text = "Precio de venta";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 138);
            label4.Name = "label4";
            label4.Size = new Size(126, 20);
            label4.TabIndex = 9;
            label4.Text = "Precio de compra";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 78);
            label3.Name = "label3";
            label3.Size = new Size(64, 20);
            label3.TabIndex = 8;
            label3.Text = "Nombre";
            // 
            // txtPrecioVenta
            // 
            txtPrecioVenta.Location = new Point(15, 220);
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.Size = new Size(280, 27);
            txtPrecioVenta.TabIndex = 5;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(15, 280);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(280, 27);
            txtStock.TabIndex = 4;
            // 
            // txtPrecioCompra
            // 
            txtPrecioCompra.Location = new Point(15, 160);
            txtPrecioCompra.Name = "txtPrecioCompra";
            txtPrecioCompra.Size = new Size(280, 27);
            txtPrecioCompra.TabIndex = 3;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(15, 100);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(280, 27);
            txtNombre.TabIndex = 2;
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(15, 40);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(280, 27);
            txtCodigo.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 18);
            label2.Name = "label2";
            label2.Size = new Size(124, 20);
            label2.TabIndex = 0;
            label2.Text = "Código de barras";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(15, 340);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(280, 28);
            cmbCategoria.TabIndex = 17;
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(15, 400);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(280, 28);
            cmbProveedor.TabIndex = 18;
            // 
            // UcProductos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 253, 225);
            Controls.Add(pnlFormulario);
            Controls.Add(dgvProductos);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Name = "UcProductos";
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscar;
        private DataGridView dgvProductos;
        private Panel pnlFormulario;
        private Label label2;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox txtPrecioVenta;
        private TextBox txtStock;
        private TextBox txtPrecioCompra;
        private TextBox txtNombre;
        private TextBox txtCodigo;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNuevo;
        private ComboBox cmbProveedor;
        private ComboBox cmbCategoria;
    }
}
