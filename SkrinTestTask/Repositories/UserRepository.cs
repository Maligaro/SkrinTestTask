using SkrinTestTask.Model.Entities;
using SkrinTestTask.Repositories.Interfaces;

namespace SkrinTestTask.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public void SaveUser(User user)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Users.Any(o => o.Email.Equals(user.Email)))
                    db.Users.Update(user);
                else
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

        public User GetUserByEmail(string emial)
        {
            using (var db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email.Equals(emial));
                if (user is null)
                    throw new Exception($"There is no user with name = \"{emial}\" in the database");
                return user;
            }
        }
    }
}
