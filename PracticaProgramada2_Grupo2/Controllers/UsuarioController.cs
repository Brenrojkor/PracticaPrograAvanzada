using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada2_Grupo2.Data;
using PracticaProgramada2_Grupo2.Models;

namespace PracticaProgramada2_Grupo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly MinombredeconexionDbContext _contextAcceso;

        public UsuarioController(MinombredeconexionDbContext contextAcceso) 
        {
            _contextAcceso = contextAcceso;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerUsuarios()
        {
            return Ok(_contextAcceso.g2_usuarios.ToList());
        }

        [HttpGet("{_id}")]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerUsuarios(int _id) 
        {
            var datos = _contextAcceso.g2_usuarios.Find(_id);

            if (datos == null) 
            {
                return NotFound("El dato buscado no existe. ");
            }

            return Ok(datos);
        }

        [HttpPost]
        public IActionResult AgregarUsuario(UsuarioModel _datos)
        {

            try 
            {
                _contextAcceso.g2_usuarios.Add(_datos);
                _contextAcceso.SaveChanges();

                return Ok("Usuario insertado exitosamente. ");

            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        public IActionResult ModificarUsuario(UsuarioModel _datos)
        {
            
            try
            {
                if (!ConsultarDatos(_datos.Id_Usuario))
                {
                    return NotFound("El dato buscado no existe. ");
                }

                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Usuario modificado exitosamente. ");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        public ActionResult EliminarUsuario(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El dato buscado no existe. ");
                }

                var datos = _contextAcceso.g2_usuarios.Find(_id);

                _contextAcceso.g2_usuarios.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok($"Se elimino el registro {_id}. ");
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.g2_usuarios.Any(x => x.Id_Usuario == _id);
        }
    }
}
