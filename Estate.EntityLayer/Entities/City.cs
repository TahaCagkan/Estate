using System.ComponentModel.DataAnnotations;


namespace Estate.EntityLayer.Entities
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool Status { get; set; }
        public virtual List<District> Districts { get; set; }
    }
}
