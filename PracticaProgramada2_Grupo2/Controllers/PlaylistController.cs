using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada2_Grupo2.Data;
using PracticaProgramada2_Grupo2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticaProgramada2_Grupo2.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly MinombredeconexionDbContext _context;

        public PlaylistController(MinombredeconexionDbContext context)
        {
            _context = context;
        }

        // GET: Playlist
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var playlists = await _context.G2_Playlists.ToListAsync();
            return View(playlists);
        }

        // GET: Playlist/{id}
        public async Task<IActionResult> Detalle(int id)
        {
            var playlist = await _context.G2_Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound("La playlist no existe en el sistema. ");
            }

            return View(playlist);
        } 

        // GET: Playlist/Agregar
        public IActionResult Agregar()
        {
            return View();
        }

        // POST: Playlist/Agregar
        [HttpPost]
        public async Task<IActionResult> Agregar(PlaylistModel nuevaPlaylist)
        {
            if (string.IsNullOrWhiteSpace(nuevaPlaylist.Nombre_Playlist) || string.IsNullOrWhiteSpace(nuevaPlaylist.Nombre_Creador))
            {
                ModelState.AddModelError("", "La información de la playlist no es válida. ");
                return View(nuevaPlaylist);
            }

            _context.G2_Playlists.Add(nuevaPlaylist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Playlist/Editar/{id}
        public async Task<IActionResult> Editar(int id)
        {
            var playlist = await _context.G2_Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound("La playlist no existe en el sistema. ");
            }
            return View(playlist);
        }

        // POST: Playlist/Editar/{id}
        [HttpPost]
        public async Task<IActionResult> Editar(int id, PlaylistModel playlistActualizada) 
        {
            if (id != playlistActualizada.Id_Playlist) 
            {
                return BadRequest("El ID de la playlist no coincide. ");
            }

            if (string.IsNullOrWhiteSpace(playlistActualizada.Nombre_Playlist) || string.IsNullOrWhiteSpace(playlistActualizada.Nombre_Creador))
            {
                ModelState.AddModelError("", "La información de la playlist no es válida. ");
                return View(playlistActualizada);
            }

            var playlist = await _context.G2_Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound("La playlist no existe en el sistema. ");
            }

            // Actualizar los datos de la playlist
            playlist.Nombre_Playlist = playlistActualizada.Nombre_Playlist;
            playlist.Nombre_Creador = playlistActualizada.Nombre_Creador;
            playlist.Descripcion_Playlist = playlistActualizada.Descripcion_Playlist;
            playlist.Genero_Playlist = playlistActualizada.Genero_Playlist;

            _context.Entry(playlist).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Playlist/Eliminar/{id}
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var playlist = await _context.G2_Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound("La playlisy no existe en el sistema. ");
            }

            _context.G2_Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
