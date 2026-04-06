namespace API.DTO
{
    public class AddNewReviewDTO
    {
        public float Score { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid? OrderId { get; set; }
        public Guid? RestaurantId { get; set; }
    }
}