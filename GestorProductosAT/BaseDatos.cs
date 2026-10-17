using Microsoft.Data.Sqlite;
using System.Data;

namespace GestorProductosAT
{
    public static class BaseDatos
    {
        // El archivo .db vive junto al programa, si cambiamos de motor (base de datos)
        // este es el único archivo que se modifica.
        // esto se hace para evitar configurar todo... de nuevo
        private static readonly string CadenaConexion =
            "Data Source=" + Path.Combine(AppContext.BaseDirectory, "GestorProductosAT.db");

        private static SqliteConnection Abrir()
        {
            var conexion = new SqliteConnection(CadenaConexion);
            conexion.Open();

            // SQLite no exige las llaves foráneas por defecto, hay que activarlas
            using var pragma = conexion.CreateCommand();
            pragma.CommandText = "PRAGMA foreign_keys = ON;";
            pragma.ExecuteNonQuery();

            return conexion;
        }

        // Para SELECT devuelve una tabla con los resultados
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

        // Para INSERT, UPDATE y DELETE se devuelve cuántas filas se afectaron
        public static int Ejecutar(string sql, params (string nombre, object valor)[] parametros)
        {
            using var conexion = Abrir();
            using var comando = conexion.CreateCommand();
            comando.CommandText = sql;
            foreach (var p in parametros)
                comando.Parameters.AddWithValue(p.nombre, p.valor ?? DBNull.Value);

            return comando.ExecuteNonQuery();
        }

        // Ejecuta varias instrucciones como una sola unidad,
        // si una falla, se deshace todo lo anterior.
        // este es un "por si acaso"

        public static void EnTransaccion(Action<Transaccion> trabajo)
        {
            using var conexion = Abrir();
            using var tx = conexion.BeginTransaction();
            try
            {
                trabajo(new Transaccion(conexion, tx));
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public class Transaccion
        {
            private readonly SqliteConnection conexion;
            private readonly SqliteTransaction tx;

            internal Transaccion(SqliteConnection conexion, SqliteTransaction tx)
            {
                this.conexion = conexion;
                this.tx = tx;
            }

            private SqliteCommand Crear(string sql, (string nombre, object valor)[] parametros)
            {
                var comando = conexion.CreateCommand();
                comando.Transaction = tx;
                comando.CommandText = sql;
                foreach (var p in parametros)
                    comando.Parameters.AddWithValue(p.nombre, p.valor ?? DBNull.Value);
                return comando;
            }

            public int Ejecutar(string sql, params (string nombre, object valor)[] parametros)
            {
                using var comando = Crear(sql, parametros);
                return comando.ExecuteNonQuery();
            }

            // INSERT que devuelve el id que la base generó
            public long Insertar(string sql, params (string nombre, object valor)[] parametros)
            {
                Ejecutar(sql, parametros);
                using var comando = Crear("SELECT last_insert_rowid()", Array.Empty<(string nombre, object valor)>());
                return Convert.ToInt64(comando.ExecuteScalar());
            }

            // Devuelve un solo valor (por ejemplo, el stock actual)
            public object? Valor(string sql, params (string nombre, object valor)[] parametros)
            {
                using var comando = Crear(sql, parametros);
                return comando.ExecuteScalar();
            }
        }
    }
}