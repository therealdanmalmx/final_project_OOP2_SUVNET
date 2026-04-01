using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IReviewService
    {
        Task<List<Models.Review>> GetAllReviews();
        Task<List<Models.Review>> GetAllReviewByRestaurantId(Guid restaurantId);
        Task<Models.Review> GetAllReviewByOrderId(Guid orderId);
    }
}