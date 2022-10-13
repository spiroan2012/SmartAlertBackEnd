namespace Api.Dtos
{
    public class UserReportDto
    {
        public int NumberOfMasterIncidentsReported { get; set; }
        public int NumberOfMasterIncidentsAccepted { get; set; }
        public int NumberOfMasterIncidentsRejected { get; set; }
        public int TotalIncidentsReported { get; set; }
        public int NumberOfSmsReceived { get; set; }
    }
}
