using Microsoft.Data.Sqlite;
using System.Data;

namespace GestorProductosAT
{
    public static class BaseDatos
    {
        //El arcchivo .db vive junto al programa
        private static readonly string CadenaConexion =
            "Data Source=" + Path.Combine(AppContext.BaseDirectory, "GestorProductosAT.db");

        private static SqliteConnection Abrir()
        {
            var conexion = new SqliteConnection(CadenaConexion);
            conexion.Open();

            //SQlite no exige llaves foraneas, se activan aca
            using var pragma = conexion.CreateCommand();
            pragma.CommandText = "PRAGMA foreign_keys = ON;";
            pragma.ExecuteNonQuery();

            return conexion;
        }

        //Para SELECT: devuelve una tabla con los resultados
        public static DataTable Consultar(string sql, params (string nombre, object valor)[] parametros)
        {
            using var conexion = Abrir();
            using var comando = conexion.CreateCommand();
            comando.CommandText = sql;
            foreach (var p in parametros)
                comando.Parameters.AddWithValue(p.nombre, p.valor ?? DBNull.Value);

            var tabla = new DataTable();
            using var lector = comando.ExecuteReader();
            tabla.Load(lector);
            return tabla;
        }

        //Para INSERT, UPDATE, DELETE: devuelve cuantas filas han sido afectadas
        public static int Ejecutar(string sql, params (string nombre, object valor)[] parametros)
        {
            using var conexion = Abrir();
            using var comando = conexion.CreateCommand();
            comando.CommandText= sql;
            foreach (var p in parametros)
                comando.Parameters.AddWithValue(p.nombre, p.valor ?? DBNull.Value);

            return comando.ExecuteNonQuery();
        }
    }
}