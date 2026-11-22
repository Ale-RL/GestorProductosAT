
using System.Data;
using System.Drawing.Printing;
using System.Globalization;

namespace GestorProductosAT
{
    public partial class UcPuntoVenta : UserControl
    {
        // El carrito almacena temporalmente los productos antes de confirmar la venta.
        private readonly DataTable carrito = new();

        public UcPuntoVenta()
        {
            InitializeComponent();
            Load += UcPuntoVenta_Load;

            // Permite agregar un producto presionando Enter después de escribir su código.
            // También funciona con lectores de códigos de barras.
            txtCodigo.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // Evita el sonido de Windows al presionar Enter.
                    AgregarProducto();
                }
            };

            // Conecta los botones y controles con sus respectivas funciones.
            btnAgregar.Click += (s, e) => AgregarProducto();
            btnQuitar.Click += (s, e) => QuitarSeleccionado();
            btnVaciar.Click += (s, e) => { carrito.Clear(); ActualizarTotal(); txtCodigo.Focus(); };
            cmbMetodo.SelectedIndexChanged += (s, e) => ActualizarCambio();
            txtRecibido.TextChanged += (s, e) => ActualizarCambio();
            btnCobrar.Click += (s, e) => Cobrar();
        }

        private void UcPuntoVenta_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar este código mientras se diseña la interfaz.
            if (DesignMode) return;

            Estilo.Aplicar(this);

            // Crea las columnas que tendrá el carrito de compras.
            carrito.Columns.Add("codigo", typeof(string));
            carrito.Columns.Add("nombre", typeof(string));
            carrito.Columns.Add("precio", typeof(double));
            carrito.Columns.Add("cantidad", typeof(int));
            carrito.Columns.Add("subtotal", typeof(double));

            // Conecta el carrito con la tabla y configura su apariencia.
            dgvCarrito.DataSource = carrito;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.MultiSelect = false;
            dgvCarrito.RowHeadersVisible = false;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Cambia los nombres de las columnas para mostrarlos de forma más clara.
            dgvCarrito.Columns["codigo"]!.HeaderText = "Código";
            dgvCarrito.Columns["nombre"]!.HeaderText = "Producto";
            dgvCarrito.Columns["precio"]!.HeaderText = "Precio";
            dgvCarrito.Columns["cantidad"]!.HeaderText = "Cantidad";
            dgvCarrito.Columns["subtotal"]!.HeaderText = "Subtotal";

            // Muestra los precios con formato de moneda.
            dgvCarrito.Columns["precio"]!.DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["subtotal"]!.DefaultCellStyle.Format = "C2";

            // Configura los métodos de pago disponibles.
            cmbMetodo.Items.Clear();
            cmbMetodo.Items.Add("Efectivo");
            cmbMetodo.Items.Add("Tarjeta");
            cmbMetodo.SelectedIndex = 0;

            // Actualiza el total inicial y coloca el cursor en el campo del código.
            ActualizarTotal();
            ActiveControl = txtCodigo;
        }

        // Calcula el total sumando los subtotales de todos los productos del carrito.
        private double Total() =>
            carrito.Rows.Cast<DataRow>().Sum(r => (double)r["subtotal"]);

        private void ActualizarTotal()
        {
            // Muestra el total de la compra con formato de moneda.
            lblTotal.Text = "Total: " + Total().ToString("C2");

            // También actualiza el cambio que debe entregarse.
            ActualizarCambio();
        }

        private void ActualizarCambio()
        {
            // Comprueba si el método de pago seleccionado es efectivo.
            bool efectivo = cmbMetodo.Text == "Efectivo";

            // El campo de efectivo recibido solo se habilita para ese método de pago.
            txtRecibido.Enabled = efectivo;

            // Calcula el cambio si se recibió una cantidad válida y suficiente.
            if (efectivo && double.TryParse(txtRecibido.Text, out double recibido) && recibido >= Total())
                lblCambio.Text = "Cambio: " + (recibido - Total()).ToString("C2");
            else
                lblCambio.Text = "Cambio: —";
        }

        private void AgregarProducto()
        {
            // Obtiene el código escrito y la cantidad seleccionada.
            string codigo = txtCodigo.Text.Trim();
            if (codigo == "") return;

            int cantidad = (int)nudCantidad.Value;

            // Busca el producto en la base de datos mediante su código de barras.
            var t = BaseDatos.Consultar(
                "SELECT nombre, precio_venta, stock FROM Producto WHERE codigo_barras = @c",
                ("@c", codigo));

            // Comprueba si el producto existe.
            if (t.Rows.Count == 0)
            {
                MessageBox.Show("No existe un producto con ese código.");
                txtCodigo.SelectAll();
                return;
            }

            // Obtiene el nombre, precio y stock del producto encontrado.
            string nombre = t.Rows[0]["nombre"].ToString()!;
            double precio = Convert.ToDouble(t.Rows[0]["precio_venta"]);
            int stock = Convert.ToInt32(t.Rows[0]["stock"]);

            // Comprueba si el producto ya se encuentra en el carrito.
            DataRow? existente = carrito.Rows.Cast<DataRow>()
                .FirstOrDefault(r => (string)r["codigo"] == codigo);

            // Obtiene la cantidad que ya se había agregado anteriormente.
            int yaEnCarrito = existente == null ? 0 : (int)existente["cantidad"];

            // Evita agregar más unidades de las que existen en el inventario.
            if (yaEnCarrito + cantidad > stock)
            {
                MessageBox.Show($"Stock insuficiente de \"{nombre}\".\nDisponibles: {stock}. En el carrito: {yaEnCarrito}.");
                return;
            }

            // Calcula la nueva cantidad y el subtotal del producto.
            int nuevaCantidad = yaEnCarrito + cantidad;
            double subtotal = Math.Round(precio * nuevaCantidad, 2);

            // Si el producto ya estaba en el carrito, actualiza sus datos.
            if (existente != null)
            {
                existente["cantidad"] = nuevaCantidad;
                existente["subtotal"] = subtotal;
            }
            // Si no estaba, agrega una nueva fila al carrito.
            else
            {
                carrito.Rows.Add(codigo, nombre, precio, nuevaCantidad, subtotal);
            }

            // Restablece los campos y actualiza el total de la compra.
            nudCantidad.Value = 1;
            txtCodigo.Clear();
            txtCodigo.Focus();
            ActualizarTotal();
        }

        private void QuitarSeleccionado()
        {
            // Comprueba si hay una fila seleccionada en el carrito.
            if (dgvCarrito.CurrentRow?.DataBoundItem is DataRowView fila)
            {
                // Elimina el producto seleccionado y actualiza el total.
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
            // Evita registrar una venta si el carrito está vacío.
            if (carrito.Rows.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.");
                return;
            }

            // Obtiene el total, el método de pago y la cantidad recibida.
            double total = Total();
            string metodo = cmbMetodo.Text;
            double recibido = total;

            // Si el pago es en efectivo, comprueba que la cantidad recibida sea suficiente.
            if (metodo == "Efectivo")
            {
                if (!double.TryParse(txtRecibido.Text, out recibido) || recibido < total)
                {
                    MessageBox.Show("El efectivo recibido es menor al total.");
                    txtRecibido.Focus();
                    return;
                }
            }

            // Obtiene la fecha y hora actual con un formato específico.
            string ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            long idVenta = 0;

            try
            {
                // Realiza todas las operaciones dentro de una transacción.
                // Esto permite guardar todo correctamente o cancelar los cambios si ocurre un error.
                BaseDatos.EnTransaccion(tx =>
                {
                    // 1. Registra la información general de la venta.
                    // Guarda la fecha, el cajero y el método de pago.
                    idVenta = tx.Insertar(
                        "INSERT INTO InformacionVenta (fecha, cajero, metodo_pago) VALUES (@f, @c, @m)",
                        ("@f", ahora), ("@c", Sesion.Nombre), ("@m", metodo));

                    // Recorre todos los productos que se encuentran en el carrito.
                    foreach (DataRow fila in carrito.Rows)
                    {
                        string codigo = (string)fila["codigo"];
                        int cantidad = (int)fila["cantidad"];

                        // 2. Registra cada producto vendido y sus datos.
                        tx.Ejecutar(
                            @"INSERT INTO Venta (id_venta, codigo_barras, cantidad, precio_unitario, subtotal)
                              VALUES (@v, @c, @q, @p, @s)",
                            ("@v", idVenta), ("@c", codigo), ("@q", cantidad),
                            ("@p", (double)fila["precio"]), ("@s", (double)fila["subtotal"]));

                        // 3. Descuenta del inventario la cantidad de productos vendidos.
                        // Solo realiza el descuento si todavía existe suficiente stock.
                        int afectadas = tx.Ejecutar(
                            "UPDATE Producto SET stock = stock - @q WHERE codigo_barras = @c AND stock >= @q",
                            ("@q", cantidad), ("@c", codigo));

                        // Si no se pudo descontar, cancela la transacción.
                        if (afectadas == 0)
                            throw new Exception($"Ya no hay stock suficiente de {fila["nombre"]}.");

                        // 4. Consulta el stock restante después de la venta.
                        int stockActual = Convert.ToInt32(tx.Valor(
                            "SELECT stock FROM Producto WHERE codigo_barras = @c", ("@c", codigo)));

                        // Registra la salida del producto en el historial de movimientos.
                        tx.Ejecutar(
                            @"INSERT INTO Movimiento (id_venta, id_proveedor, codigo_barras, tipo_movimiento, cantidad, fecha, inventario)
                              VALUES (@v, NULL, @c, 'SALIDA', @q, @f, @i)",
                            ("@v", idVenta), ("@c", codigo), ("@q", cantidad), ("@f", ahora), ("@i", stockActual));
                    }
                });
            }
            catch (Exception ex)
            {
                // Informa si ocurrió un error y la venta no pudo registrarse.
                MessageBox.Show("No se pudo registrar la venta. No se guardó ningún cambio.\n" + ex.Message);
                return;
            }

            // Construye el contenido del ticket antes de vaciar el carrito.
            var lineas = new List<string>
            {
                "GESTOR DE PRODUCTOS AT",
                "Ticket #" + idVenta,
                "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                "Cajero: " + Sesion.Nombre,
                new string('-', 34)
            };

            // Agrega al ticket el nombre, la cantidad, el precio y el subtotal de cada producto.
            foreach (DataRow fila in carrito.Rows)
            {
                lineas.Add($"{fila["cantidad"]} x {fila["nombre"]}");
                lineas.Add(((double)fila["precio"]).ToString("C2").PadLeft(16) +
                           ((double)fila["subtotal"]).ToString("C2").PadLeft(14));
            }

            // Agrega el total y la información del pago.
            lineas.Add(new string('-', 34));
            lineas.Add("TOTAL: " + total.ToString("C2"));
            lineas.Add("Pago: " + metodo);

            // Si se pagó en efectivo, también muestra el dinero recibido y el cambio.
            if (metodo == "Efectivo")
            {
                lineas.Add("Recibido: " + recibido.ToString("C2"));
                lineas.Add("Cambio: " + (recibido - total).ToString("C2"));
            }

            // Agrega un mensaje de agradecimiento al final del ticket.
            lineas.Add("");
            lineas.Add("¡Gracias por su compra!");

            // Limpia el carrito y los campos después de registrar la venta.
            carrito.Clear();
            txtRecibido.Clear();
            ActualizarTotal();
            txtCodigo.Focus();

            // Pregunta si se desea abrir la vista previa para imprimir el ticket.
            if (MessageBox.Show("Venta registrada. ¿Imprimir ticket?", "Venta",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                MostrarTicket(lineas);
            }
        }

        private void MostrarTicket(List<string> lineas)
        {
            // Crea un documento que se utilizará para mostrar e imprimir el ticket.
            var doc = new PrintDocument();

            // Define qué contenido se dibujará en la página del documento.
            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics!;

                // Selecciona la fuente y establece la posición inicial del texto.
                using var fuente = new Font("Consolas", 9);
                float y = 10;

                // Recorre las líneas del ticket y las dibuja una debajo de otra.
                foreach (var linea in lineas)
                {
                    g.DrawString(linea, fuente, Brushes.Black, 10, y);
                    y += fuente.GetHeight(g) + 2;
                }
            };

            // Abre una ventana con la vista previa del ticket antes de imprimirlo.
            using var vista = new PrintPreviewDialog
            {
                Document = doc,
                WindowState = FormWindowState.Maximized
            };

            vista.ShowDialog();
        }
    }
}
