using System.Data;
using System.Drawing.Printing;
using System.Globalization;

namespace GestorProductosAT
{
    public partial class UcPuntoVenta : UserControl
    {
        // El carrito es como una tabla de memoria, no se guarda hasta cobrar la venta
        private readonly DataTable carrito = new();

        public UcPuntoVenta()
        {
            InitializeComponent();
            Load += UcPuntoVenta_Load;

            txtCodigo.KeyDown += (s, e) =>
            {
                // Un lector de código de barras "teclea" el código y presiona Enter
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // evita el sonido de Windows
                    AgregarProducto();
                }
            };
            btnAgregar.Click += (s, e) => AgregarProducto();
            btnQuitar.Click += (s, e) => QuitarSeleccionado();
            btnVaciar.Click += (s, e) => { carrito.Clear(); ActualizarTotal(); txtCodigo.Focus(); };
            cmbMetodo.SelectedIndexChanged += (s, e) => ActualizarCambio();
            txtRecibido.TextChanged += (s, e) => ActualizarCambio();
            btnCobrar.Click += (s, e) => Cobrar();
        }

        private void UcPuntoVenta_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            carrito.Columns.Add("codigo", typeof(string));
            carrito.Columns.Add("nombre", typeof(string));
            carrito.Columns.Add("precio", typeof(double));
            carrito.Columns.Add("cantidad", typeof(int));
            carrito.Columns.Add("subtotal", typeof(double));

            dgvCarrito.DataSource = carrito;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.MultiSelect = false;
            dgvCarrito.RowHeadersVisible = false;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCarrito.Columns["codigo"]!.HeaderText = "Código";
            dgvCarrito.Columns["nombre"]!.HeaderText = "Producto";
            dgvCarrito.Columns["precio"]!.HeaderText = "Precio";
            dgvCarrito.Columns["cantidad"]!.HeaderText = "Cantidad";
            dgvCarrito.Columns["subtotal"]!.HeaderText = "Subtotal";
            dgvCarrito.Columns["precio"]!.DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["subtotal"]!.DefaultCellStyle.Format = "C2";

            cmbMetodo.Items.Clear();
            cmbMetodo.Items.Add("Efectivo");
            cmbMetodo.Items.Add("Tarjeta");
            cmbMetodo.SelectedIndex = 0;

