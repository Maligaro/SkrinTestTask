using SkrinTestTask.Model.Entities;

namespace SkrinTestTask.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void SaveUser(User user);
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
    }
}
