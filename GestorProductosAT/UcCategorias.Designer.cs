namespace GestorProductosAT
{
    // Clase parcial que contiene los elementos visuales del control de categorías
    partial class UcCategorias
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        // Contenedor que administra los componentes del control
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        // Libera los recursos utilizados por los componentes
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            // Ejecuta el método Dispose de la clase base
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        // Crea y configura todos los elementos visuales del control
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

            // Prepara los componentes para configurar la tabla y el panel
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            pnlFormulario.SuspendLayout();
            SuspendLayout();

            // Configuración de la etiqueta de búsqueda
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(15, 18);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 0;
            label1.Text = "Buscar:";

            // Configuración del campo donde se escribe la búsqueda
            txtBuscar.Location = new Point(75, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(300, 27);
            txtBuscar.TabIndex = 1;

            // Configuración de la tabla donde se muestran las categorías
            dgvCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Location = new Point(15, 55);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.Size = new Size(640, 560);
            dgvCategorias.TabIndex = 2;

            // Configuración del panel que contiene el formulario
            pnlFormulario.BackColor = Color.White;

            // Agrega los botones, etiquetas y campos de texto al panel
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

            // Configuración del botón para eliminar una categoría
            btnEliminar.Location = new Point(205, 220);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(85, 28);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;

            // Configuración del botón para guardar una categoría
            btnGuardar.Location = new Point(110, 220);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(85, 28);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;

            // Configuración del botón para iniciar un registro nuevo
            btnNuevo.Location = new Point(15, 220);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(85, 28);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;

            // Configuración de la etiqueta de descripción
            label3.AutoSize = true;
            label3.Location = new Point(15, 78);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 3;
            label3.Text = "Descripción";

            // Configuración de la etiqueta de nombre
            label2.AutoSize = true;
            label2.Location = new Point(15, 18);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 2;
            label2.Text = "Nombre";

            // Configuración del campo para escribir la descripción
            txtDescripcion.Location = new Point(15, 100);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(280, 90);
            txtDescripcion.TabIndex = 1;

            // Configuración del campo para escribir el nombre
            txtNombre.Location = new Point(15, 40);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(280, 27);
            txtNombre.TabIndex = 0;

            // Configuración general del control de categorías
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 254, 225);

            // Agrega los elementos principales al control
            Controls.Add(pnlFormulario);
            Controls.Add(dgvCategorias);
            Controls.Add(txtBuscar);
            Controls.Add(label1);

            Name = "UcCategorias";
            Size = new Size(1000, 650);

            // Asocia el evento Load con el método que configura el control
            Load += UcCategorias_Load;

            // Finaliza la configuración de la tabla y del panel
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Declaración de los controles utilizados en el módulo
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