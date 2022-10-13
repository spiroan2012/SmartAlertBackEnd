namespace Api.Dtos
{
    public class UserReportDto
    {
        public int NumberOfIncidentsAccepted { get; set; }
        public int NumberOfIncidentsPending { get; set; }
        public int NumberOfIncidentsRejected { get; set; }
        public int TotalIncidentsReported { get; set; }
        public int NumberOfSmsReceived { get; set; }
    }
}
