
namespace GestorProductosAT
{
    // Clase parcial que contiene los elementos visuales del formulario de inicio de sesión.
    partial class FrmLogin
    {
        // Contenedor encargado de administrar los componentes utilizados por el formulario.
        private System.ComponentModel.IContainer components = null;

        // Método encargado de liberar los recursos utilizados por el formulario.
        protected override void Dispose(bool disposing)
        {
            // Comprueba si se deben liberar los recursos administrados.
            if (disposing && (components != null))
            {
                components.Dispose(); // Libera los componentes del formulario.
            }

            base.Dispose(disposing); // Ejecuta la limpieza correspondiente de la clase base.
        }

        // Región que contiene el código generado por el diseñador de Windows Forms.
        #region Windows Form Designer generated code

        // Método que inicializa y configura todos los elementos visuales del formulario.
        private void InitializeComponent()
        {
            // Permite obtener los recursos asociados al formulario, como imágenes e iconos.
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));

            // Crea las etiquetas, cajas de texto, botones y el control de imagen.
            label1 = new Label();
            txtUsuario = new TextBox();
            label2 = new Label();
            txtContrasena = new TextBox();
            btnEntrar = new Button();
            pictureBox1 = new PictureBox();
            btnSalirse = new Button();

            // Inicializa el control de imagen antes de configurarlo.
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();

            SuspendLayout(); // Suspende temporalmente el diseño mientras se configuran los controles.

