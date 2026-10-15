namespace GestorProductosAT
{
    public partial class Form1 : Form
    {
        // Colores utilizados para los botones del menú.
        private readonly Color VerdeOscuro = Color.FromArgb(31, 92, 69);
        private readonly Color VerdeActivo = Color.FromArgb(46, 125, 94);

        private Button? botonActivo; // Guarda el botón que se encuentra seleccionado.

        public bool CerrarSesionSolicitado { get; private set; } // Indica si el usuario solicitó cerrar sesión.

        public Form1()
        {
            InitializeComponent();
        }
        // Se ejecuta cuando se carga el formulario principal.
        private void Form1_Load(object sender, EventArgs e) {   
            
         // Muestra el nombre y el rol del usuario que inició sesión.
            lblBienvenida.Text = $"Bienvenido, {Sesion.Nombre}    ({Sesion.Rol})";
            bool esAdmin = Sesion.Rol == "Administrador"; // Comprueba si el usuario tiene el rol de administrador.

            // Define las opciones del menú y si son exclusivas del administrador.
            var opciones = new (string texto, bool soloAdmin)[]
            {
                ("Inicio", false),
                ("Productos", true),
                ("Categorías", true),
                ("Proveedores", true),
                ("Punto de Venta", false),
                ("Entradas y Salidas", false),
                ("Usuarios", true),
                ("Reportes", true)
            };

            int y = 20; // Establece la posición inicial de los botones.
            Button? primero = null; // Guarda el primer botón para seleccionarlo al abrir el formulario.


            // Recorre todas las opciones para crear los botones del menú.
            foreach (var (texto, soloAdmin) in opciones)
            {
                if (soloAdmin && !esAdmin) continue;  // Si la opción es exclusiva del administrador, la oculta al cajero.

                var btn = CrearBotonMenu(texto); // Crea el botón correspondiente a la opción actual.
                btn.Location = new Point(0, y);  // Establece la posición del botón dentro del menú.
                btn.Click += (s, ev) =>  // Define las acciones que se realizan al pulsar el botón.
                {
                    Activar(btn);
                    MostrarPantalla(texto);
                };
                pnlMenu.Controls.Add(btn);  // Agrega el botón al panel del menú.
                primero ??= btn;  // Guarda el primer botón creado.
                y += 50; // Desplaza la posición para el siguiente botón.
            }
            // Crea el botón para cerrar la sesión del usuario.
            var btnCerrarSesion = CrearBotonMenu("Cerrar sesión"); 
            btnCerrarSesion.Dock = DockStyle.Bottom;   // Coloca el botón en la parte inferior del menú. 
            btnCerrarSesion.Click += (s, ev) => // Define las acciones para cerrar la sesión.
            {
                // Limpia los datos del usuario actual.
                Sesion.Nombre = ""; 
                Sesion.Rol = ""; 

                CerrarSesionSolicitado = true; // Indica que se solicitó cerrar la sesión.
                Close(); // Cierra el formulario actual.
            };
            pnlMenu.Controls.Add(btnCerrarSesion); // Agrega el botón de cerrar sesión al menú.

            var btnSalir = CrearBotonMenu("Salir"); // Crea el botón para salir del formulario.
            btnSalir.Dock = DockStyle.Bottom; // Coloca el botón en la parte inferior del menú.
            btnSalir.Click += (s, ev) => Close(); // Cierra el formulario cuando se pulsa el botón.
            pnlMenu.Controls.Add(btnSalir);  // Agrega el botón de salida al menú.

            primero?.PerformClick(); // Selecciona automáticamente el primer botón al abrir el formulario.
        }

        private Button CrearBotonMenu(string texto)  // Crea un botón con el diseño establecido para el menú.
        {
            var btn = new Button
            {
                // Configura las propiedades visuales del botón.
                Text = "  " + texto,
                Size = new Size(pnlMenu.Width, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = VerdeOscuro,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0; // Elimina el borde del botón.
            return btn; // Devuelve el botón creado.
        }

        // Cambia el color del botón seleccionado para identificarlo visualmente.
        private void Activar(Button btn)
        {
            // Devuelve al color original el botón que estaba seleccionado.
            if (botonActivo != null) botonActivo.BackColor = VerdeOscuro;
            botonActivo = btn; //Guarda el boton que se acaba de seleccionar.
            btn.BackColor = VerdeActivo; //Cambia el color del botón seleccionado.
        }

        // Muestra la pantalla correspondiente a la opción seleccionada.
        private void MostrarPantalla(string titulo)
        {
            pnlContenido.Controls.Clear(); // Elimina los controles de la pantalla anterior.

            // Selecciona qué pantalla se debe mostrar según el título.
            Control pantalla = titulo switch 
            {
                "Productos" => new UcProductos(),
                "Categorías" => new UcCategorias(),
                "Proveedores" => new UcProveedores(),
                _ => new Label
                {
                    // Mostramos lo que no hemos hecho xd buena esa gang
                    Text = titulo + "\n(en construcción",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font ("Segoe UI", 20)
                }
            };

            pantalla.Dock = DockStyle.Fill; // Hace que la pantalla ocupe todo el espacio disponible.
            pnlContenido.Controls.Add(pantalla); // Agrega la nueva pantalla al panel de contenido.
        }
    }
}
