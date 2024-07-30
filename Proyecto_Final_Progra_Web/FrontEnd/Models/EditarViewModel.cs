using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class EditarUsuarioViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; }

        [Display(Name = "Roles")]
        public List<string> Roles { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile ImagenUsuario { get; set; }
    }
}
