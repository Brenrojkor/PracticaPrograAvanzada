using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada2_Grupo2.Data;
using PracticaProgramada2_Grupo2.Models;
using System.Reflection.Metadata.Ecma335;

namespace PracticaProgramada2_Grupo2.Controllers
{
    public class PlaylistController : ControllerBase
    {
        private readonly MinombredeconexionDbContext _contextAcceso;

        public PlaylistController(MinombredeconexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlaylistModel>> ObtenerPlaylists()
        {
            return Ok(_contextAcceso.G2_Playlists.ToList());
        }

        [HttpGet("{_id}")]
        public ActionResult<IEnumerable<PlaylistModel>> ObtenerPlaylists(int _id)
        {
            var datos = _contextAcceso.G2_Playlists.Find(_id);

            if (datos == null)
            {
                return NotFound("El dato buscado no existe. ");
            }

            return Ok(datos);
        }

        [HttpPost]
        public IActionResult AgregarPlaylist(PlaylistModel _datos)
        {
            try
            {
                _contextAcceso.G2_Playlists.Add(_datos);
                _contextAcceso.SaveChanges();

                return Ok("Playlist insertada exitosamente. ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult ModificarPlaylist(PlaylistModel _datos)
        {
            try
            {
                if (!ConsultarDatos(_datos.Id_Playlist))
                {
                    return NotFound("El dato buscado no existe. ");
                }

                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Playlist modificada exitosamente. ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{_id}")]
        public ActionResult EliminarPlaylists(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El dato buscado no existe. ");
                }

                var datos = _contextAcceso.G2_Playlists.Find(_id);

                _contextAcceso.G2_Playlists.Remove(datos);
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
            return _contextAcceso.G2_Playlists.Any(x => x.Id_Playlist == _id);
        }
    }
}
