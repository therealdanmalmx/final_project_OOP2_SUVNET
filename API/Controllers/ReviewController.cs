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

        [HttpGet]
        public async Task<ActionResult<AddNewReviewDTO>> GetAllReviews()
        {
            try
            {
                var reviews = await _dbContext.Reviews.ToListAsync();
                return Ok(reviews);
            }

            catch (NullReferenceException ex)
            {
                return NotFound(new {error = ex.Message});
            }

        }
        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<Review>> GetAllReviewByRestaurantId(Guid restaurantId)
        {
            try
            {
                var reviews = await _dbContext.Reviews.Where(r => r.RestaurantId == restaurantId).ToListAsync();
                return Ok(reviews);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new {error = ex.Message});
            }

        }


        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<Review>> GetAllReviewByOrderId(Guid orderId)
        {
            try
            {
                var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.OrderId == orderId);
                return Ok(review);
            }

            catch (NullReferenceException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }


        [HttpPost]
        public async Task<ActionResult<Review>> AddNewReview([FromBody] AddNewReviewDTO newReview)
        {
            try
            {
                var review = new Review
                {
                    Score = newReview.Score,
                    Comment = newReview.Comment,
                    OrderId = newReview.OrderId,
                    RestaurantId = newReview.RestaurantId
                };

                _dbContext.Reviews.Add(review);
                await _dbContext.SaveChangesAsync();

                return Created("/api/review", review);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                 return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred: ", ex });

            }

        }
    }
}