using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estate.EntityLayer.Entities
{
    public class GeneralSettings
    {
        [Key]
        public int GeneralSettingsId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
