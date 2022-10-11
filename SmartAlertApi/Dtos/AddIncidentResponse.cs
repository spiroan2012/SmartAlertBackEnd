namespace Api.Dtos
{
    public class AddIncidentResponse
    {
        public bool Success { get; set; }
        public DateTime RequestedAt { get; set; }
        public string? Message { get; set; }
    }
}