            ActualizarTotal();
            ActiveControl = txtCodigo;
        }

        private double Total() =>
            carrito.Rows.Cast<DataRow>().Sum(r => (double)r["subtotal"]);

        private void ActualizarTotal()
        {
            lblTotal.Text = "Total: " + Total().ToString("C2");
            ActualizarCambio();
        }

        private void ActualizarCambio()
        {
            bool efectivo = cmbMetodo.Text == "Efectivo";
            txtRecibido.Enabled = efectivo;

            if (efectivo && double.TryParse(txtRecibido.Text, out double recibido) && recibido >= Total())
                lblCambio.Text = "Cambio: " + (recibido - Total()).ToString("C2");
            else
                lblCambio.Text = "Cambio: —";
        }

        private void AgregarProducto()
        {
            string codigo = txtCodigo.Text.Trim();
            if (codigo == "") return;
            int cantidad = (int)nudCantidad.Value;

            var t = BaseDatos.Consultar(
                "SELECT nombre, precio_venta, stock FROM Producto WHERE codigo_barras = @c",
                ("@c", codigo));

            if (t.Rows.Count == 0)
            {
                MessageBox.Show("No existe un producto con ese código.");
                txtCodigo.SelectAll();
                return;
            }

            string nombre = t.Rows[0]["nombre"].ToString()!;
            double precio = Convert.ToDouble(t.Rows[0]["precio_venta"]);
            int stock = Convert.ToInt32(t.Rows[0]["stock"]);

            // ¿Ya está en el carrito? Entonces se suma a lo que ya llevaba
            DataRow? existente = carrito.Rows.Cast<DataRow>()
                .FirstOrDefault(r => (string)r["codigo"] == codigo);
            int yaEnCarrito = existente == null ? 0 : (int)existente["cantidad"];

            if (yaEnCarrito + cantidad > stock)
            {
                MessageBox.Show($"Stock insuficiente de \"{nombre}\".\nDisponibles: {stock}. En el carrito: {yaEnCarrito}.");
                return;
            }

            int nuevaCantidad = yaEnCarrito + cantidad;
            double subtotal = Math.Round(precio * nuevaCantidad, 2);

            if (existente != null)
            {
                existente["cantidad"] = nuevaCantidad;
                existente["subtotal"] = subtotal;
            }
            else
            {
                carrito.Rows.Add(codigo, nombre, precio, nuevaCantidad, subtotal);
            }

            nudCantidad.Value = 1;
            txtCodigo.Clear();
            txtCodigo.Focus();
            ActualizarTotal();
        }

        private void QuitarSeleccionado()
        {
            if (dgvCarrito.CurrentRow?.DataBoundItem is DataRowView fila)
            {
                carrito.Rows.Remove(fila.Row);
                ActualizarTotal();
            }
            else
            {
                MessageBox.Show("Selecciona un producto del carrito.");
            }
            txtCodigo.Focus();
        }

        private void Cobrar()
        {
            if (carrito.Rows.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.");
                return;
            }

            double total = Total();
            string metodo = cmbMetodo.Text;
            double recibido = total;

            if (metodo == "Efectivo")
            {
                if (!double.TryParse(txtRecibido.Text, out recibido) || recibido < total)
                {
                    MessageBox.Show("El efectivo recibido es menor al total.");
                    txtRecibido.Focus();
                    return;
                }
            }

            string ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            long idVenta = 0;

            try
            {
                // Aqui hay de dos o se guarda todo o no se guarda nada
                BaseDatos.EnTransaccion(tx =>
                {
                    // 1. El ticket (un registro por venta)
                    idVenta = tx.Insertar(
                        "INSERT INTO InformacionVenta (fecha, cajero, metodo_pago) VALUES (@f, @c, @m)",
                        ("@f", ahora), ("@c", Sesion.Nombre), ("@m", metodo));

                    foreach (DataRow fila in carrito.Rows)
                    {
                        string codigo = (string)fila["codigo"];
                        int cantidad = (int)fila["cantidad"];

                        // 2. Cada producto vendido
                        tx.Ejecutar(
                            @"INSERT INTO Venta (id_venta, codigo_barras, cantidad, precio_unitario, subtotal)
                              VALUES (@v, @c, @q, @p, @s)",
                            ("@v", idVenta), ("@c", codigo), ("@q", cantidad),
                            ("@p", (double)fila["precio"]), ("@s", (double)fila["subtotal"]));

                        // 3. Se resta del inventario (solo si todavía alcanza)
                        int afectadas = tx.Ejecutar(
                            "UPDATE Producto SET stock = stock - @q WHERE codigo_barras = @c AND stock >= @q",
                            ("@q", cantidad), ("@c", codigo));
                        if (afectadas == 0)
                            throw new Exception($"Ya no hay stock suficiente de {fila["nombre"]}.");

                        // 4. Queda registrada la salida en el historial
                        int stockActual = Convert.ToInt32(tx.Valor(
                            "SELECT stock FROM Producto WHERE codigo_barras = @c", ("@c", codigo)));
                        tx.Ejecutar(
                            @"INSERT INTO Movimiento (id_venta, id_proveedor, tipo_movimiento, cantidad, fecha, inventario)
                              VALUES (@v, NULL, 'SALIDA', @q, @f, @i)",
                            ("@v", idVenta), ("@q", cantidad), ("@f", ahora), ("@i", stockActual));

                        // 5. Disfrutar, no lo descargo por que ya lo tengo :D
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar la venta. No se guardó ningún cambio.\n" + ex.Message);
                return;
            }

            // El ticket se arma antes de vaciar el carrito
            var lineas = new List<string>
            {
                "GESTOR DE PRODUCTOS AT",
                "Ticket #" + idVenta,
                "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                "Cajero: " + Sesion.Nombre,
                new string('-', 34)
            };
            foreach (DataRow fila in carrito.Rows)
            {
                lineas.Add($"{fila["cantidad"]} x {fila["nombre"]}");
                lineas.Add(((double)fila["precio"]).ToString("C2").PadLeft(16) +
                           ((double)fila["subtotal"]).ToString("C2").PadLeft(14));
            }
            lineas.Add(new string('-', 34));
            lineas.Add("TOTAL: " + total.ToString("C2"));
            lineas.Add("Pago: " + metodo);
            if (metodo == "Efectivo")
            {
                lineas.Add("Recibido: " + recibido.ToString("C2"));
                lineas.Add("Cambio: " + (recibido - total).ToString("C2"));
            }
            lineas.Add("");
            lineas.Add("¡Gracias por su compra!");

            carrito.Clear();
            txtRecibido.Clear();
            ActualizarTotal();
            txtCodigo.Focus();

            if (MessageBox.Show("Venta registrada. ¿Imprimir ticket?", "Venta",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                MostrarTicket(lineas);
            }
        }

        private void MostrarTicket(List<string> lineas)
        {
            var doc = new PrintDocument();
            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics!;
                using var fuente = new Font("Consolas", 9);
                float y = 10;
                foreach (var linea in lineas)
                {
                    g.DrawString(linea, fuente, Brushes.Black, 10, y);
                    y += fuente.GetHeight(g) + 2;
                }
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