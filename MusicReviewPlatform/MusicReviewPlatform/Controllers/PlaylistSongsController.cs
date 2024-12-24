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
    public class PlaylistSongsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public PlaylistSongsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/PlaylistSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistSong>>> GetPlaylistSongs()
        {
            return await _context.PlaylistSongs
                .Include(ps => ps.Playlist)
                .Include(ps => ps.Song)
                .ToListAsync();
        }

        // GET: api/PlaylistSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistSong>> GetPlaylistSong(int id)
        {
            var playlistSong = await _context.PlaylistSongs
                .Include(ps => ps.Playlist)
                .Include(ps => ps.Song)
                .FirstOrDefaultAsync(ps => ps.PlaylistSongId == id);

            if (playlistSong == null)
            {
                return NotFound();
            }

            return playlistSong;
        }

        // POST: api/PlaylistSongs
        [HttpPost]
        public async Task<ActionResult<PlaylistSong>> PostPlaylistSong(PlaylistSong playlistSong)
        {
            if (!_context.Playlists.Any(p => p.PlaylistId == playlistSong.PlaylistId))
            {
                return BadRequest("Invalid PlaylistID.");
            }

            if (!_context.Songs.Any(s => s.SongId == playlistSong.SongId))
            {
                return BadRequest("Invalid SongID.");
            }

            _context.PlaylistSongs.Add(playlistSong);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlaylistSong), new { id = playlistSong.PlaylistSongId }, playlistSong);
        }

        // DELETE: api/PlaylistSongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylistSong(int id)
        {
            var playlistSong = await _context.PlaylistSongs.FindAsync(id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            _context.PlaylistSongs.Remove(playlistSong);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
