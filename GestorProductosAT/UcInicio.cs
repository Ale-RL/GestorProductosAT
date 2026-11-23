using System.Data;

namespace GestorProductosAT
{
    public partial class UcInicio : UserControl
    {
        // Define el límite de existencias para considerar que un producto tiene stock bajo

        // Constructor del panel de inicio
        public UcInicio()
        {
            InitializeComponent();

            // Ejecuta la configuración inicial cuando se carga el control
            Load += UcInicio_Load;

            // Reorganiza los elementos cada vez que cambia el tamaño de la ventana
            Resize += (s, e) => Acomodar();
        }

        // Configura y carga la información inicial del panel
        private void UcInicio_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar estas instrucciones mientras se utiliza el diseñador
            if (DesignMode) return;

            // Configura las propiedades de las tablas
            ConfigurarTabla(dgvRecientes);
            ConfigurarTabla(dgvMasVendidos);
            ConfigurarTabla(dgvPorAgotarse);

            // Acomoda los elementos y carga la información del panel
            Acomodar();
            CargarTarjetas();
            CargarRecientes();
            CargarMasVendidos();
            CargarPorAgotarse();
        }

        // Configura las propiedades generales de una tabla
        private static void ConfigurarTabla(DataGridView dgv)
        {
            // Impide modificar directamente los datos desde la tabla
            dgv.ReadOnly = true;

            // Deshabilita la creación de filas desde la interfaz
            dgv.AllowUserToAddRows = false;

            // Permite seleccionar una fila completa
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Evita seleccionar varias filas al mismo tiempo
            dgv.MultiSelect = false;

            // Oculta los encabezados laterales de las filas
            dgv.RowHeadersVisible = false;

            // Ajusta automáticamente el ancho de las columnas
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Organiza la posición y el tamaño de las tarjetas y tablas
        private void Acomodar()
        {
            // Evita modificar los elementos mientras se utiliza el diseñador
            if (DesignMode) return;

            // Aplica el diseño personalizado a los controles
            Estilo.Aplicar(this);

            // Define los márgenes y la separación entre elementos
            int margen = 15;
            int separacion = 15;

            // Calcula el espacio disponible dentro del control
            int ancho = ClientSize.Width - margen * 2;

            // Si la ventana es demasiado pequeña, conserva la distribución actual
            if (ancho < 600) return;

            // Agrupa las tarjetas informativas en un arreglo
            Panel[] tarjetas = { pnlProductos, pnlStockBajo, pnlVentas, pnlSemana };

            // Calcula el ancho que tendrá cada tarjeta
            int anchoTarjeta = (ancho - separacion * 3) / 4;

            // Recorre las tarjetas para establecer su posición y tamaño
            for (int i = 0; i < tarjetas.Length; i++)
            {
                tarjetas[i].Left = margen + i * (anchoTarjeta + separacion);
                tarjetas[i].Width = anchoTarjeta;
            }

            // Calcula el espacio que ocuparán las dos zonas de tablas
            int izquierda = (ancho - separacion) * 55 / 100;
            int derecha = ancho - izquierda - separacion;

            // Calcula la posición horizontal de la zona derecha
            int xDerecha = margen + izquierda + separacion;

            // Calcula la altura disponible para las tablas
            int alto = ClientSize.Height - dgvRecientes.Top - margen;

            // Establece una altura mínima para evitar que las tablas sean demasiado pequeñas
            if (alto < 200) alto = 200;

            // Ajusta el tamaño de la tabla de movimientos recientes
            dgvRecientes.Width = izquierda;
            dgvRecientes.Height = alto;

            // Divide el espacio derecho en dos secciones para las otras tablas
            int altoPrimera = (alto - 45) / 2;
            int altoSegunda = alto - 45 - altoPrimera;

            // Ajusta la posición del título y la tabla de productos más vendidos
            lblMasVendidos.Left = xDerecha;
            dgvMasVendidos.SetBounds(xDerecha, dgvRecientes.Top, derecha, altoPrimera);

            // Ajusta la posición del título y la tabla de productos por agotarse
            lblPorAgotarse.Location = new Point(xDerecha, dgvRecientes.Top + altoPrimera + 15);
            dgvPorAgotarse.SetBounds(xDerecha, lblPorAgotarse.Top + 30, derecha, altoSegunda);
        }

        // Consulta los datos generales y actualiza las tarjetas informativas
        private void CargarTarjetas()
        {
            // Cuenta la cantidad total de productos registrados
            var total = BaseDatos.Consultar("SELECT COUNT(*) FROM Producto");
            lblProductosValor.Text = Convert.ToInt32(total.Rows[0][0]).ToString();

            // Cuenta los productos que tienen existencias iguales o menores al límite establecido
            var bajo = BaseDatos.Consultar(
                "SELECT COUNT(*) FROM Producto WHERE stock <= @limite", ("@limite", Config.StockBajo));

            lblStockBajoValor.Text = Convert.ToInt32(bajo.Rows[0][0]).ToString();

            // COALESCE(..., 0): si no hay ventas, SUM devuelve NULL y se convierte en 0
            // date(fecha) obtiene únicamente el día de la venta
            // date('now','localtime') obtiene la fecha actual según la hora local

            // Calcula el total de ventas realizadas durante el día actual
            var ventas = BaseDatos.Consultar(@"
                SELECT COALESCE(SUM(v.subtotal), 0)
                FROM Venta v
                JOIN InformacionVenta i ON i.id_venta = v.id_venta
                WHERE date(i.fecha) = date('now', 'localtime')");

            // Muestra el total de ventas con formato de moneda
            lblVentasValor.Text = Convert.ToDouble(ventas.Rows[0][0]).ToString("C2");

            // Calcula el total de ventas de los últimos siete días
            var semana = BaseDatos.Consultar(@"
                SELECT COALESCE(SUM(v.subtotal), 0)
                FROM Venta v
                JOIN InformacionVenta i ON i.id_venta = v.id_venta
                WHERE date(i.fecha) >= date('now', 'localtime', '-6 days')");

            // Muestra el resultado con formato de moneda
            lblSemanaValor.Text = Convert.ToDouble(semana.Rows[0][0]).ToString("C2");
        }

        // Consulta y muestra los diez movimientos más recientes del inventario
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

        // Consulta y muestra los cinco productos más vendidos durante los últimos siete días
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

        // Consulta y muestra los productos que tienen pocas existencias
        private void CargarPorAgotarse()
        {
            dgvPorAgotarse.DataSource = BaseDatos.Consultar(@"
                SELECT p.nombre AS Producto, p.stock AS Stock, pr.nombre AS Proveedor
                FROM Producto p
                LEFT JOIN Proveedor pr ON pr.id_proveedor = p.id_proveedor
                WHERE p.stock <= @limite
                ORDER BY p.stock, p.nombre
                LIMIT 10",
                ("@limite", Config.StockBajo));
        }
    }
}