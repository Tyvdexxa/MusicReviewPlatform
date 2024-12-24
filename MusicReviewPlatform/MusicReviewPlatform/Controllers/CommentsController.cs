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
    public class CommentsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public CommentsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Review)
                .ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Review)
                .FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            if (!_context.Users.Any(u => u.UserId == comment.UserId))
            {
                return BadRequest("Invalid UserID.");
            }

            if (!_context.Reviews.Any(r => r.ReviewId == comment.ReviewId))
            {
                return BadRequest("Invalid ReviewID.");
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
