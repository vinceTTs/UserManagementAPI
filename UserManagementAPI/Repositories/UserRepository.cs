using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public IEnumerable<User> GetAll() => _users;

        public User? GetByIndex(int index) => index >= 0 && index < _users.Count ? _users[index] : null;

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Update(int index, User user)
        {
            if (index >= 0 && index < _users.Count)
            {
                _users[index].Name = user.Name;
                _users[index].Email = user.Email;
            }
        }

        public void Delete(int index)
        {
            if (index >= 0 && index < _users.Count)
            {
                _users.RemoveAt(index);
            }
        }
    }
}