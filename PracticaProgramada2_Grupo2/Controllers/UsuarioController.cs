using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada2_Grupo2.Data;
using PracticaProgramada2_Grupo2.Models;
using System.Threading.Tasks;

namespace PracticaProgramada2_Grupo2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly MinombredeconexionDbContext _contextAcceso;

        public UsuarioController(MinombredeconexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        // GET: Usuario/Register
        [HttpGet]
        public IActionResult Registro()
        {
            return View("~/Views/Home/Registro.cshtml");
        }

        // POST: Usuario/Register
        [HttpPost]
        public async Task<IActionResult> Registro(string Usuario, string Contraseña)
        {
            var existingUser = await _contextAcceso.G2_Usuarios.FirstOrDefaultAsync(u => u.Usuario == Usuario);
            if (existingUser != null)
            {
                ModelState.AddModelError("usuario", "Este usuario ya existe.");
                return View("~/Views/Home/Registro.cshtml");
            }

            var newUser = new UsuarioModel
            {
                Usuario = Usuario,
                Contraseña = Contraseña
            };

            _contextAcceso.G2_Usuarios.Add(newUser);
            await _contextAcceso.SaveChangesAsync();
            return RedirectToAction("IniciarSesion", "Usuario");
        }

        // GET: Usuario/Login
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View("~/Views/Home/IniciarSesion.cshtml");
        }

        // POST: Usuario/Login
        [HttpPost]
        public IActionResult Login(string Usuario, string Contraseña)
        {
            var user = _contextAcceso.G2_Usuarios
                .FirstOrDefault(u => u.Usuario == Usuario && u.Contraseña == Contraseña);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return View("~/Views/Home/IniciarSesion.cshtml");
        }
    }
}
