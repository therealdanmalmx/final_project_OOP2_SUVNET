using API.DTO;

namespace API.Services
{
    public interface IReviewService
    {
        Task<List<Models.Review>> GetAllReviews();
        Task<List<Models.Review>> GetAllReviewByRestaurantId(Guid restaurantId);
        Task<Models.Review> GetAllReviewByOrderId(Guid orderId);
        Task<Models.Review> AddNewReview(AddNewReviewDTO newReview);
    }
}