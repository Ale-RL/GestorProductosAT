
using System.Data;
using System.Drawing.Printing;
using System.Globalization;

namespace GestorProductosAT
{
    public partial class UcReportes : UserControl
    {
        // Guarda el periodo de fechas seleccionado para mostrarlo al imprimir.
        private string periodo = "";

        public UcReportes()
        {
            InitializeComponent();

            // Conecta los eventos de los controles con sus respectivos métodos.
            Load += UcReportes_Load;
            btnGenerar.Click += (s, e) => Generar();
            btnImprimir.Click += (s, e) => Imprimir();

            // Comprueba si el reporte seleccionado necesita un rango de fechas.
            cmbReporte.SelectedIndexChanged += (s, e) =>
            {
                bool usaFechas = cmbReporte.Text != "Inventario";

                // Deshabilita las fechas cuando se consulta el inventario.
                dtpDesde.Enabled = usaFechas;
                dtpHasta.Enabled = usaFechas;
            };
        }

        private void UcReportes_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar este código mientras se diseña la interfaz.
            if (DesignMode) return;

            Estilo.Aplicar(this);

            // Configura la tabla para mostrar los resultados de los reportes.
            dgvReporte.ReadOnly = true;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.MultiSelect = false;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Agrega los diferentes tipos de reportes disponibles.
            cmbReporte.Items.Clear();
            cmbReporte.Items.Add("Ventas");
            cmbReporte.Items.Add("Productos más vendidos");
            cmbReporte.Items.Add("Corte de caja");
            cmbReporte.Items.Add("Inventario");
            cmbReporte.SelectedIndex = 0;

            // Establece la fecha actual como inicio y final del periodo.
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            // Genera el primer reporte para que la pantalla no aparezca vacía.
            Generar();
        }

        private void Generar()
        {
            // Obtiene el tipo de reporte seleccionado.
            string reporte = cmbReporte.Text;

            // Comprueba que la fecha inicial no sea posterior a la fecha final.
            if (reporte != "Inventario" && dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha \"Desde\" no puede ser mayor que \"Hasta\".");
                return;
            }

            // Convierte las fechas a un formato que coincide con el utilizado en la base de datos.
            string desde = dtpDesde.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string hasta = dtpHasta.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Variables que almacenan la consulta y la información del total.
            string sql;
            string columnaTotal;
            string textoTotal;
            string[] monedas;

            // Selecciona la consulta dependiendo del reporte elegido.
            switch (reporte)
            {
                case "Ventas":

                    // Muestra un registro por cada venta y suma los productos de su ticket.
                    sql = @"
                        SELECT i.id_venta AS Ticket, i.fecha AS Fecha, i.cajero AS Cajero,
                               i.metodo_pago AS Pago, SUM(v.subtotal) AS Total
                        FROM InformacionVenta i
                        JOIN Venta v ON v.id_venta = i.id_venta
                        WHERE date(i.fecha) BETWEEN @d AND @h
                        GROUP BY i.id_venta
                        ORDER BY i.id_venta DESC";

                    columnaTotal = "Total";
                    textoTotal = "Total vendido";
                    monedas = new[] { "Total" };
                    break;

                case "Productos más vendidos":

                    // Agrupa los productos repetidos y suma sus unidades vendidas e importes.
                    sql = @"
                        SELECT p.nombre AS Producto, SUM(v.cantidad) AS Unidades, SUM(v.subtotal) AS Importe
                        FROM Venta v
                        JOIN InformacionVenta i ON i.id_venta = v.id_venta
                        JOIN Producto p ON p.codigo_barras = v.codigo_barras
                        WHERE date(i.fecha) BETWEEN @d AND @h
                        GROUP BY p.codigo_barras
                        ORDER BY Unidades DESC";

                    columnaTotal = "Importe";
                    textoTotal = "Total vendido";
                    monedas = new[] { "Importe" };
                    break;

                case "Corte de caja":

                    // Agrupa las ventas por método de pago y calcula cuánto se vendió con cada uno.
                    sql = @"
                        SELECT i.metodo_pago AS [Método], COUNT(DISTINCT i.id_venta) AS Ventas,
                               SUM(v.subtotal) AS Total
                        FROM InformacionVenta i
                        JOIN Venta v ON v.id_venta = i.id_venta
                        WHERE date(i.fecha) BETWEEN @d AND @h
                        GROUP BY i.metodo_pago
                        ORDER BY [Método]";

                    columnaTotal = "Total";
                    textoTotal = "Total del corte";
                    monedas = new[] { "Total" };
                    break;

                case "Inventario":

                    // Muestra el stock actual y calcula el valor de la mercancía
                    // multiplicando las existencias por el precio de compra.
                    sql = @"
                        SELECT p.nombre AS Producto, c.nombre AS [Categoría], p.stock AS Stock,
                               p.precio_compra AS [Precio compra], p.stock * p.precio_compra AS Valor
                        FROM Producto p
                        LEFT JOIN Categoria c ON c.id_categoria = p.id_categoria
                        ORDER BY p.nombre";

                    columnaTotal = "Valor";
                    textoTotal = "Valor del inventario";
                    monedas = new[] { "Precio compra", "Valor" };
                    break;

                default:
                    // Muestra un aviso si no se seleccionó un reporte válido.
                    MessageBox.Show("Elige un reporte.");
                    return;
            }

