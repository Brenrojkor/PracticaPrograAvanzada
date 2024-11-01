using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada2_Grupo2.Data;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PracticaProgramada2_Grupo2.Data;
using PracticaProgramada2_Grupo2.Models;

namespace PracticaProgramada2_Grupo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancionController : ControllerBase
    {
        private readonly MinombredeconexionDbContext _contextAcceso;

        public CancionController(MinombredeconexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        // GET: api/Cancion
        [HttpGet]
        public async Task<IActionResult> ObtenerCanciones()
        {
            var canciones = await _contextAcceso.G2_Canciones.ToListAsync();
            return Ok(canciones);
        }

        // GET: api/Cancion/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCancion(int id)
        {
            var cancion = await _contextAcceso.G2_Canciones.FindAsync(id);
            if (cancion == null)
            {
                return NotFound("La canción no existe en el sistema.");
            }
            return Ok(cancion);
        }


        // POST: api/Cancion
        [HttpPost]
        public async Task<IActionResult> AgregarCancion([FromBody] CancionModel nuevaCancion)
        {
            if (string.IsNullOrWhiteSpace(nuevaCancion.Titulo) || string.IsNullOrWhiteSpace(nuevaCancion.Artista))
            {
                return BadRequest("La información de la canción no es válida.");
            }

            _contextAcceso.G2_Canciones.Add(nuevaCancion);
            await _contextAcceso.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerCancion), new { id = nuevaCancion.Id_Cancion }, nuevaCancion);
        }

        // DELETE: api/Cancion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarCancion(int id)
        {
            var cancion = await _contextAcceso.G2_Canciones.FindAsync(id);
            if (cancion == null)
            {
                return NotFound("La canción no existe en el sistema.");
            }

            _contextAcceso.G2_Canciones.Remove(cancion);
            await _contextAcceso.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Cancion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCancion(int id, [FromBody] CancionModel cancionActualizada)
        {
            if (id != cancionActualizada.Id_Cancion)
            {
                return BadRequest("El ID de la canción no coincide.");
            }

            if (string.IsNullOrWhiteSpace(cancionActualizada.Titulo) || string.IsNullOrWhiteSpace(cancionActualizada.Artista))
            {
                return BadRequest("La información de la canción no es válida.");
            }

            var cancion = await _contextAcceso.G2_Canciones.FindAsync(id);
            if (cancion == null)
            {
                return NotFound("La canción no existe en el sistema.");
            }

            // Actualizar los datos de la canción
            cancion.Titulo = cancionActualizada.Titulo;
            cancion.Artista = cancionActualizada.Artista;

            _contextAcceso.Entry(cancion).State = EntityState.Modified;
            await _contextAcceso.SaveChangesAsync();

            return Ok($"Canción modificada exitosamente, ID: {id}");
        }
    }
}