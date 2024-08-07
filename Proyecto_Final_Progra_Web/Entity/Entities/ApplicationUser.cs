using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
        [Table("AspNetUsers")]
        public class ApplicationUser : IdentityUser
        {
            [Required(ErrorMessage = "Su nombre es requerido")]
            [MaxLength(100)]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "Su apellido es requerido")]
            [MaxLength(100)]
            public string Apellido { get; set; }
            public byte[]? Imagen { get; set; }
            public DateTime? UltimaConexion { get; set; }
            public bool Estado { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}
