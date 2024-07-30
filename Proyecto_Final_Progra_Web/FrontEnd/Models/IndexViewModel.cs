using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class IndexViewModel
    {
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Imagen")]
        public IFormFile Imagen { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
