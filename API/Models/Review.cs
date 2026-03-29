namespace API.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public float Score { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid? OrderId { get; set; }
        public Guid? RestaurantId { get; set; }
    }
}