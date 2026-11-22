
using Microsoft.Data.Sqlite; // Importa las herramientas necesarias para trabajar con bases de datos SQLite.
using System.Data; // Permite utilizar estructuras como DataTable y DataRow.

namespace GestorProductosAT
{
    // Clase encargada de administrar las operaciones relacionadas con la base de datos.
    public static class BaseDatos
    {
        // Define la ruta del archivo de la base de datos, ubicado junto al programa.
        // Al utilizar esta ruta, no es necesario configurar manualmente la ubicación del archivo.
        private static readonly string CadenaConexion =
            "Data Source=" + Path.Combine(AppContext.BaseDirectory, "GestorProductosAT.db");

        // Método que abre una conexión con la base de datos.
        private static SqliteConnection Abrir()
        {
            // Crea una nueva conexión utilizando la cadena de conexión.
            var conexion = new SqliteConnection(CadenaConexion);
            conexion.Open(); // Abre la conexión con SQLite.

            // Activa las llaves foráneas para mantener la relación entre las tablas.
            using var pragma = conexion.CreateCommand();
            pragma.CommandText = "PRAGMA foreign_keys = ON;";
            pragma.ExecuteNonQuery(); // Ejecuta la instrucción para activar las llaves foráneas.

            return conexion; // Devuelve la conexión abierta.
        }

        // Método utilizado para realizar consultas SELECT y obtener sus resultados en una tabla.
        public static DataTable Consultar(string sql, params (string nombre, object valor)[] parametros)
        {
            using var conexion = Abrir(); // Abre la conexión con la base de datos.
            using var comando = conexion.CreateCommand(); // Crea un comando SQL.
            comando.CommandText = sql; // Asigna la consulta que se va a ejecutar.

            // Agrega los parámetros necesarios para completar la consulta.
            foreach (var p in parametros)
                comando.Parameters.AddWithValue(p.nombre, p.valor ?? DBNull.Value);

            var tabla = new DataTable(); // Crea una tabla para almacenar los resultados.
            using var lector = comando.ExecuteReader(); // Ejecuta la consulta y obtiene un lector de datos.
            tabla.Load(lector); // Carga los resultados del lector dentro de la tabla.

            return tabla; // Devuelve la tabla con los resultados obtenidos.
        }

        // Método utilizado para ejecutar instrucciones INSERT, UPDATE y DELETE.
        public static int Ejecutar(string sql, params (string nombre, object valor)[] parametros)
        {
            using var conexion = Abrir(); // Abre la conexión con la base de datos.
            using var comando = conexion.CreateCommand(); // Crea un comando SQL.
            comando.CommandText = sql; // Asigna la instrucción que se va a ejecutar.

            // Agrega los parámetros necesarios para completar la instrucción.
            foreach (var p in parametros)
                comando.Parameters.AddWithValue(p.nombre, p.valor ?? DBNull.Value);

            return comando.ExecuteNonQuery(); // Ejecuta la instrucción y devuelve las filas afectadas.
        }

        // Comprueba si es necesario actualizar la estructura de la base de datos.
        public static void Actualizar()
        {
            // Consulta la información de las columnas que tiene la tabla Movimiento.
            var columnas = Consultar("PRAGMA table_info(Movimiento)");

            // Comprueba si ya existe la columna codigo_barras.
            bool existe = columnas.Rows.Cast<DataRow>()
                .Any(r => r["name"].ToString() == "codigo_barras");

            // Si la columna no existe, la agrega a la tabla Movimiento.
            if (!existe)
                Ejecutar("ALTER TABLE Movimiento ADD COLUMN codigo_barras TEXT REFERENCES Producto(codigo_barras)");

            // Obtiene los identificadores y las contraseñas de los usuarios registrados.
            var usuarios = Consultar("SELECT id_usuario, contrasena FROM Usuario");

            // Recorre todos los usuarios para comprobar si sus contraseñas necesitan protección.
            foreach (DataRow u in usuarios.Rows)
            {
                string actual = u["contrasena"].ToString()!;

                // Comprueba si la contraseña todavía no está protegida mediante un hash.
                if (!Seguridad.EsHash(actual))
                    Ejecutar("UPDATE Usuario SET contrasena = @h WHERE id_usuario = @id",
                        ("@h", Seguridad.Hashear(actual)), ("@id", u["id_usuario"]));
            }
        }

        // Permite ejecutar varias operaciones SQL dentro de una misma transacción.
        // Si ocurre un error, los cambios realizados se deshacen.
        public static void EnTransaccion(Action<Transaccion> trabajo)
        {
            using var conexion = Abrir(); // Abre una conexión con la base de datos.
            using var tx = conexion.BeginTransaction(); // Inicia una nueva transacción.

            try
            {
                // Ejecuta las operaciones que se recibieron como parámetro.
                trabajo(new Transaccion(conexion, tx));

                tx.Commit(); // Confirma y guarda todos los cambios realizados.
            }
            catch
            {
                tx.Rollback(); // Deshace los cambios si ocurre algún error.
                throw; // Vuelve a lanzar el error para que pueda ser atendido.
            }
        }

        // Clase que contiene los métodos para ejecutar operaciones dentro de una transacción.
        public class Transaccion
        {
            private readonly SqliteConnection conexion; // Guarda la conexión utilizada.
            private readonly SqliteTransaction tx; // Guarda la transacción activa.

            // Constructor que recibe la conexión y la transacción.
            internal Transaccion(SqliteConnection conexion, SqliteTransaction tx)
            {
                this.conexion = conexion;
                this.tx = tx;
            }

            // Crea un comando SQL asociado a la transacción actual.
            private SqliteCommand Crear(string sql, (string nombre, object valor)[] parametros)
            {
                var comando = conexion.CreateCommand(); // Crea un nuevo comando SQL.
                comando.Transaction = tx; // Asocia el comando con la transacción.
                comando.CommandText = sql; // Asigna la instrucción SQL.

                // Agrega los parámetros necesarios para ejecutar la instrucción.
                foreach (var p in parametros)
                    comando.Parameters.AddWithValue(p.nombre, p.valor ?? DBNull.Value);

                return comando; // Devuelve el comando preparado.
            }

            // Ejecuta instrucciones SQL que modifican los datos de la base de datos.
            public int Ejecutar(string sql, params (string nombre, object valor)[] parametros)
            {
                using var comando = Crear(sql, parametros); // Crea el comando con sus parámetros.
                return comando.ExecuteNonQuery(); // Ejecuta la instrucción y devuelve las filas afectadas.
            }

            // Ejecuta una inserción y devuelve el identificador generado automáticamente.
            public long Insertar(string sql, params (string nombre, object valor)[] parametros)
            {
                Ejecutar(sql, parametros); // Ejecuta la instrucción INSERT.

                // Consulta el identificador del último registro insertado en esta conexión.
                using var comando = Crear("SELECT last_insert_rowid()", Array.Empty<(string nombre, object valor)>());

                return Convert.ToInt64(comando.ExecuteScalar()); // Convierte y devuelve el identificador.
            }

            // Ejecuta una consulta que devuelve un solo valor, como una cantidad o un precio.
            public object? Valor(string sql, params (string nombre, object valor)[] parametros)
            {
                using var comando = Crear(sql, parametros); // Crea el comando con sus parámetros.
                return comando.ExecuteScalar(); // Ejecuta la consulta y devuelve un único resultado.
            }
        }
    }
}