            // El reporte de inventario no necesita fechas.
            // Los demás reportes utilizan el periodo seleccionado.
            var tabla = reporte == "Inventario"
                ? BaseDatos.Consultar(sql)
                : BaseDatos.Consultar(sql, ("@d", desde), ("@h", hasta));

            // Limpia la tabla anterior y muestra los resultados nuevos.
            dgvReporte.DataSource = null;
            dgvReporte.Columns.Clear();
            dgvReporte.DataSource = tabla;

            // Aplica formato de moneda a las columnas correspondientes.
            foreach (string columna in monedas)
                dgvReporte.Columns[columna]!.DefaultCellStyle.Format = "C2";

            // Suma los valores de la columna correspondiente para obtener el total general.
            // Si una celda está vacía, se considera como cero.
            double total = tabla.Rows.Cast<DataRow>()
                .Sum(r => r.IsNull(columnaTotal) ? 0 : Convert.ToDouble(r[columnaTotal]));

            // Muestra el resultado de la suma en la etiqueta inferior.
            lblTotal.Text = textoTotal + ": " + total.ToString("C2");

            // Guarda el periodo utilizado para incluirlo posteriormente en el ticket impreso.
            periodo = reporte == "Inventario"
                ? "Al " + DateTime.Today.ToString("dd/MM/yyyy")
                : "Del " + dtpDesde.Value.ToString("dd/MM/yyyy") + " al " + dtpHasta.Value.ToString("dd/MM/yyyy");
        }

        private void Imprimir()
        {
            // Comprueba que existan resultados antes de intentar imprimir.
            if (dgvReporte.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para imprimir. Genera un reporte primero.");
                return;
            }

            // Crea una lista con el encabezado y la información del reporte.
            var lineas = new List<string>
            {
                "GESTOR DE PRODUCTOS AT",
                cmbReporte.Text + " (" + periodo + ")",
                new string('-', 60),

                // Agrega los nombres de las columnas de la tabla.
                string.Join(" | ", dgvReporte.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText))
            };

            // Recorre las filas de la tabla y agrega sus datos al documento.
            foreach (DataGridViewRow fila in dgvReporte.Rows)
                lineas.Add(string.Join(" | ", fila.Cells.Cast<DataGridViewCell>().Select(c => c.FormattedValue?.ToString())));

            // Agrega una separación y el total general al final del reporte.
            lineas.Add(new string('-', 60));
            lineas.Add(lblTotal.Text);

            // Guarda la posición de la siguiente línea que se imprimirá.
            int siguiente = 0;

            // Crea el documento que se utilizará para imprimir el reporte.
            var doc = new PrintDocument();

            // Reinicia la posición cuando comienza una nueva impresión o vista previa.
            doc.BeginPrint += (s, e) => siguiente = 0;

            // Define cómo se dibujará el contenido en cada página.
            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics!;

                // Establece la fuente, el espacio entre líneas y la posición inicial.
                using var fuente = new Font("Consolas", 10);
                float alto = fuente.GetHeight(g) + 4;
                float y = e.MarginBounds.Top;

                // Dibuja las líneas mientras exista espacio en la página.
                while (siguiente < lineas.Count && y + alto < e.MarginBounds.Bottom)
                {
                    g.DrawString(lineas[siguiente], fuente, Brushes.Black, e.MarginBounds.Left, y);
                    y += alto;
                    siguiente++;
                }

                // Indica si todavía quedan líneas que deben imprimirse en otra página.
                e.HasMorePages = siguiente < lineas.Count;
            };

            // Abre la vista previa del documento antes de imprimirlo.
            using var vista = new PrintPreviewDialog
            {
                Document = doc,
                WindowState = FormWindowState.Maximized
            };

            vista.ShowDialog();
        }
    }
}
