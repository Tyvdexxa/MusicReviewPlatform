using Microsoft.AspNetCore.Mvc;
using MusicReviewPlatform.Models;

using System.Threading.Tasks;
using System.Linq;

namespace MusicReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MusicReviewPlatformContext _context;

        public ReviewsController(MusicReviewPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await Task.Run(() => _context.Reviews.ToList());
            return Ok(reviews);
        }

        // GET: api/Reviews/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var review = await Task.Run(() => _context.Reviews.Find(id));
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Task.Run(() => _context.Reviews.Add(review));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
        }

        // PUT: api/Reviews/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            if (id != review.ReviewId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingReview = await Task.Run(() => _context.Reviews.Find(id));
            if (existingReview == null)
            {
                return NotFound();
            }

            existingReview.Rating = review.Rating;
            existingReview.ReviewText = review.ReviewText;
            existingReview.ReviewDate = review.ReviewDate;
            existingReview.AlbumId = review.AlbumId;
            existingReview.UserId = review.UserId;

            _context.Reviews.Update(existingReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Reviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await Task.Run(() => _context.Reviews.Find(id));
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
