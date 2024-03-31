/*
    Clase que servirá como Entrada para el BE.
    Los datos cargados del Fe se pasaron por JSON y serán cargados a esta clase.
*/

namespace UF2_Robots.Models.JsonData
{
    public class ItemJSON
    {
        public string? TableName { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal? Price { get; set; }
        public int? Category { get; set; }
        public string? DescCategory { get; set; }
    }
}