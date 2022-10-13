namespace Api.Dtos
{
    public class UserReportResponse
    {
        public bool Success { get; set; }
        public DateTime RequestedAt { get; set; }
        public UserReportDto UserReport { get; set; }
    }

}
