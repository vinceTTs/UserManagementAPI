using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetByIndex(int index);
        void Add(User user);
        void Update(int index, User user);
        void Delete(int index);
    }
}