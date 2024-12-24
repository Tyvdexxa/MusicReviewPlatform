using Microsoft.AspNetCore.Mvc;
using MusicReviewPlatform.Models;

using System.Threading.Tasks;
using System.Linq;

namespace MusicReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public SongsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<IActionResult> GetSongs()
        {
            var songs = await Task.Run(() => _context.Songs.ToList());
            return Ok(songs);
        }

        // GET: api/Songs/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSong(int id)
        {
            var song = await Task.Run(() => _context.Songs.Find(id));
            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<IActionResult> CreateSong([FromBody] Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Task.Run(() => _context.Songs.Add(song));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSong), new { id = song.SongId }, song);
        }

        // PUT: api/Songs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, [FromBody] Song song)
        {
            if (id != song.SongId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingSong = await Task.Run(() => _context.Songs.Find(id));
            if (existingSong == null)
            {
                return NotFound();
            }

            existingSong.Title = song.Title;
            existingSong.Duration = song.Duration;
            existingSong.AlbumId = song.AlbumId;

            _context.Songs.Update(existingSong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Songs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await Task.Run(() => _context.Songs.Find(id));
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}