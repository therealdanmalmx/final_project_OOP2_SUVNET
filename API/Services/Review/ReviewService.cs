using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO.Review;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Review
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _dbContext;

        public ReviewService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Review>> GetAllReviews()
        {
            var reviews = await _dbContext.Reviews.ToListAsync();

            if (reviews is null)
            {
                throw new NullReferenceException("Can't find any reviews");

            }

            return reviews;
        }
        public async Task<List<Models.Review>> GetAllReviewByRestaurantId(Guid restaurantId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.RestaurantId == restaurantId).ToListAsync();
            if (reviews is null)
            {
                throw new NullReferenceException($"Can't find any review with restaurnt id {restaurantId}");
            }

            return reviews;
        }

        public async Task<Models.Review> GetAllReviewByOrderId(Guid orderId)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.OrderId == orderId);
            if (review is null)
            {
                throw new NullReferenceException($"Can't find any review with order id {orderId}");
            }

            return review;
        }

        public async Task<Models.Review> AddNewReview(AddNewReviewDTO newReview)
        {
            if (newReview.Score <= 0)
            {
                throw new ArgumentOutOfRangeException("Score has to be between 1 and 5");
            }

            if (newReview.OrderId == null || newReview.OrderId == Guid.Empty)
            {
                throw new ArgumentNullException("OrderId cannot be empty");
            }

            var review = new Models.Review
            {
                Score = newReview.Score,
                Comment = newReview.Comment,
                OrderId = newReview.OrderId,
                RestaurantId = newReview.RestaurantId
            };

            if (review is null)
            {
                throw new ArgumentOutOfRangeException("Could not create new review");
            }

            var exists = await _dbContext.Reviews.AnyAsync(r => r.OrderId == newReview.OrderId);
            if (exists)
            {
                throw new ApplicationException("Review already exists for this order.");
            }

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            return review;

        }
    }
}