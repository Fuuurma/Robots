using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UF2_Robots.Models;
using UF2_Robots.Models.Androids;
using UF2_Robots.Models.Database;
using UF2_Robots.Models.Generics;
using UF2_Robots.Models.Robots;
using UF2_Robots.Models.JsonData;

namespace UF2_Robots.Controllers;


/*
    Se usa un solo controlador para la App.
    Permite hacer las acciones mediante consultas Http a los Modelos que actualizaran la db.
    - Crear Item
    - Actualizar Item
    - Resetear Item
    - Eliminar Item

    La clase generica que se encarga de realizar las acciones es DbManager.
    una para Robots y otra para Androides.

    Para las acciones en que no se necesitan muchos datos como Reset o Eliminar
    solo se le pasa la tabla y el id. 

    Para otras acciones más complejas como crearItem, se ha creado una clase ItemJSON que sirve como entrada para el BE, 
    Con JS se le pasa un JSON a la ruta y luego acaba convertida en el objeto INotHuman<Robot/Android> en el controlador.

*/

public class HomeController : Controller
{
    
    private readonly DbManager<Robot> _robotDbManager;
    private readonly DbManager<Android> _androidDbManager;

    public HomeController(DatabaseConnection dbConnection)
    {
        _robotDbManager = new DbManager<Robot>(dbConnection);
        _androidDbManager = new DbManager<Android>(dbConnection);
    }

    public IActionResult Index()
    {
        var robots = _robotDbManager.GetAll("Robots"); 
        var androids = _androidDbManager.GetAll("Androids"); 
            
        ViewData["Robots"] = robots;
        ViewData["Androids"] = androids;

        return View();
    }

    [HttpPost]
    public IActionResult Delete(string tableName, int id)
    {
        if (tableName == "Robots")
            _robotDbManager.Delete(id, tableName);
        else if (tableName == "Androids")
            _androidDbManager.Delete(id, tableName);
        else
            return BadRequest($"No se ha podido encontrar la tabla. {tableName}");

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Update([FromBody] ItemJSON item)
    {
        try
        {
            string tableName = item.TableName!;
            int id = item.Id ?? 0;
            string name = item.Name ?? "";
            string type = item.Type ?? "";
            decimal price = item.Price ?? 0;
            int category = item.Category ?? 0;
            string descCategory = item.DescCategory ?? "";

            switch (tableName)
            {
                case "Robots":
                    var robot = new INotHuman<Robot>
                    {
                        Id = id,
                        Name = name,
                        Type = type,
                        Price = price,
                        Category = 1,
                        DescCategory = "Robot"
                    };
                    _robotDbManager.Update(robot, tableName);
                    break;
                case "Androids":
                    var android = new INotHuman<Android>
                    {
                        Id = id,
                        Name = name,
                        Type = type,
                        Price = price,
                        Category = 2,
                        DescCategory = "Android"
                    };
                    _androidDbManager.Update(android, tableName);
                    break;
                default:
                    return BadRequest($"No se ha podido encontrar la tabla. {tableName}");
            }
            return Ok($"{name} ha sido actualizado.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al actualizar: {ex.Message}");
        }
    }


    [HttpPost]
    public IActionResult Create([FromBody] ItemJSON item)
    {
        try
        {
            string tableName = item.TableName!;
            int id = item.Id ?? 0;
            string name = item.Name ?? "";
            string type = item.Type ?? "";
            decimal price = item.Price ?? 0;
            int category = item.Category ?? 0;
            string descCategory = item.DescCategory ?? "";

            Console.WriteLine($"Trying to CREATE: {name}");
            switch (tableName)
            {
                case "Robots":
                    var robot = new INotHuman<Robot>
                    {
                        Id = id,
                        Name = name,
                        Type = type,
                        Price = price,
                        Category = 1,
                        DescCategory = "Robot"
                    };
                    _robotDbManager.Insert(robot, tableName);
                    break;
                case "Androids":
                    var android = new INotHuman<Android>
                    {
                        Id = id,
                        Name = name,
                        Type = type,
                        Price = price,
                        Category = 2,
                        DescCategory = "Android"
                    };
                    _androidDbManager.Insert(android, tableName);
                    break;

                default:
                    return BadRequest($"No se ha podido encontrar la tabla. {tableName}");

            }
            Console.WriteLine($"CREATED! {name}");
            return Ok($"{name} ha sido creado.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al crear: {ex.Message}");
        }
    }


    [HttpPost]
    public IActionResult Reset(string tableName, int id) 
    {
        try
        {
            switch (tableName)
            {
                case "Robots":
                    _robotDbManager.SetToFactory(id, tableName);
                    break;
                case "Androids":
                    _androidDbManager.SetToFactory(id, tableName);
                    break;
                default:
                    return BadRequest($"No se ha podido encontrar la tabla. {tableName}");
            }
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    public IActionResult InsertTestItems()
    { // /Home/InsertTestItems/
        _robotDbManager.Insert(new INotHuman<Robot>
        {
            Name = "Test-Robot-1",
            Type = "R2D2",
            Price = 100,
            Category = 1,
            DescCategory = "Robot"
        }, "Robots");

        // Insert test androids
        _androidDbManager.Insert(new INotHuman<Android>
        {
            Name = "Android 1",
            Type = "Type 1",
            Price = 200,
            Category = 2,
            DescCategory = "Android"
        }, "Androids");

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
