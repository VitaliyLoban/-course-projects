using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_reg.Repos
{
    class UserworkRepos : IRepository<user_work>
    {
        private Model1 db;

        public UserworkRepos(Model1 context)
        {
            this.db = context;
        }

        public IEnumerable<user_work> GetAll()
        {
            return db.user_work;
        }

        public user_work Get(int id)
        {
            return db.user_work.Find(id);
        }

        public void Create(user_work user_a)
        {
            db.user_work.Add(user_a);
        }

        public void Update(user_work user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(user_work user)
        {
                db.user_work.Remove(user);
        }
        public void Delete(int id)
        {
            user_work us_a = db.user_work.Find(id);
            if (us_a != null)
                db.user_work.Remove(us_a);
        }
    }
}
