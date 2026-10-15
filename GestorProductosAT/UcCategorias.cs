using System.Data;

namespace GestorProductosAT
{
    public partial class UcCategorias : UserControl
    {
        private bool esNuevo = true;
        private long idActual = 0; // id de la categoría que se está editando

        public UcCategorias()
        {
            InitializeComponent();
            Load += UcCategorias_Load;
            txtBuscar.TextChanged += (s, e) => CargarCategorias();
            dgvCategorias.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0) LlenarFormulario(dgvCategorias.Rows[e.RowIndex]);
            };
            btnNuevo.Click += (s, e) => LimpiarFormulario();
            btnGuardar.Click += (s, e) => Guardar();
            btnEliminar.Click += (s, e) => Eliminar();
        }

        private void UcCategorias_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            dgvCategorias.ReadOnly = true;
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.MultiSelect = false;
            dgvCategorias.RowHeadersVisible = false;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            CargarCategorias();
            LimpiarFormulario();
        }

        private void CargarCategorias()
        {
            var tabla = BaseDatos.Consultar(@"
                SELECT id_categoria, nombre, descripcion
                FROM Categoria
                WHERE nombre LIKE @f OR descripcion LIKE @f
                ORDER BY nombre",
                ("@f", "%" + txtBuscar.Text.Trim() + "%"));

            dgvCategorias.DataSource = tabla;

            dgvCategorias.Columns["id_categoria"]!.HeaderText = "ID";
            dgvCategorias.Columns["nombre"]!.HeaderText = "Nombre";
            dgvCategorias.Columns["descripcion"]!.HeaderText = "Descripción";
        }

        private void LlenarFormulario(DataGridViewRow fila)
        {
            esNuevo = false;
            idActual = Convert.ToInt64(fila.Cells["id_categoria"].Value);
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtDescripcion.Text = fila.Cells["descripcion"].Value?.ToString();
        }

        private void LimpiarFormulario()
        {
            esNuevo = true;
            idActual = 0;
            txtNombre.Clear();
            txtDescripcion.Clear();
            dgvCategorias.ClearSelection();
            txtNombre.Focus();
        }

        private void Guardar()
        {
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (nombre == "")
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            try
            {
                if (esNuevo)
                {
                    // El id se genera solo asi no hay que escribirlo
                    BaseDatos.Ejecutar(
                        "INSERT INTO Categoria (nombre, descripcion) VALUES (@n, @d)",
                        ("@n", nombre), ("@d", descripcion));
                }
                else
                {
                    BaseDatos.Ejecutar(
                        "UPDATE Categoria SET nombre = @n, descripcion = @d WHERE id_categoria = @id",
                        ("@n", nombre), ("@d", descripcion), ("@id", idActual));
                }

                CargarCategorias();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar:\n" + ex.Message);
            }
        }

        private void Eliminar()
        {
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona una categoría de la lista.");
                return;
            }

            if (MessageBox.Show($"¿Eliminar la categoría \"{txtNombre.Text}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                BaseDatos.Ejecutar("DELETE FROM Categoria WHERE id_categoria = @id", ("@id", idActual));
                CargarCategorias();
                LimpiarFormulario();
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar: hay productos que usan esta categoría.\n" +
                                "Primero cámbialos de categoría o elimínalos.");
            }
        }

    }

}