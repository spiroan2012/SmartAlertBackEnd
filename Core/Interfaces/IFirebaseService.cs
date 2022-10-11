using Core.Entities;


namespace Core.Interfaces
{
    public interface IFirebaseService
    {
        public Task<IReadOnlyList<FirebaseUser>> GetAllUsers();
    }
}
