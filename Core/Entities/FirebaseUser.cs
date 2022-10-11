namespace Core.Entities
{
    public class FirebaseUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public long CreatedOn { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long LocationUpdatedOn { get; set; }
        public bool CivilDefenceEmployee { get; set; }
    }
}
