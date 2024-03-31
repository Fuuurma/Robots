// genericModel
using UF2_Robots.Models.Database;
using System.Data;
using UF2_Robots.Models.Validate;

/*
    Clase de Genericos. Contiene la gran parte de la lógica. 
    Se encarga de crear clases genericas para:
        - Robots y Androides
        - Conexion y acciones con la db (una para cada tipo)

*/

namespace UF2_Robots.Models.Generics
{
    public class INotHuman<T> // Androide y Robot comparten estructura con INotHuman
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Category { get; set; }
        public string DescCategory { get; set; }
    }

    public class DbManager<T> // dbManagerRobot y dbManagerAndroid podrán hacer las mismas funciones.
    {
        private readonly DatabaseConnection dbConnection;

        public DbManager(DatabaseConnection dbConn)
        {
            dbConnection = dbConn;
        }

        public void Insert(INotHuman<T> item, string tableName)
        {
            using (var connection = dbConnection.Open())
            {
                // int validationErrorCode = Validations<T>.ValidateName(this, tableName, item.Id, item.Name);
                // if (validationErrorCode != 0) 
                // {
                //     Dictionary<int, string>  errorMessage = ErrorMessages<T>.GetErrorMessage(validationErrorCode, item);
                //     Console.WriteLine(errorMessage); 
                //     return; 
                // }
                
                var actions = new DatabaseActions(connection, tableName);

                var query = new DatabaseQuery(connection);

                // Prepare the insert query
                string insertQuery = $"INSERT INTO {tableName} (Name, Type, Price, Category, DescCategory) VALUES (@Name, @Type, @Price, @Category, @DescCategory)";
                var parameters = new Dictionary<string, object>
                {
                    {"@Name", item.Name },
                    { "@Type", item.Type },
                    { "@Price", item.Price },
                    { "@Category", item.Category },
                    { "@DescCategory", item.DescCategory }
                };

                query.ExecuteNonQuery(insertQuery, parameters);
                Console.WriteLine($"{item.Name} ha sido añadido a {tableName}");
            }
        }

        public void Delete(int Id, string tableName)
        {
            using (var connection = dbConnection.Open())
            {
                var query = new DatabaseQuery(connection);

                string deleteQuery = $"DELETE FROM {tableName} WHERE Id = @ItemId";
                var parameters = new Dictionary<string, object>
                {
                    {"@ItemId", Id}
                };
                query.ExecuteNonQuery(deleteQuery, parameters);
                Console.WriteLine($"{Id} ha sido eliminado de {tableName}");
            }
        }

        public List<INotHuman<T>> GetAll(string tableName)
        {
            List<INotHuman<T>> items = new List<INotHuman<T>>();

            using (var connection = dbConnection.Open())
            {
                var query = new DatabaseQuery(connection);

                string selectQuery = $"SELECT * FROM {tableName}";

                DataTable result = query.Select(selectQuery);

                foreach (DataRow row in result.Rows)
                {
                    var item = new INotHuman<T>
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Type = row["Type"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Category = Convert.ToInt32(row["Category"]),
                        DescCategory = row["DescCategory"].ToString()
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public INotHuman<T> Search(int id, string tableName)
        {
            INotHuman<T> item = null;

            using (var connection = dbConnection.Open())
            {
                var query = new DatabaseQuery(connection);

                string selectQuery = $"SELECT * FROM {tableName} WHERE Id = @Id";
                var parameters = new Dictionary<string, object>
                {
                    {"@Id", id}
                };

                DataTable result = query.Select(selectQuery, parameters);

                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    item = new INotHuman<T>
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Type = row["Type"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Category = Convert.ToInt32(row["Category"]),
                        DescCategory = row["DescCategory"].ToString()
                    };
                }
            }
            return item;
        }


        public void SetToFactory(int itemId, string tableName)
        {
            using (var connection = dbConnection.Open())
            {
                var query = new DatabaseQuery(connection);

                string setToFactoryQuery = $"UPDATE {tableName} SET Name = '', Type = '', Price = 0 WHERE Id = @ItemId";
                var parameters = new Dictionary<string, object>
                {
                    {"@ItemId", itemId}
                };
                query.ExecuteNonQuery(setToFactoryQuery, parameters);
                Console.WriteLine($"Se ha actualiado con Id: {itemId}");
            }
        }

        public void Update(INotHuman<T> item, string tableName)
        {
            using (var connection = dbConnection.Open())
            {
                var query = new DatabaseQuery(connection);

                string updateQuery = $"UPDATE {tableName} SET Name = @Name, Type = @Type, Price = @Price WHERE Id = @ItemId";
                var parameters = new Dictionary<string, object>
                {
                    { "@Name", item.Name },
                    { "@Type", item.Type },
                    { "@Price", item.Price },
                    { "@ItemId", item.Id }
                };
                query.ExecuteNonQuery(updateQuery, parameters);
                Console.WriteLine($"Se ha actualiado: {item.Name}");
            }
        }
    }
}

