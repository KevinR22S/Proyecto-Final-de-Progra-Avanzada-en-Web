using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Proyecto_Final_Progra_Web.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    public string Nombre { get; set; }

    [Required]
    [DisplayName("Primer Apellido")]
    public string PrimerApellido { get; set; }

    [Required]
    [DisplayName("Segundo Apellido")]
    public string SegundoApellido { get; set; }
    [Required]
    public int Edad { get; set; }
}

