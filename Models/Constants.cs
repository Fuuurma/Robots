using System.Collections.Generic;
using System.Text.RegularExpressions;
using UF2_Robots.Models.Generics;

/*
    TODO:
    Clase para validar los datos que entran el BE para realizar las acciones.
    Me hubiera gustado poder terminarlo y añadirlo a la App, 
    pero me estaba dando muchos errores y voy bastante justo de tiempo. 
*/

namespace UF2_Robots.Models.Validate
{
    public static class Constants
    {
        public static readonly 
            Dictionary<int, string> Categories = new Dictionary<int, string>
        {
            { 1, "Robot" },
            { 2, "Android" }
        };

        public static readonly 
            Dictionary<string, string> CategoriesTableInDb = new Dictionary<string, string>
        {
            { "Robot", "Robots" },
            { "Android", "Androids" }
        };

        public static readonly 
        Dictionary<int, string> ErrorCodes = new Dictionary<int, string>
        {
            {0, "Ok"},
            {1, "IncorrectNameFormat"},
            {2, "NameAlreadyExists"},
            {3, "IncorrectType"},
            {4, "IncorrectPrice"},
            {5, "IncorrectCategory"}, 
            {6, "IncorrectTableName"}, 
            {7, "OtherError"}
        };
    }

    public static class Validations<T>
    {
        public static int 
            ValidateName(DbManager<T> dbManager, 
                         string? tableName, 
                         int? id, 
                         string? name)
        {
            if (string.IsNullOrEmpty(name))
                return 1;
            if (string.IsNullOrEmpty(tableName))
                return 6;

            if (!Constants.CategoriesTableInDb.ContainsKey(tableName))
                return 6;

            switch (tableName)
            {
                case "Robots":
                    if (!Regex.IsMatch(name, @"^[A-Za-z]{2}-\d{3}$"))
                        return 1;
                    break;

                case "Androids":
                    if (!Regex.IsMatch(name, @"^[A-Za-z]{4}-\d{3}$"))
                        return 1;
                    break;

                default:
                    return 1;
            }

            INotHuman<T> item = dbManager.Search((int) id, tableName);
            if (item != null && item.Name != name)
                return 2;

            return 0;
        }

        public static int ValidateCategory(int category, string descCategory)
        {
            return Constants.Categories.ContainsKey(category) &&
                   Constants.Categories.ContainsValue(descCategory) ? 0 : 5;
        }
    }

   public static class ErrorMessages<T>
    {
        public static Dictionary<int, string> GetErrorMessage(int errorCode, INotHuman<T> item)
        {
           Dictionary<int, string> errorDictionary = new Dictionary<int, string>();

        switch (errorCode)
        {
            case 1:
                errorDictionary.Add(1, IncorrectNameFormat(item.Name, item.DescCategory));
                break;
            case 2:
                errorDictionary.Add(2, NameAlreadyExists(item.Id, item.Name, item.Category, item.DescCategory));
                break;
            case 3:
                errorDictionary.Add(3, IncorrectType(item.Type));
                break;
            case 4:
                errorDictionary.Add(4, IncorrectPrice(item.Price));
                break;
            case 5:
                errorDictionary.Add(5, IncorrectCategory(item.Category, item.DescCategory));
                break;
            case 6:
                errorDictionary.Add(6, IncorrectTableName(item.DescCategory));
                break;
            default:
                errorDictionary.Add(0, "Error desconocido.");
                break;
        }

        return errorDictionary;
        }

        private static string IncorrectNameFormat(string itemName, string itemCategory)
        {
            return $"El formato de {itemName} no es válido para {itemCategory}.";
        }

        private static string NameAlreadyExists(int existingId, string existingItemName, int newItemId, string itemCategory)
        {
            return $"El nombre '{existingItemName}' ya existe para {itemCategory} con el ID '{existingId}' (nuevo ID: '{newItemId}').";
        }

        private static string IncorrectType(string type)
        {
            return $"'{type}' no es un tipo válido";
        }

        private static string IncorrectPrice(decimal price)
        {
            return $"'{price}' no es un precio válido";
        }

        private static string IncorrectCategory(int category, string descCategory)
        {
            return $"'{descCategory}' no es una categoria válida";
        }

        private static string IncorrectTableName(string tableName)
        {
            return $"'{tableName}' no existe en la db.";
        }
    }
}

