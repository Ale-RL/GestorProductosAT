using System.Data;
using System.Drawing.Printing;
using System.Globalization;

namespace GestorProductosAT
{
    public partial class UcReportes : UserControl
    {
        private string periodo = ""; // texto del rango de fechas, se usa al imprimir

        public UcReportes()
        {
            InitializeComponent();
            Load += UcReportes_Load;
            btnGenerar.Click += (s, e) => Generar();
            btnImprimir.Click += (s, e) => Imprimir();

            cmbReporte.SelectedIndexChanged += (s, e) =>
            {
                bool usaFechas = cmbReporte.Text != "Inventario";
                dtpDesde.Enabled = usaFechas;
                dtpHasta.Enabled = usaFechas;
            };
        }

        private void UcReportes_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;
            Estilo.Aplicar(this);

            dgvReporte.ReadOnly = true;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.MultiSelect = false;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cmbReporte.Items.Clear();
            cmbReporte.Items.Add("Ventas");
            cmbReporte.Items.Add("Productos más vendidos");
            cmbReporte.Items.Add("Corte de caja");
            cmbReporte.Items.Add("Inventario");
            cmbReporte.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            Generar(); // para que la pantalla no aparezca vacía
        }

        private void Generar()
        {
            string reporte = cmbReporte.Text;

            if (reporte != "Inventario" && dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha \"Desde\" no puede ser mayor que \"Hasta\".");
                return;
            }

            // Las fechas se mandan como texto "2026-11-15", igual que se guardan en la base
            string desde = dtpDesde.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string hasta = dtpHasta.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            string sql;            // la consulta
            string columnaTotal;   // qué columna se suma para el total de abajo
            string textoTotal;     // cómo se llama ese total
            string[] monedas;      // columnas que se muestran como dinero

            switch (reporte)
            {
                case "Ventas":
                    // Un renglón por ticket, SUM suma los productos de cada ticket
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
                    // GROUP BY junta todas las ventas del mismo producto
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
                    // Cuanto entro por cada método de pago (el efectivo es lo que debe haber en caja)
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
                    // Stock actual y cuánto dinero hay en mercancía (stock x precio de compra)
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
                    MessageBox.Show("Elige un reporte.");
                    return;
            }

            // El inventario no usa fechas, así que no se le mandan
            var tabla = reporte == "Inventario"
                ? BaseDatos.Consultar(sql)
                : BaseDatos.Consultar(sql, ("@d", desde), ("@h", hasta));

            dgvReporte.DataSource = null;
            dgvReporte.Columns.Clear(); // evita que se queden columnas del reporte anterior
            dgvReporte.DataSource = tabla;

            foreach (string columna in monedas)
                dgvReporte.Columns[columna]!.DefaultCellStyle.Format = "C2";

            // Si no hay filas, la suma da 0; si una celda viene vacía, cuenta como 0 (no falla)
            double total = tabla.Rows.Cast<DataRow>()
                .Sum(r => r.IsNull(columnaTotal) ? 0 : Convert.ToDouble(r[columnaTotal]));
            lblTotal.Text = textoTotal + ": " + total.ToString("C2");

            periodo = reporte == "Inventario"
                ? "Al " + DateTime.Today.ToString("dd/MM/yyyy")
                : "Del " + dtpDesde.Value.ToString("dd/MM/yyyy") + " al " + dtpHasta.Value.ToString("dd/MM/yyyy");
        }

        // Imprime lo que se ve en la tabla, en texto sencillo, con vista previa
        private void Imprimir()
        {
            if (dgvReporte.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para imprimir. Genera un reporte primero.");
                return;
            }

            var lineas = new List<string>
            {
                "GESTOR DE PRODUCTOS AT",
                cmbReporte.Text + " (" + periodo + ")",
                new string('-', 60),
                string.Join(" | ", dgvReporte.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText))
            };

            foreach (DataGridViewRow fila in dgvReporte.Rows)
                lineas.Add(string.Join(" | ", fila.Cells.Cast<DataGridViewCell>().Select(c => c.FormattedValue?.ToString())));

            lineas.Add(new string('-', 60));
            lineas.Add(lblTotal.Text);

            int siguiente = 0; // línea por la que va la impresión (para pasar a la siguiente hoja)

            var doc = new PrintDocument();
            doc.BeginPrint += (s, e) => siguiente = 0; // la vista previa vuelve a empezar desde cero
            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics!;
                using var fuente = new Font("Consolas", 10);
                float alto = fuente.GetHeight(g) + 4;
                float y = e.MarginBounds.Top;

                while (siguiente < lineas.Count && y + alto < e.MarginBounds.Bottom)
                {
                    g.DrawString(lineas[siguiente], fuente, Brushes.Black, e.MarginBounds.Left, y);
                    y += alto;
                    siguiente++;
                }

                e.HasMorePages = siguiente < lineas.Count;
            };

            using var vista = new PrintPreviewDialog
            {
                Document = doc,
                WindowState = FormWindowState.Maximized
            };
            vista.ShowDialog();
        }
    }
}