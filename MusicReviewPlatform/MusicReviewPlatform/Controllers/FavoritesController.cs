using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicReviewPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public FavoritesController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
        {
            return await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Song)
                .ToListAsync();
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavorite(int id)
        {
            var favorite = await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Song)
                .FirstOrDefaultAsync(f => f.FavoriteId == id);

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }

        // POST: api/Favorites
        [HttpPost]
        public async Task<ActionResult<Favorite>> PostFavorite(Favorite favorite)
        {
            if (!_context.Users.Any(u => u.UserId == favorite.UserId))
            {
                return BadRequest("Invalid UserID.");
            }

            if (!_context.Songs.Any(s => s.SongId== favorite.SongId))
            {
                return BadRequest("Invalid SongID.");
            }

            // Проверка на существующую запись
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.SongId == favorite.SongId);

            if (existingFavorite != null)
            {
                return Conflict("This song is already in the user's favorites.");
            }

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavorite), new { id = favorite.FavoriteId }, favorite);
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
