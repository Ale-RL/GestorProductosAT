using System.Data;

namespace GestorProductosAT
{
    public partial class UcInicio : UserControl
    {
        private const int StockBajo = 10; // el mismo límite que usa UcProductos

        public UcInicio()
        {
            InitializeComponent();
            Load += UcInicio_Load;
            Resize += (s, e) => Acomodar(); // cada vez que cambia el tamaño de la ventana
        }

        private void UcInicio_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            ConfigurarTabla(dgvRecientes);
            ConfigurarTabla(dgvMasVendidos);
            ConfigurarTabla(dgvPorAgotarse);

            Acomodar();
            CargarTarjetas();
            CargarRecientes();
            CargarMasVendidos();
            CargarPorAgotarse();
        }

        private static void ConfigurarTabla(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Acomodar()
        {
            if (DesignMode) return;

            int margen = 15;
            int separacion = 15;
            int ancho = ClientSize.Width - margen * 2;
            if (ancho < 600) return; // ventana demasiado chica, se deja como está

            // Tarjetas
            Panel[] tarjetas = { pnlProductos, pnlStockBajo, pnlVentas, pnlSemana };
            int anchoTarjeta = (ancho - separacion * 3) / 4;
            for (int i = 0; i < tarjetas.Length; i++)
            {
                tarjetas[i].Left = margen + i * (anchoTarjeta + separacion);
                tarjetas[i].Width = anchoTarjeta;
            }

            // Zona de tablas
            int izquierda = (ancho - separacion) * 55 / 100;
            int derecha = ancho - izquierda - separacion;
            int xDerecha = margen + izquierda + separacion;

            int alto = ClientSize.Height - dgvRecientes.Top - margen;
            if (alto < 200) alto = 200;

            dgvRecientes.Width = izquierda;
            dgvRecientes.Height = alto;

            int altoPrimera = (alto - 45) / 2;
            int altoSegunda = alto - 45 - altoPrimera;

            lblMasVendidos.Left = xDerecha;
            dgvMasVendidos.SetBounds(xDerecha, dgvRecientes.Top, derecha, altoPrimera);

            lblPorAgotarse.Location = new Point(xDerecha, dgvRecientes.Top + altoPrimera + 15);
            dgvPorAgotarse.SetBounds(xDerecha, lblPorAgotarse.Top + 30, derecha, altoSegunda);
        }

        private void CargarTarjetas()
        {
            var total = BaseDatos.Consultar("SELECT COUNT(*) FROM Producto");
            lblProductosValor.Text = Convert.ToInt32(total.Rows[0][0]).ToString();

            var bajo = BaseDatos.Consultar(
                "SELECT COUNT(*) FROM Producto WHERE stock <= @limite", ("@limite", StockBajo));
            lblStockBajoValor.Text = Convert.ToInt32(bajo.Rows[0][0]).ToString();

            // COALESCE(..., 0): si no hay ventas, SUM da vacío; así se convierte en 0
            // date(fecha) se queda solo con el día; date('now','localtime') es el día de hoy
            var ventas = BaseDatos.Consultar(@"
                SELECT COALESCE(SUM(v.subtotal), 0)
                FROM Venta v
                JOIN InformacionVenta i ON i.id_venta = v.id_venta
                WHERE date(i.fecha) = date('now', 'localtime')");
            lblVentasValor.Text = Convert.ToDouble(ventas.Rows[0][0]).ToString("C2");

            var semana = BaseDatos.Consultar(@"
                SELECT COALESCE(SUM(v.subtotal), 0)
                FROM Venta v
                JOIN InformacionVenta i ON i.id_venta = v.id_venta
                WHERE date(i.fecha) >= date('now', 'localtime', '-6 days')");
            lblSemanaValor.Text = Convert.ToDouble(semana.Rows[0][0]).ToString("C2");
        }

        private void CargarRecientes()
        {
            dgvRecientes.DataSource = BaseDatos.Consultar(@"
                SELECT m.fecha AS Fecha, p.nombre AS Producto, m.tipo_movimiento AS Tipo,
                       m.cantidad AS Cantidad, m.inventario AS [Stock resultante],
                       CASE WHEN m.id_venta IS NULL THEN 'Manual'
                            ELSE 'Venta #' || m.id_venta END AS Origen
                FROM Movimiento m
                LEFT JOIN Producto p ON p.codigo_barras = m.codigo_barras
                ORDER BY m.id_movimiento DESC
                LIMIT 10");
        }

        private void CargarMasVendidos()
        {
            dgvMasVendidos.DataSource = BaseDatos.Consultar(@"
                SELECT p.nombre AS Producto, SUM(v.cantidad) AS Unidades
                FROM Venta v
                JOIN InformacionVenta i ON i.id_venta = v.id_venta
                JOIN Producto p ON p.codigo_barras = v.codigo_barras
                WHERE date(i.fecha) >= date('now', 'localtime', '-6 days')
                GROUP BY p.codigo_barras
                ORDER BY Unidades DESC
                LIMIT 5");
        }

        private void CargarPorAgotarse()
        {
            dgvPorAgotarse.DataSource = BaseDatos.Consultar(@"
                SELECT p.nombre AS Producto, p.stock AS Stock, pr.nombre AS Proveedor
                FROM Producto p
                LEFT JOIN Proveedor pr ON pr.id_proveedor = p.id_proveedor
                WHERE p.stock <= @limite
                ORDER BY p.stock, p.nombre
                LIMIT 10",
                ("@limite", StockBajo));
        }
    }
}