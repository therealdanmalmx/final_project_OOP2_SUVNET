using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO.Review;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<AddNewReviewDTO>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviews();
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
                var reviews = await _reviewService.GetAllReviewByRestaurantId(restaurantId);
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
                var review = await _reviewService.GetAllReviewByOrderId(orderId);
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