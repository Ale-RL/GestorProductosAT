using System.Drawing.Drawing2D;

namespace GestorProductosAT
{
    public static class Estilo
    {
        // Paleta de colores de todo el programa
        public static readonly Color Verde = Color.FromArgb(31, 92, 69);
        public static readonly Color VerdeClaro = Color.FromArgb(46, 125, 94);
        public static readonly Color Crema = Color.FromArgb(255, 253, 225);
        public static readonly Color Amarillo = Color.FromArgb(255, 226, 130);
        public static readonly Color Peligro = Color.FromArgb(192, 57, 43);

        // Recorre los controles y aplica el estilo según el tipo y nombre del control
        public static void Aplicar(Control raiz)
        {
            foreach (Control hijo in raiz.Controls)
            {
                switch (hijo)
                {
                    case DataGridView dgv:
                        Tabla(dgv);
                        break;
                    case Button boton:
                        Boton(boton, ColorBoton(boton.Name));
                        break;
                    case Panel panel:
                        // Los paneles acoplados a un lado (formularios) quedan blancos,
                        // los sueltos (tarjetas de Inicio) además se redondean
                        if (panel.Dock == DockStyle.None) Tarjeta(panel);
                        else panel.BackColor = Color.White;
                        break;
                }

                if (hijo.HasChildren) Aplicar(hijo); // también los controles que están dentro de paneles
            }
        }

        // El color del botón se decide por su nombre
        private static Color ColorBoton(string nombre)
        {
            if (nombre.Contains("Eliminar") || nombre.Contains("Quitar")) return Peligro;
            if (nombre.Contains("Salir")) return Color.SaddleBrown;
            if (nombre.Contains("Nuevo") || nombre.Contains("Limpiar") ||
                nombre.Contains("Vaciar") || nombre.Contains("Imprimir")) return Amarillo;
            return VerdeClaro; // Guardar, Cobrar, Generar, Agregar, Entrar...
        }

        public static void Tabla(DataGridView dgv)
        {
            dgv.BackgroundColor = Crema; // bye bye gris emo
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false; // sin esto el color del encabezado se ignora

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Verde;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Verde; // evita el azul al hacer clic
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 0, 0);

            dgv.DefaultCellStyle.SelectionBackColor = VerdeClaro;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 245);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(220, 230, 220);
            dgv.RowTemplate.Height = 30;
        }

        public static void Boton(Button b, Color fondo)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = fondo;

            // Cambia dependiendo del cursor del mouse, para dar un efecto de "resaltar" y "hundir"
            b.FlatAppearance.MouseOverBackColor =
                Mezclar(fondo, Color.White, 0.2);

            b.FlatAppearance.MouseDownBackColor =
                Mezclar(fondo, Color.Black, 0.15);

            b.ForeColor = fondo.GetBrightness() > 0.7f
                ? Verde : Color.White;

            b.Cursor = Cursors.Hand;

            if (!b.Font.Bold)
                b.Font = new Font(b.Font, FontStyle.Bold);

            Redondear(b, 10);
        }

        public static void Tarjeta(Panel p)
        {
            p.BackColor = Color.White;
            Redondear(p, 14);
        }

        // Esquinas redondeadas, se recorta la forma del control y la recalcula si cambia de tamaño
        public static void Redondear(Control c, int radio)
        {
            void Aplicar()
            {
                if (c.Width <= 0 || c.Height <= 0) return;

                int r = Math.Min(radio, Math.Min(c.Width, c.Height) / 2);
                int d = r * 2;

                using var ruta = new GraphicsPath();

                ruta.AddArc(0, 0, d, d, 180, 90);
                ruta.AddArc(c.Width - d, 0, d, d, 270, 90);
                ruta.AddArc(c.Width - d, c.Height - d, d, d, 0, 90);
                ruta.AddArc(0, c.Height - d, d, d, 90, 90);

                ruta.CloseFigure();

                c.Region = new Region(ruta);
            }

            // Activar doble búfer para mejorar el dibujo
            typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance
            )?.SetValue(c, true, null);

            // Redibujar cuando cambie el tamaño
            Aplicar();
            c.SizeChanged += (s, e) => Aplicar();
        }

        // Un color intermedio entre dos colores (t = 0 es "a", t = 1 es "b")
        private static Color Mezclar(Color a, Color b, double t) =>
            Color.FromArgb(
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
    }
}