            // Configuración de la etiqueta que identifica el campo de usuario.
            label1.AutoSize = true; // Ajusta automáticamente el tamaño al contenido.
            label1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold); // Establece la fuente y el estilo.
            label1.Location = new Point(45, 90); // Define la posición de la etiqueta.
            label1.Name = "label1"; // Asigna un nombre al control.
            label1.Size = new Size(80, 25); // Establece sus dimensiones.
            label1.TabIndex = 0; // Define su orden de navegación.
            label1.Text = "Usuario:"; // Establece el texto que se mostrará.
            label1.Click += label1_Click; // Asocia el evento que se ejecuta al hacer clic.

            // Configuración de la caja de texto donde se escribe el nombre de usuario.
            txtUsuario.Font = new Font("Segoe UI", 10.8F); // Establece la fuente del texto.
            txtUsuario.ForeColor = Color.FromArgb(36, 59, 83); // Define el color del texto.
            txtUsuario.Location = new Point(94, 139); // Establece su posición.
            txtUsuario.Name = "txtUsuario"; // Asigna el nombre del control.
            txtUsuario.Size = new Size(350, 31); // Define el ancho y alto.
            txtUsuario.TabIndex = 1; // Establece su orden de navegación.

            // Configuración de la etiqueta que identifica el campo de contraseña.
            label2.AutoSize = true; // Ajusta automáticamente el tamaño al contenido.
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold); // Establece la fuente y el estilo.
            label2.Location = new Point(45, 194); // Define su posición.
            label2.Name = "label2"; // Asigna un nombre al control.
            label2.Size = new Size(110, 25); // Establece sus dimensiones.
            label2.TabIndex = 2; // Define su orden de navegación.
            label2.Text = "Contraseña:"; // Establece el texto que se mostrará.

            // Configuración de la caja de texto para introducir la contraseña.
            txtContrasena.Font = new Font("Segoe UI", 10.8F); // Establece la fuente del texto.
            txtContrasena.ForeColor = Color.FromArgb(36, 59, 83); // Define el color del texto.
            txtContrasena.Location = new Point(94, 240); // Establece su posición.
            txtContrasena.Name = "txtContrasena"; // Asigna el nombre del control.
            txtContrasena.Size = new Size(350, 31); // Define sus dimensiones.
            txtContrasena.TabIndex = 3; // Establece su orden de navegación.
            txtContrasena.UseSystemPasswordChar = true; // Oculta los caracteres de la contraseña.

            // Configuración del botón para iniciar sesión.
            btnEntrar.BackColor = Color.FromArgb(36, 92, 69); // Establece el color de fondo.
            btnEntrar.Cursor = Cursors.Hand; // Cambia el cursor al pasar sobre el botón.
            btnEntrar.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0); // Define la fuente.
            btnEntrar.ForeColor = Color.White; // Establece el color del texto.
            btnEntrar.Location = new Point(307, 298); // Define la posición del botón.
            btnEntrar.Name = "btnEntrar"; // Asigna el nombre del control.
            btnEntrar.Size = new Size(137, 43); // Establece sus dimensiones.
            btnEntrar.TabIndex = 0; // Define su orden de navegación.
            btnEntrar.Text = "Iniciar sesión"; // Establece el texto del botón.
            btnEntrar.UseVisualStyleBackColor = false; // Permite utilizar el color de fondo personalizado.
            btnEntrar.Click += btnEntrar_Click; // Asocia el evento que ejecuta el inicio de sesión.

            // Configuración del control que muestra la imagen del formulario.
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image"); // Carga la imagen desde los recursos.
            pictureBox1.Location = new Point(259, -7); // Define la posición de la imagen.
            pictureBox1.Name = "pictureBox1"; // Asigna el nombre del control.
            pictureBox1.Size = new Size(298, 177); // Establece sus dimensiones.
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Ajusta la imagen al tamaño del control.
            pictureBox1.TabIndex = 4; // Define su orden de navegación.
            pictureBox1.TabStop = false; // Evita que reciba el foco mediante la tecla Tab.

            // Configuración del botón para cerrar el formulario.
            btnSalirse.BackColor = Color.SaddleBrown; // Establece el color de fondo.
            btnSalirse.Cursor = Cursors.Hand; // Cambia el cursor al pasar sobre el botón.
            btnSalirse.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0); // Define la fuente.
            btnSalirse.ForeColor = Color.White; // Establece el color del texto.
            btnSalirse.Location = new Point(407, 412); // Define su posición.
            btnSalirse.Name = "btnSalirse"; // Asigna el nombre del control.
            btnSalirse.Size = new Size(90, 40); // Establece sus dimensiones.
            btnSalirse.TabIndex = 5; // Define su orden de navegación.
            btnSalirse.Text = "Salir"; // Establece el texto del botón.
            btnSalirse.UseVisualStyleBackColor = false; // Permite utilizar el color personalizado.
            btnSalirse.Click += btnSalirse_Click; // Asocia el evento para salir del formulario.

            // Configuración general del formulario de inicio de sesión.
            AcceptButton = btnEntrar; // Permite iniciar sesión al presionar la tecla Enter.
            AutoScaleDimensions = new SizeF(8, 20); // Define las dimensiones de referencia para el escalado.
            AutoScaleMode = AutoScaleMode.Font; // Ajusta el diseño según las características de la fuente.
            BackColor = Color.Khaki; // Establece el color de fondo del formulario.
            ClientSize = new Size(540, 488); // Define el ancho y alto del área de trabajo.
            Controls.Add(btnSalirse); // Agrega el botón de salida al formulario.
            Controls.Add(btnEntrar); // Agrega el botón de inicio de sesión.
            Controls.Add(txtContrasena); // Agrega la caja de texto de contraseña.
            Controls.Add(label2); // Agrega la etiqueta de contraseña.
            Controls.Add(txtUsuario); // Agrega la caja de texto de usuario.
            Controls.Add(label1); // Agrega la etiqueta de usuario.
            Controls.Add(pictureBox1); // Agrega la imagen al formulario.
            FormBorderStyle = FormBorderStyle.FixedDialog; // Evita que el usuario cambie el tamaño de la ventana.
            Icon = (Icon)resources.GetObject("$this.Icon"); // Asigna el icono del formulario.
            MaximizeBox = false; // Deshabilita el botón para maximizar la ventana.
            Name = "FrmLogin"; // Asigna el nombre del formulario.
            StartPosition = FormStartPosition.CenterScreen; // Centra la ventana al abrirse.
            Text = "Gestor de Productos AT"; // Establece el título de la ventana.

            // Finaliza la inicialización del control de imagen.
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();

            ResumeLayout(false); // Reactiva el diseño del formulario.
            PerformLayout(); // Aplica los cambios pendientes de distribución y tamaño.
        }

        #endregion

        // Declaración de los controles utilizados en el formulario.
        private Label label1;
        private TextBox txtUsuario;
        private Label label2;
        private TextBox txtContrasena;
        private Button btnEntrar;
        private PictureBox pictureBox1;
        private Button btnSalirse;
    }
}
