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
    public class DownloadsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public DownloadsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Downloads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Download>>> GetDownloads()
        {
            return await _context.Downloads
                .Include(d => d.User)
                .Include(d => d.Song)
                .ToListAsync();
        }

        // GET: api/Downloads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Download>> GetDownload(int id)
        {
            var download = await _context.Downloads
                .Include(d => d.User)
                .Include(d => d.Song)
                .FirstOrDefaultAsync(d => d.DownloadId == id);

            if (download == null)
            {
                return NotFound();
            }

            return download;
        }

        // POST: api/Downloads
        [HttpPost]
        public async Task<ActionResult<Download>> PostDownload(Download download)
        {
            if (!_context.Users.Any(u => u.UserId == download.UserId))
            {
                return BadRequest("Invalid UserID.");
            }

            if (!_context.Songs.Any(s => s.SongId == download.SongId))
            {
                return BadRequest("Invalid SongID.");
            }

            _context.Downloads.Add(download);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDownload), new { id = download.DownloadId }, download);
        }

        // DELETE: api/Downloads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDownload(int id)
        {
            var download = await _context.Downloads.FindAsync(id);
            if (download == null)
            {
                return NotFound();
            }

            _context.Downloads.Remove(download);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
