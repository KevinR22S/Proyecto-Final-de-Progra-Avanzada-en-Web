using Entities.Entities;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Controllers
{
 public class AdminController : Controller
    {
        private readonly AuthDbContext _authDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(AuthDbContext authDbContext, RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            _authDbContext = authDbContext;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<ApplicationUser>)_userStore;
            _userManager = userManager;

        }

        //CREAR GET
        [Authorize(Roles = "Admin")]
        public IActionResult CrearUsuario()
        {
            //Se obtienen los roles y los pasa a la lista como datos de select list
            var listaRoles = _roleManager.Roles;
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "Name");
            return View();
        }

        //CREAR POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CrearUsuario(AdminCrearUsuarioViewModel usuarioModel, IFormFile imagenUsuario)
        {
            if (ModelState.IsValid)
            {
                //Crea una nueva instancia de applicationuser
                var user = new ApplicationUser();

                //Establece los datos del usuario, incluyendo el estado activo
                await _userStore.SetUserNameAsync(user, usuarioModel.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, usuarioModel.Email, CancellationToken.None);
                user.Nombre = usuarioModel.Nombre;
                user.Apellido = usuarioModel.Apellido;
                user.Estado = true;

                //Proceso para las imagenes
                byte[]? imagen = null;
                if (imagenUsuario != null && imagenUsuario.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagenUsuario.CopyToAsync(memoryStream);
                        imagen = memoryStream.ToArray();
                    }
                }

                user.Imagen = imagen;
                var result = await _userManager.CreateAsync(user, usuarioModel.Password);

                if (result.Succeeded)
                {
                    //Obtiene el nombre del rol seleccionado
                    string normalizedName = _roleManager.Roles.FirstOrDefault(r => r.Id == usuarioModel.IdRol).NormalizedName;
                    //Asigna el rol al usuario
                    var resutlRole = await _userManager.AddToRoleAsync(user, normalizedName);
                    return RedirectToAction("Index");
                }
            }
            //Obtiene los roles disponibles y los pasa a la vista como datos de selectlist
            var listaRoles = _roleManager.Roles;
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "Name");
            return View();
        }

        //INDEX
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            //Obtiene los usuarios de la bd
            var users = await _userManager.Users.ToListAsync();

            //Se repite la acción para obtener los roles y asignarlos al usuario
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.Roles = roles.ToList();
            }

            //TempData para mostrar en caso de que se altere los roles de un usuario
            if (TempData.ContainsKey("CambiosDeRolMensaje"))
            {
                ViewBag.CambiosDeRolMensaje = TempData["CambiosDeRolMensaje"].ToString();
            }

            return View(users);
        }

        //EDITAR GET
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditarUsuario(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //Se obtienen los roles del usuario
            var userRoles = await _userManager.GetRolesAsync(user);

            //Se obtiene la lista de los roles como SelectListItems
            var rolesList = _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList();

            //Uso del modelo para la vista de edición de usuario
            var model = new EditarUsuarioViewModel
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                Roles = userRoles.ToList()
            };

            //Se asigna la lista de los roles a ViewData
            ViewData["Roles"] = rolesList;

            return View(model);
        }

        //EDITAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditarUsuario(string id, EditarUsuarioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                //Para la actualización de información del usuario
                user.Nombre = model.Nombre;
                user.Apellido = model.Apellido;
                user.Email = model.Email;

                //Se elimina todos los roles existentes del usuario
                var result = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error al cambiar roles antiguos.");
                    return View(model);
                }

                //Se agregan los nuevos roles seleccionados al usuario
                result = await _userManager.AddToRolesAsync(user, model.Roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error al agregar nuevos roles.");
                    return View(model);
                }
                //Actualiza la imagen del usuario, si es que se brindó
                if (model.ImagenUsuario != null && model.ImagenUsuario.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ImagenUsuario.CopyToAsync(memoryStream);
                        user.Imagen = memoryStream.ToArray();
                    }
                }
                //Muestra este mensaje al cambiar de usuario
                TempData["CambiosDeRolMensaje"] = "Si se han realizado cambios en el rol del usuario, este último debe de volver a iniciar sesión";

                try
                {
                    //Actualiza el usuario en la db
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Error al guardar los cambios.");
                    return View(model);
                }
            }

            //Si el modelo no es válido, vuelve a mostrar el formulario con los errores correspondientes
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "Name", model.Roles);
            return View(model);
        }

        //DETALLES
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DetallesUsuario(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            ViewData["UserRoles"] = roles;

            return View(user);
        }

        //ELIMINAR sin vista, es directo
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Estado = false;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        //RESTAURAR sin vista, es directo
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestaurarUsuario(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Estado = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

    }
}
