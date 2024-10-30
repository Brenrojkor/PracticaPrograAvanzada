using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada2_Grupo2.Data;
using PracticaProgramada2_Grupo2.Models;

namespace PracticaProgramada2_Grupo2.Controllers
{
    public class CancionController : ControllerBase
    {
        private readonly MinombredeconexionDbContext _contextAcceso;

        public CancionController(MinombredeconexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CancionModel>> ObtenerCanciones()
        {
            return Ok(_contextAcceso.g2_canciones.ToList());
        }

        [HttpGet("{_id}")]
        public ActionResult<IEnumerable<CancionModel>> ObtenerCanciones(int _id)
        {
            var datos = _contextAcceso.g2_canciones.Find(_id);

            if (datos == null)
            {
                return NotFound("El dato buscado no existe. ");
            }

            return Ok(datos);
        }

        [HttpPost]
        public IActionResult AgregarCancion(CancionModel _datos)
        {
            try
            {
                _contextAcceso.g2_canciones.Add(_datos);
                _contextAcceso.SaveChanges();

                return Ok("Canción insertada exitosamente. ");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult ModificarCancion(CancionModel _datos)
        {
            try
            {
                if (!ConsultarDatos(_datos.Id_Cancion))
                {
                    return NotFound("El dato buscado no existe. ");
                }

                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Canción modificada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{_id}")]
        public ActionResult EliminarCanciones(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El datos buscado no existe. ");
                }

                var datos = _contextAcceso.g2_canciones.Find(_id);

                _contextAcceso.g2_canciones.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok($"Se eliminó el registro {_id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.g2_canciones.Any(x => x.Id_Cancion == _id);
        }
    }
}
