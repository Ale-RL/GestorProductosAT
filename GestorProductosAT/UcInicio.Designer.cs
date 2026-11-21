namespace GestorProductosAT
{
    partial class UcInicio
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
            pnlProductos = new Panel();
            lblProductosValor = new Label();
            lblProductosTitulo = new Label();
            pnlStockBajo = new Panel();
            lblStockBajoValor = new Label();
            lblStockBajoTitulo = new Label();
            pnlVentas = new Panel();
            lblVentasValor = new Label();
            lblVentasTitulo = new Label();
            label2 = new Label();
            dgvRecientes = new DataGridView();
            pnlSemana = new Panel();
            lblSemanaTitulo = new Label();
            lblSemanaValor = new Label();
            lblMasVendidos = new Label();
            dgvMasVendidos = new DataGridView();
            lblPorAgotarse = new Label();
            dgvPorAgotarse = new DataGridView();
            pnlProductos.SuspendLayout();
            pnlStockBajo.SuspendLayout();
            pnlVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecientes).BeginInit();
            pnlSemana.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMasVendidos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPorAgotarse).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 18);
            label1.Name = "label1";
            label1.Size = new Size(189, 31);
            label1.TabIndex = 0;
            label1.Text = "Resumen del día";
            // 
            // pnlProductos
            // 
            pnlProductos.BackColor = Color.White;
            pnlProductos.Controls.Add(lblProductosValor);
            pnlProductos.Controls.Add(lblProductosTitulo);
            pnlProductos.Location = new Point(15, 60);
            pnlProductos.Name = "pnlProductos";
            pnlProductos.Size = new Size(225, 110);
            pnlProductos.TabIndex = 1;
            // 
            // lblProductosValor
            // 
            lblProductosValor.AutoSize = true;
            lblProductosValor.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProductosValor.ForeColor = Color.ForestGreen;
            lblProductosValor.Location = new Point(15, 45);
            lblProductosValor.Name = "lblProductosValor";
            lblProductosValor.Size = new Size(46, 54);
            lblProductosValor.TabIndex = 1;
            lblProductosValor.Text = "0";
            // 
            // lblProductosTitulo
            // 
            lblProductosTitulo.AutoSize = true;
            lblProductosTitulo.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProductosTitulo.ForeColor = Color.DimGray;
            lblProductosTitulo.Location = new Point(15, 15);
            lblProductosTitulo.Name = "lblProductosTitulo";
            lblProductosTitulo.Size = new Size(152, 23);
            lblProductosTitulo.TabIndex = 0;
            lblProductosTitulo.Text = "Total de productos";
            // 
            // pnlStockBajo
            // 
            pnlStockBajo.BackColor = Color.White;
            pnlStockBajo.Controls.Add(lblStockBajoValor);
            pnlStockBajo.Controls.Add(lblStockBajoTitulo);
            pnlStockBajo.Location = new Point(255, 60);
            pnlStockBajo.Name = "pnlStockBajo";
            pnlStockBajo.Size = new Size(225, 110);
            pnlStockBajo.TabIndex = 2;
            // 
            // lblStockBajoValor
            // 
            lblStockBajoValor.AutoSize = true;
            lblStockBajoValor.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockBajoValor.ForeColor = Color.Firebrick;
            lblStockBajoValor.Location = new Point(15, 45);
            lblStockBajoValor.Name = "lblStockBajoValor";
            lblStockBajoValor.Size = new Size(46, 54);
            lblStockBajoValor.TabIndex = 1;
            lblStockBajoValor.Text = "0";
            // 
            // lblStockBajoTitulo
            // 
            lblStockBajoTitulo.AutoSize = true;
            lblStockBajoTitulo.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockBajoTitulo.ForeColor = Color.DimGray;
            lblStockBajoTitulo.Location = new Point(15, 15);
            lblStockBajoTitulo.Name = "lblStockBajoTitulo";
            lblStockBajoTitulo.Size = new Size(90, 23);
            lblStockBajoTitulo.TabIndex = 0;
            lblStockBajoTitulo.Text = "Stock bajo";
            // 
            // pnlVentas
            // 
            pnlVentas.BackColor = Color.White;
            pnlVentas.Controls.Add(lblVentasValor);
            pnlVentas.Controls.Add(lblVentasTitulo);
            pnlVentas.Location = new Point(495, 60);
            pnlVentas.Name = "pnlVentas";
            pnlVentas.Size = new Size(225, 110);
            pnlVentas.TabIndex = 3;
            // 
            // lblVentasValor
            // 
            lblVentasValor.AutoSize = true;
            lblVentasValor.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVentasValor.ForeColor = Color.ForestGreen;
            lblVentasValor.Location = new Point(15, 45);
            lblVentasValor.Name = "lblVentasValor";
            lblVentasValor.Size = new Size(126, 54);
            lblVentasValor.TabIndex = 1;
            lblVentasValor.Text = "$0.00";
            // 
            // lblVentasTitulo
            // 
            lblVentasTitulo.AutoSize = true;
            lblVentasTitulo.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            lblVentasTitulo.ForeColor = Color.DimGray;
            lblVentasTitulo.Location = new Point(15, 15);
            lblVentasTitulo.Name = "lblVentasTitulo";
            lblVentasTitulo.Size = new Size(117, 23);
            lblVentasTitulo.TabIndex = 0;
            lblVentasTitulo.Text = "Ventas del día";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 195);
            label2.Name = "label2";
            label2.Size = new Size(205, 25);
            label2.TabIndex = 4;
            label2.Text = "Movimientos recientes";
            // 
            // dgvRecientes
            // 
            dgvRecientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecientes.Location = new Point(15, 225);
            dgvRecientes.Name = "dgvRecientes";
            dgvRecientes.RowHeadersWidth = 51;
            dgvRecientes.Size = new Size(540, 380);
            dgvRecientes.TabIndex = 5;
            // 
            // pnlSemana
            // 
            pnlSemana.BackColor = Color.White;
            pnlSemana.Controls.Add(lblSemanaValor);
            pnlSemana.Controls.Add(lblSemanaTitulo);
            pnlSemana.Location = new Point(735, 60);
            pnlSemana.Name = "pnlSemana";
            pnlSemana.Size = new Size(225, 110);
            pnlSemana.TabIndex = 6;
            // 
            // lblSemanaTitulo
            // 
            lblSemanaTitulo.AutoSize = true;
            lblSemanaTitulo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSemanaTitulo.ForeColor = Color.DimGray;
            lblSemanaTitulo.Location = new Point(15, 15);
            lblSemanaTitulo.Name = "lblSemanaTitulo";
            lblSemanaTitulo.Size = new Size(171, 23);
            lblSemanaTitulo.TabIndex = 0;
            lblSemanaTitulo.Text = "Ventas de la semana";
            // 
            // lblSemanaValor
            // 
            lblSemanaValor.AutoSize = true;
            lblSemanaValor.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSemanaValor.ForeColor = Color.ForestGreen;
            lblSemanaValor.Location = new Point(15, 45);
            lblSemanaValor.Name = "lblSemanaValor";
            lblSemanaValor.Size = new Size(126, 54);
            lblSemanaValor.TabIndex = 1;
            lblSemanaValor.Text = "$0.00";
            // 
            // lblMasVendidos
            // 
            lblMasVendidos.AutoSize = true;
            lblMasVendidos.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblMasVendidos.ForeColor = Color.Black;
            lblMasVendidos.Location = new Point(570, 195);
            lblMasVendidos.Name = "lblMasVendidos";
            lblMasVendidos.Size = new Size(265, 25);
            lblMasVendidos.TabIndex = 7;
            lblMasVendidos.Text = "Más vendidos (últimos 7 días)";
            // 
            // dgvMasVendidos
            // 
            dgvMasVendidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMasVendidos.Location = new Point(570, 225);
            dgvMasVendidos.Name = "dgvMasVendidos";
            dgvMasVendidos.RowHeadersWidth = 51;
            dgvMasVendidos.Size = new Size(410, 180);
            dgvMasVendidos.TabIndex = 8;
            // 
            // lblPorAgotarse
            // 
            lblPorAgotarse.AutoSize = true;
            lblPorAgotarse.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblPorAgotarse.Location = new Point(570, 420);
            lblPorAgotarse.Name = "lblPorAgotarse";
            lblPorAgotarse.Size = new Size(119, 25);
            lblPorAgotarse.TabIndex = 9;
            lblPorAgotarse.Text = "Por agotarse";
            // 
            // dgvPorAgotarse
            // 
            dgvPorAgotarse.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPorAgotarse.Location = new Point(570, 450);
            dgvPorAgotarse.Name = "dgvPorAgotarse";
            dgvPorAgotarse.RowHeadersWidth = 51;
            dgvPorAgotarse.Size = new Size(410, 155);
            dgvPorAgotarse.TabIndex = 10;
            // 
            // UcInicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 253, 225);
            Controls.Add(dgvPorAgotarse);
            Controls.Add(lblPorAgotarse);
            Controls.Add(dgvMasVendidos);
            Controls.Add(lblMasVendidos);
            Controls.Add(pnlSemana);
            Controls.Add(dgvRecientes);
            Controls.Add(label2);
            Controls.Add(pnlVentas);
            Controls.Add(pnlStockBajo);
            Controls.Add(pnlProductos);
            Controls.Add(label1);
            Name = "UcInicio";
            Size = new Size(1000, 650);
            pnlProductos.ResumeLayout(false);
            pnlProductos.PerformLayout();
            pnlStockBajo.ResumeLayout(false);
            pnlStockBajo.PerformLayout();
            pnlVentas.ResumeLayout(false);
            pnlVentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecientes).EndInit();
            pnlSemana.ResumeLayout(false);
            pnlSemana.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMasVendidos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPorAgotarse).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel pnlProductos;
        private Panel pnlStockBajo;
        private Panel pnlVentas;
        private Label label2;
        private DataGridView dgvRecientes;
        private Label lblProductosValor;
        private Label lblProductosTitulo;
        private Label lblStockBajoTitulo;
        private Label lblStockBajoValor;
        private Label lblVentasValor;
        private Label lblVentasTitulo;
        private Panel pnlSemana;
        private Label lblSemanaValor;
        private Label lblSemanaTitulo;
        private Label lblMasVendidos;
        private DataGridView dgvMasVendidos;
        private Label lblPorAgotarse;
        private DataGridView dgvPorAgotarse;
    }
}
