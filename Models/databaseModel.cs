// databaseModel

using System.Data;
using System.Data.SQLite;

/*
    Clase que se encarga de 
        - establecer conexión con la db SqLite.
        - Crear templates para queries a la db. 
          Sus parametros serán llenados en la clase genericModel con la logica del controlador.
    Al final se ha utilizado 2 tablas: 
        - Robots y Androids

*/

namespace UF2_Robots.Models.Database
{
     public class DatabaseConnection
    {
        public string ConnectionString { get; set; }

        public DatabaseConnection(string dbName)
        {
            ConnectionString = $"Data Source={dbName}.db;Version=3;";

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                const string tableName = "Inventory";
                connection.Open();
                var dbActions = new DatabaseActions(connection, tableName);

                var robotsColumns = new List<string> { "Name TEXT", "Type TEXT", "Price DECIMAL", "Category INT", "DescCategory TEXT" };
                var androidsColumns = new List<string> { "Name TEXT", "Type TEXT", "Price DECIMAL", "Category INT", "DescCategory TEXT" };
                var Inventorycolumns = new List<string> { "Name TEXT", "Type TEXT", "Price DECIMAL", "Category INT", "DescCategory TEXT" };

                // Create the tables
                dbActions.CreateTable("Robots", robotsColumns);
                dbActions.CreateTable("Androids", androidsColumns);
                dbActions.CreateTable(tableName, Inventorycolumns);

                connection.Close();
            }
        }

        public SQLiteConnection Open()
        {
            try
            {
                var connection = new SQLiteConnection(ConnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Close(SQLiteConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public class DatabaseActions // crea tablas
    {
        private readonly SQLiteConnection connection;
        public string TableName { get; set; }

        public DatabaseActions(SQLiteConnection dbConnect, string tableName)
        {
            connection = dbConnect;
            TableName = tableName;
        }


        public void CreateInventoryTable()
        {
            CreateTable("Inventory", new List<string> { "Name TEXT", 
                                                        "Type TEXT", 
                                                        "Price DECIMAL", 
                                                        "Category INT", 
                                                        "DescCategory TEXT" });
        }

        public void CreateTable(string tableName, List<string> columns)
        {
            try
            {
                string columnDefinitions = string.Join(", ", columns);
                string query = $"CREATE TABLE IF NOT EXISTS {tableName} (Id INTEGER PRIMARY KEY AUTOINCREMENT, {columnDefinitions})";
                
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando la tabla {tableName}: {ex.Message}");
            }
        }
    }

    public class DatabaseQuery // Queries
    {
        private readonly SQLiteConnection connection;

        public DatabaseQuery(SQLiteConnection dbConnect)
        {
            connection = dbConnect;
        }

        public DataTable Select(string query, Dictionary<string, object>? parameters = null)
        {
            DataTable result = new DataTable();
            try
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    var adapter = new SQLiteDataAdapter(command);
                    adapter.Fill(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            int rowsAffected = 0;
            try
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rowsAffected;
        }
    }
}
