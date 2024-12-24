using Microsoft.AspNetCore.Mvc;
using MusicReviewPlatform.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MusicReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public AlbumsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Albums
        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await Task.Run(() => _context.Albums.ToList());
            return Ok(albums);
        }

        // GET: api/Albums/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var album = await Task.Run(() => _context.Albums.Find(id));
            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // POST: api/Albums
        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Task.Run(() => _context.Albums.Add(album));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, album);
        }

        // PUT: api/Albums/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(int id, [FromBody] Album album)
        {
            if (id != album.AlbumId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingAlbum = await Task.Run(() => _context.Albums.Find(id));
            if (existingAlbum == null)
            {
                return NotFound();
            }

            existingAlbum.Title = album.Title;
            existingAlbum.ReleaseDate = album.ReleaseDate;
            existingAlbum.ArtistId = album.ArtistId;

            _context.Albums.Update(existingAlbum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Albums/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await Task.Run(() => _context.Albums.Find(id));
            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
