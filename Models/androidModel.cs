// androidModel
/*
    Clase Androide. Solo sirve para instanciarse con este tipo de dato. 
    Toda la lógica está en genericModel.cs
*/

namespace UF2_Robots.Models.Androids
{
    public class Android
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Category { get; set; }
        public string DescCategory { get; set; }
    }
}
