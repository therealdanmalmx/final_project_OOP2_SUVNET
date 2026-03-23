namespace API.DTO
{
    public class GetCourierDTO
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public List<Guid>? Orders { get; set; }
    }
}