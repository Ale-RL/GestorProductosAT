namespace GestorProductosAT
{
    public partial class Form1 : Form
    {

        private readonly Color VerdeOscuro = Color.FromArgb(31, 92, 69);
        private readonly Color VerdeActivo = Color.FromArgb(46, 125, 94);
        private Button? botonActivo;
        public bool CerrarSesionSolicitado { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = $"Bienvenido, {Sesion.Nombre}    ({Sesion.Rol})";
            bool esAdmin = Sesion.Rol == "Administrador";

            // Texto del boton ¿Solo para administrador?
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

            int y = 20;
            Button? primero = null;

            foreach (var (texto, soloAdmin) in opciones)
            {
                if (soloAdmin && !esAdmin) continue; // el cajero no ve esta opción

                var btn = CrearBotonMenu(texto);
                btn.Location = new Point(0, y);
                btn.Click += (s, ev) =>
                {
                    Activar(btn);
                    MostrarPantalla(texto);
                };
                pnlMenu.Controls.Add(btn);
                primero ??= btn;
                y += 50;
            }
            // Botón de cerrar sesión y salir
            var btnCerrarSesion = CrearBotonMenu("Cerrar sesión");
            btnCerrarSesion.Dock = DockStyle.Bottom;
            btnCerrarSesion.Click += (s, ev) =>
            {
                Sesion.Nombre = "";
                Sesion.Rol = "";
                CerrarSesionSolicitado = true;
                Close();
            };
            pnlMenu.Controls.Add(btnCerrarSesion);

            var btnSalir = CrearBotonMenu("Salir");
            btnSalir.Dock = DockStyle.Bottom;
            btnSalir.Click += (s, ev) => Close();
            pnlMenu.Controls.Add(btnSalir);

            primero?.PerformClick(); // al abrir, muestra Inicio
        }

        private Button CrearBotonMenu(string texto)
        {
            var btn = new Button
            {
                Text = "  " + texto,
                Size = new Size(pnlMenu.Width, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = VerdeOscuro,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        //Marca visualmente el botón de la pantalla actual
        private void Activar(Button btn)
        {
            if (botonActivo != null) botonActivo.BackColor = VerdeOscuro;
            botonActivo = btn;
            btn.BackColor = VerdeActivo;
        }

        //Por ahora muestra un texto temporar. Luego aqui cargaremos cada pantalla
        private void MostrarPantalla(string titulo)
        {
            pnlContenido.Controls.Clear();

            Control pantalla = titulo switch
            {
                "Productos" => new UcProductos(),
                "Categorías" => new UcCategorias(),
                _ => new Label
                {
                    Text = titulo + "\n(en construcción",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font ("Segoe UI", 20)
                }
            };

            pantalla.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(pantalla);
        }
    }
}
