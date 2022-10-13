namespace Api.Dtos
{
    public class AdminReportDto
    {
        public int NumberOfIncidentsProcessed { get; set; }
        public int NumberOfIncidentsAccepted { get; set; }
        public int NumberOfIncidentsRejected { get; set; }
        public int NumberOfIncidentsPending { get; set; }
        public int TotalOfIncidentsSubmited { get; set; }
    }
}
