using System.ComponentModel.DataAnnotations;

namespace Estate.EntityLayer.Entities
{
    public class Neighbourhood
    {
        [Key]
        public int NeighbourhoodId { get; set; }
        public string NeighbourhoodName { get; set; }
        public bool Status { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
    }
}
