namespace Api.Dtos
{
    public class IncidentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int IncidentCount { get; set; }
        public double Grade { get; set; }
    }
}
