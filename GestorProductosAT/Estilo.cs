
using System.Drawing.Drawing2D; // Permite trabajar con formas y gráficos personalizados.

namespace GestorProductosAT
{
    // Clase encargada de controlar el diseño visual de los elementos del programa.
    public static class Estilo
    {
        // Define la paleta de colores que se utilizará en todo el programa.
        public static readonly Color Verde = Color.FromArgb(31, 92, 69);
        public static readonly Color VerdeClaro = Color.FromArgb(46, 125, 94);
        public static readonly Color Crema = Color.FromArgb(255, 253, 225);
        public static readonly Color Amarillo = Color.FromArgb(255, 226, 130);
        public static readonly Color Peligro = Color.FromArgb(192, 57, 43);

        // Recorre los controles de un formulario o panel y les aplica el estilo correspondiente.
        public static void Aplicar(Control raiz)
        {
            foreach (Control hijo in raiz.Controls)
            {
                // Identifica el tipo de control para aplicar su diseño correspondiente.
                switch (hijo)
                {
                    case DataGridView dgv:
                        Tabla(dgv); // Aplica el diseño personalizado a las tablas.
                        break;

                    case Button boton:
                        Boton(boton, ColorBoton(boton.Name)); // Personaliza el botón según su nombre.
                        break;

                    case Panel panel:
                        // Comprueba si el panel está acoplado a otro elemento.
                        if (panel.Dock == DockStyle.None) Tarjeta(panel);
                        else panel.BackColor = Color.White;
                        break;
                }

                // Si el control contiene otros elementos, también les aplica el estilo.
                if (hijo.HasChildren) Aplicar(hijo);
            }
        }

        // Determina el color de un botón dependiendo de su nombre.
        private static Color ColorBoton(string nombre)
        {
            // Utiliza el color rojo para los botones relacionados con eliminar o quitar.
            if (nombre.Contains("Eliminar") || nombre.Contains("Quitar")) return Peligro;

            // Utiliza un color café para los botones de salida.
            if (nombre.Contains("Salir")) return Color.SaddleBrown;

            // Utiliza el color amarillo para los botones de nuevo, limpiar, vaciar o imprimir.
            if (nombre.Contains("Nuevo") || nombre.Contains("Limpiar") ||
                nombre.Contains("Vaciar") || nombre.Contains("Imprimir")) return Amarillo;

            // Si no coincide con ninguna condición anterior, utiliza el color verde claro.
            return VerdeClaro;
        }

        // Personaliza la apariencia de una tabla de datos.
        public static void Tabla(DataGridView dgv)
        {
            dgv.BackgroundColor = Crema; // Establece el color de fondo de la tabla.
            dgv.BorderStyle = BorderStyle.None; // Elimina el borde exterior.
            dgv.EnableHeadersVisualStyles = false; // Permite modificar manualmente el diseño de los encabezados.

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Elimina los bordes de los encabezados.
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Evita modificar su altura.
            dgv.ColumnHeadersHeight = 36; // Establece la altura de los encabezados.
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Verde; // Cambia el fondo de los encabezados.
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Establece el color del texto.
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Verde; // Mantiene el color al seleccionar un encabezado.
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); // Define la fuente y su tamaño.
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 0, 0); // Agrega espacio interno al texto.

            dgv.DefaultCellStyle.SelectionBackColor = VerdeClaro; // Cambia el fondo de las celdas seleccionadas.
            dgv.DefaultCellStyle.SelectionForeColor = Color.White; // Cambia el color del texto seleccionado.
            dgv.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0); // Agrega espacio interno a las celdas.
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 245); // Alterna el color de las filas.
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Muestra únicamente bordes horizontales.
            dgv.GridColor = Color.FromArgb(220, 230, 220); // Define el color de las líneas de la tabla.
            dgv.RowTemplate.Height = 30; // Establece la altura de las filas.
        }

        // Personaliza la apariencia de un botón utilizando el color recibido.
        public static void Boton(Button b, Color fondo)
        {
            b.FlatStyle = FlatStyle.Flat; // Establece un diseño plano para el botón.
            b.FlatAppearance.BorderSize = 0; // Elimina el borde del botón.
            b.BackColor = fondo; // Asigna el color de fondo.

            // Cambia el color del botón cuando el cursor del mouse pasa sobre él.
            b.FlatAppearance.MouseOverBackColor =
                Mezclar(fondo, Color.White, 0.2);

            // Cambia el color del botón mientras se mantiene presionado.
            b.FlatAppearance.MouseDownBackColor =
                Mezclar(fondo, Color.Black, 0.15);

            // Selecciona el color del texto dependiendo del brillo del fondo.
            b.ForeColor = fondo.GetBrightness() > 0.7f
                ? Verde : Color.White;

            b.Cursor = Cursors.Hand; // Cambia el cursor para indicar que el botón es interactivo.

            // Si el texto no está en negritas, modifica su fuente para resaltarlo.
            if (!b.Font.Bold)
                b.Font = new Font(b.Font, FontStyle.Bold);

            Redondear(b, 10); // Aplica esquinas redondeadas al botón.
        }

        // Personaliza un panel para que tenga apariencia de tarjeta.
        public static void Tarjeta(Panel p)
        {
            p.BackColor = Color.White; // Establece el fondo blanco.
            Redondear(p, 14); // Redondea las esquinas del panel.
        }

        // Modifica la forma de un control para darle esquinas redondeadas.
        public static void Redondear(Control c, int radio)
        {
            // Método interno que calcula y aplica la forma del control.
            void Aplicar()
            {
                // Comprueba que el control tenga dimensiones válidas.
                if (c.Width <= 0 || c.Height <= 0) return;

                // Calcula el radio de las esquinas sin superar la mitad del tamaño del control.
                int r = Math.Min(radio, Math.Min(c.Width, c.Height) / 2);
                int d = r * 2;

                using var ruta = new GraphicsPath(); // Crea una ruta para dibujar la forma personalizada.

                // Dibuja los cuatro arcos que forman las esquinas redondeadas.
                ruta.AddArc(0, 0, d, d, 180, 90);
                ruta.AddArc(c.Width - d, 0, d, d, 270, 90);
                ruta.AddArc(c.Width - d, c.Height - d, d, d, 0, 90);
                ruta.AddArc(0, c.Height - d, d, d, 90, 90);

                ruta.CloseFigure(); // Une los arcos para cerrar la figura.

                c.Region = new Region(ruta); // Aplica la forma creada al control.
            }

            // Activa el doble búfer para mejorar la calidad del dibujo y reducir parpadeos.
            typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance
            )?.SetValue(c, true, null);

            Aplicar(); // Aplica inicialmente las esquinas redondeadas.

            // Vuelve a calcular la forma cuando cambia el tamaño del control.
            c.SizeChanged += (s, e) => Aplicar();
        }

        // Combina dos colores para obtener un color intermedio.
        // El valor t determina qué tanto se acerca el resultado a cada color.
        private static Color Mezclar(Color a, Color b, double t) =>
            Color.FromArgb(
                (int)(a.R + (b.R - a.R) * t), // Calcula el componente rojo.
                (int)(a.G + (b.G - a.G) * t), // Calcula el componente verde.
                (int)(a.B + (b.B - a.B) * t)); // Calcula el componente azul.
    }
}
