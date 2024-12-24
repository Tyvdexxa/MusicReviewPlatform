using Microsoft.AspNetCore.Mvc;
using MusicReviewPlatform.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MusicReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public ArtistsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await Task.Run(() => _context.Artists.ToList());
            return Ok(artists);
        }

        // GET: api/Artists/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(int id)
        {
            var artist = await Task.Run(() => _context.Artists.Find(id));
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<IActionResult> CreateArtist([FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Task.Run(() => _context.Artists.Add(artist));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistId }, artist);
        }

        // PUT: api/Artists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] Artist artist)
        {
            if (id != artist.ArtistId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingArtist = await Task.Run(() => _context.Artists.Find(id));
            if (existingArtist == null)
            {
                return NotFound();
            }

            existingArtist.Name = artist.Name;
            existingArtist.Genre = artist.Genre;
            existingArtist.Country = artist.Country;

            _context.Artists.Update(existingArtist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Artists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await Task.Run(() => _context.Artists.Find(id));
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
