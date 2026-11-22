using System.Data;

namespace GestorProductosAT 
{
    public partial class UcCategorias : UserControl //top 5 clases publicas jejejejejejsjejejsje
    {
        // Indica si se está creando una categoría nueva o editando una existente
        private bool esNuevo = true;

        // Guarda el ID de la categoría que se está editando
        private long idActual = 0;

        // Constructor del control de categorías
        public UcCategorias()
        {
            InitializeComponent();

            // Ejecuta la carga inicial cuando se abre el control
            Load += UcCategorias_Load;

            // Actualiza la lista cada vez que cambia el texto de búsqueda
            txtBuscar.TextChanged += (s, e) => CargarCategorias();

            // Al seleccionar una fila, muestra sus datos en el formulario
            dgvCategorias.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0) LlenarFormulario(dgvCategorias.Rows[e.RowIndex]);
            };

            // Prepara el formulario para registrar una categoría nueva
            btnNuevo.Click += (s, e) => LimpiarFormulario();

            // Ejecuta el método para guardar una categoría
            btnGuardar.Click += (s, e) => Guardar();

            // Ejecuta el método para eliminar una categoría
            btnEliminar.Click += (s, e) => Eliminar();
        }

        // Configura el control cuando termina de cargarse
        private void UcCategorias_Load(object? sender, EventArgs e)
        {
            // Evita ejecutar estas instrucciones mientras se diseña el formulario
            if (DesignMode) return;

            // Aplica el diseño personalizado
            Estilo.Aplicar(this);

            // Configura las propiedades de la tabla de categorías
            dgvCategorias.ReadOnly = true;
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.MultiSelect = false;
            dgvCategorias.RowHeadersVisible = false;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Carga las categorías y deja el formulario limpio
            CargarCategorias();
            LimpiarFormulario();
        }

        // Consulta las categorías registradas y las muestra en la tabla
        private void CargarCategorias()
        {
            var tabla = BaseDatos.Consultar(@"
                SELECT id_categoria, nombre, descripcion
                FROM Categoria
                WHERE nombre LIKE @f OR descripcion LIKE @f
                ORDER BY nombre",
                ("@f", "%" + txtBuscar.Text.Trim() + "%"));

            // Asigna los resultados de la consulta a la tabla visual
            dgvCategorias.DataSource = tabla;

            // Cambia los nombres de las columnas que se muestran al usuario
            dgvCategorias.Columns["id_categoria"]!.HeaderText = "ID";
            dgvCategorias.Columns["nombre"]!.HeaderText = "Nombre";
            dgvCategorias.Columns["descripcion"]!.HeaderText = "Descripción";
        }

        // Coloca los datos de la categoría seleccionada en los campos del formulario
        private void LlenarFormulario(DataGridViewRow fila)
        {
            esNuevo = false;

            // Obtiene el ID de la categoría seleccionada
            idActual = Convert.ToInt64(fila.Cells["id_categoria"].Value);

            // Muestra el nombre y la descripción de la categoría
            txtNombre.Text = fila.Cells["nombre"].Value?.ToString();
            txtDescripcion.Text = fila.Cells["descripcion"].Value?.ToString();
        }

        // Limpia los campos y prepara el formulario para una nueva categoría
        private void LimpiarFormulario()
        {
            esNuevo = true;
            idActual = 0;

            // Borra el contenido de los campos de texto
            txtNombre.Clear();
            txtDescripcion.Clear();

            // Quita la selección actual de la tabla
            dgvCategorias.ClearSelection();

            // Coloca el cursor en el campo del nombre
            txtNombre.Focus();
        }

        // Guarda una categoría nueva o actualiza una existente
        private void Guardar()
        {
            // Obtiene los datos escritos y elimina espacios innecesarios
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            // Comprueba que el nombre no esté vacío
            if (nombre == "")
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            try
            {
                if (esNuevo)
                {
                    // Registra una categoría nueva en la base de datos
                    // El ID se genera automáticamente, por lo que no es necesario escribirlo
                    BaseDatos.Ejecutar(
                        "INSERT INTO Categoria (nombre, descripcion) VALUES (@n, @d)",
                        ("@n", nombre), ("@d", descripcion));
                }
                else
                {
                    // Actualiza los datos de la categoría seleccionada
                    BaseDatos.Ejecutar(
                        "UPDATE Categoria SET nombre = @n, descripcion = @d WHERE id_categoria = @id",
                        ("@n", nombre), ("@d", descripcion), ("@id", idActual));
                }

                // Actualiza la tabla y limpia los campos después de guardar
                CargarCategorias();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Muestra el error si no se pudo guardar la categoría
                MessageBox.Show("No se pudo guardar:\n" + ex.Message);
            }
        }

        // Elimina la categoría seleccionada de la base de datos
        private void Eliminar()
        {
            // Comprueba que se haya seleccionado una categoría existente
            if (esNuevo)
            {
                MessageBox.Show("Primero selecciona una categoría de la lista.");
                return;
            }

            // Solicita confirmación antes de eliminar el registro
            if (MessageBox.Show($"¿Eliminar la categoría \"{txtNombre.Text}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                // Elimina la categoría utilizando su ID
                BaseDatos.Ejecutar("DELETE FROM Categoria WHERE id_categoria = @id", ("@id", idActual));

                // Actualiza la tabla y limpia el formulario
                CargarCategorias();
                LimpiarFormulario();
            }
            catch (Exception)
            {
                // Muestra un mensaje si no es posible eliminar la categoría
                // Esto puede ocurrir cuando existen productos relacionados con ella
                MessageBox.Show("No se puede eliminar: hay productos que usan esta categoría.\n" +
                                "Primero cámbialos de categoría o elimínalos.");
            }
        }

    }

}