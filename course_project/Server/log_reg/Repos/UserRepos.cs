using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_reg.Repos
{
    class UserRepos:IRepository<User>
    {
        private Model1 db;

        public UserRepos(Model1 context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public User FindLog(string log)
        {
            return db.Users.Where(c => c.login.Equals(log)).First();
        }
        public void Create(User user)
        {
            db.Users.Add(user);
        }
        
        public bool IsExist(string f2)
        {
            if (db.Users.Any(c => c.login.Equals(f2)))
                return true;
            else return false;
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User us = db.Users.Find(id);
            if (us != null)
                db.Users.Remove(us);
        }
    }
}
