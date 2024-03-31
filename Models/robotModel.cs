// robotModel

/*
    Clase Robot. Solo sirve para instanciarse con este tipo de dato. 
    Toda la lógica está en genericModel.cs
*/

namespace UF2_Robots.Models.Robots
{

    public class Robot
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Category { get; set; }
        public string DescCategory { get; set; }
    }
}
