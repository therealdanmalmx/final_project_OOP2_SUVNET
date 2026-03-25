using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO.Review;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReviewController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Review>> AddNewReview([FromBody] AddNewReviewDTO newReview)
        {
            if (newReview.Score <= 0)
            {
                return BadRequest("Score has to be between 1 and 5");
            }

            if (newReview.OrderId == null || newReview.OrderId == Guid.Empty)
            {
                return BadRequest("OrderId cannot be empty");
            }

            var review = new Review
            {
                Score = newReview.Score,
                Comment = newReview.Comment,
                OrderId = newReview.OrderId,
            };

            if (review is null)
            {
                return BadRequest("Could not create new review");
            }

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            return Created("/api/review", review);
        }
    }
}