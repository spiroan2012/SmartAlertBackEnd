using Core.Entities;


namespace Core.Interfaces
{
    public interface IFirebaseService
    {
        public Task<Dictionary<string, FirebaseUser>> GetAllUsers();
    }
}
