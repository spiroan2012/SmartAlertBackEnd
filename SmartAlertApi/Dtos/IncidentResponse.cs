namespace Api.Dtos
{
    public class IncidentResponse
    {
        public bool Success { get; set; }
        public DateTime RequestedAt { get; set; }
        public int Results { get; set; }
        public IncidentData Data { get; set; }
    }

    public class IncidentData
    {
        public IncidentDto[] Incidents { get; set; }
        public int MyProperty { get; set; }
    }

}
