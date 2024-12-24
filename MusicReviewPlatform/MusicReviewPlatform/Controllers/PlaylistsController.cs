using Microsoft.AspNetCore.Mvc;
using MusicReviewPlatform.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MusicReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public PlaylistsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<IActionResult> GetPlaylists()
        {
            var playlists = await Task.Run(() => _context.Playlists.ToList());
            return Ok(playlists);
        }

        // GET: api/Playlists/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylist(int id)
        {
            var playlist = await Task.Run(() => _context.Playlists.Find(id));
            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist);
        }

        // POST: api/Playlists
        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromBody] Playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Task.Run(() => _context.Playlists.Add(playlist));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlaylist), new { id = playlist.PlaylistId }, playlist);
        }

        // PUT: api/Playlists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] Playlist playlist)
        {
            if (id != playlist.PlaylistId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPlaylist = await Task.Run(() => _context.Playlists.Find(id));
            if (existingPlaylist == null)
            {
                return NotFound();
            }

            existingPlaylist.Name = playlist.Name;
            existingPlaylist.UserId = playlist.UserId;
            existingPlaylist.CreationDate = playlist.CreationDate;

            _context.Playlists.Update(existingPlaylist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Playlists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await Task.Run(() => _context.Playlists.Find(id));
            if (playlist == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}