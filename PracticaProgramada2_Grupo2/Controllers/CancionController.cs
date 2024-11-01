using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada2_Grupo2.Data;
using PracticaProgramada2_Grupo2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticaProgramada2_Grupo2.Controllers
{
    public class CancionController : Controller
    {
        private readonly MinombredeconexionDbContext _context;

        public CancionController(MinombredeconexionDbContext context)
        {
            _context = context;
        }

        // GET: Cancion
        public async Task<IActionResult> Index()
        {
            var canciones = await _context.G2_Canciones.ToListAsync();
            return View(canciones); 
        }

        // GET: Cancion/{id}
        public async Task<IActionResult> Detalle(int id)
        {
            var cancion = await _context.G2_Canciones.FindAsync(id);
            if (cancion == null)
            {
                return NotFound("La canción no existe en el sistema.");
            }
            return View(cancion); 
        }

        // GET: Cancion/Agregar
        public IActionResult Agregar()
        {
            return View(); 
        }

        // POST: Cancion/Agregar
        [HttpPost]
        public async Task<IActionResult> Agregar(CancionModel nuevaCancion)
        {
            if (string.IsNullOrWhiteSpace(nuevaCancion.Titulo) || string.IsNullOrWhiteSpace(nuevaCancion.Artista))
            {
                ModelState.AddModelError("", "La información de la canción no es válida.");
                return View(nuevaCancion); 
            }

            _context.G2_Canciones.Add(nuevaCancion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Cancion/Editar/{id}
        public async Task<IActionResult> Editar(int id)
        {
            var cancion = await _context.G2_Canciones.FindAsync(id);
            if (cancion == null)
            {
                return NotFound("La canción no existe en el sistema.");
            }
            return View(cancion); 
        }

        // POST: Cancion/Editar/{id}
        [HttpPost]
        public async Task<IActionResult> Editar(int id, CancionModel cancionActualizada)
        {
            if (id != cancionActualizada.Id_Cancion)
            {
                return BadRequest("El ID de la canción no coincide.");
            }

            if (string.IsNullOrWhiteSpace(cancionActualizada.Titulo) || string.IsNullOrWhiteSpace(cancionActualizada.Artista))
            {
                ModelState.AddModelError("", "La información de la canción no es válida.");
                return View(cancionActualizada); 
            }

            var cancion = await _context.G2_Canciones.FindAsync(id);
            if (cancion == null)
            {
                return NotFound("La canción no existe en el sistema.");
            }

            // Actualizar los datos de la canción
            cancion.Titulo = cancionActualizada.Titulo;
            cancion.Artista = cancionActualizada.Artista;

            _context.Entry(cancion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 
        }

        // POST: Cancion/Eliminar/{id}
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var cancion = await _context.G2_Canciones.FindAsync(id);
            if (cancion == null)
            {
                return NotFound("La canción no existe en el sistema.");
            }

            _context.G2_Canciones.Remove(cancion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 
        }
    }
}