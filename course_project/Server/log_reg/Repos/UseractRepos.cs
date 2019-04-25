using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_reg.Repos
{
    class UseractRepos : IRepository<User_activity>
    {
        private Model1 db;

        public UseractRepos(Model1 context)
        {
            this.db = context;
        }

        public IEnumerable<User_activity> GetAll()
        {
            return db.User_activity;
        }

        public User_activity Get(int id)
        {
            return db.User_activity.Find(id);
        }

        public void Create(User_activity user_a)
        {
            db.User_activity.Add(user_a);
        }

        public void Update(User_activity user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(User_activity user)
        {
                db.User_activity.Remove(user);
        }
        public void Delete(int id)
        {
            User_activity us_a = db.User_activity.Find(id);
            if (us_a != null)
                db.User_activity.Remove(us_a);
        }
    }
}
