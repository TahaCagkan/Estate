using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estate.EntityLayer.Entities
{
    public class Images
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public bool Status { get; set; }
        public int AdvertId { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public virtual Advert Advert { get; set; }

    }
}
