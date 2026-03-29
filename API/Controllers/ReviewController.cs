using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO.Review;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<Review>> GetAllReviewByRestaurantId(Guid restaurantId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.RestaurantId == restaurantId).ToListAsync();
            if (reviews is null)
            {
                return NotFound("No reviews exist");
            }

            return Ok(reviews);
        }


        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<Review>> GetAllReviewByOrderId(Guid orderId)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.OrderId == orderId);
            if (review is null)
            {
                return NotFound("No reviews exist");
            }

            return Ok(review);
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
                RestaurantId = newReview.RestaurantId
            };

            if (review is null)
            {
                return BadRequest("Could not create new review");
            }

            var exists = await _dbContext.Reviews.AnyAsync(r => r.OrderId == newReview.OrderId);
            if (exists)
            {
                return Conflict("Review already exists for this order.");
            }

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            var avgScore = await _dbContext.Reviews
                .Where(r => r.RestaurantId == review.RestaurantId)
                .AverageAsync(r => r.Score);

            var restaurant = await _dbContext.Restaurants
                .FirstOrDefaultAsync(r => r.Id == review.RestaurantId);

            if (restaurant is not null)
            {
                restaurant.Review = avgScore;
                await _dbContext.SaveChangesAsync();
            }

            return Created("/api/review", review);

        }
    }
}