namespace Api.Dtos
{
    public class AdminReportResponse
    {
        public bool Success { get; set; }
        public DateTime RequestedAt { get; set; }
        public AdminReportDto AdminReport { get; set; }
    }
}
