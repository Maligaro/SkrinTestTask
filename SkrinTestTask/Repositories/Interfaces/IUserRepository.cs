using SkrinTestTask.Model.Entities;

namespace SkrinTestTask.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void CreateUser(User user);
        public User GetUserById(int id);
    }
}
