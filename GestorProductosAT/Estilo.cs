namespace GestorProductosAT
{
    public static class Estilo
    {
        public static readonly Color Verde = Color.FromArgb(31, 92, 69);
        public static readonly Color VerdeClaro = Color.FromArgb(46, 125, 94);

        public static void Tabla(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(255, 253, 225); // adiós al gris
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false; // sin esto, el color del encabezado se ignora
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Verde;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.DefaultCellStyle.SelectionBackColor = VerdeClaro;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 245);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(220, 230, 220);
            dgv.RowTemplate.Height = 28;
        }

        public static void Boton(Button b, Color color)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = color;
            b.ForeColor = Color.White;
            b.Cursor = Cursors.Hand;
        }
    }
}