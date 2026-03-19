namespace API.DTO.OrderItem
{
    public class GetOrderItemDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}