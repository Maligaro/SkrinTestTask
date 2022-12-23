using SkrinTestTask.Model.Entities;
using SkrinTestTask.Repositories.Interfaces;

namespace SkrinTestTask.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public void CreateUser(User user)
        {
            using (var db = new ApplicationContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public User GetUserById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user is null)
                    throw new Exception($"There is no user with id = \"{id}\" in the database");
                return user;
            }
        }
    }
}
