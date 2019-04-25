using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_reg.Repos
{
    class UserprofRepos:IRepository<User_profile>
    {
        private Model1 db;

        public UserprofRepos(Model1 context)
        {
            this.db = context;
        }

        public IEnumerable<User_profile> GetAll()
        {
            return db.User_profile;
        }

        public IEnumerable<User_profile> Sort()
        {
            return db.User_profile.OrderBy(c => c.Name);
        }
        public IEnumerable<User_profile> SortRoom()
        {
            return db.User_profile.OrderBy(c => c.room_num);
        }
        public User_profile Get(int id)
        {
            return db.User_profile.Find(id);
        }

        public void Create(User_profile user)
        {
            db.User_profile.Add(user);
        }

        public void Update(User_profile user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(User_profile user)
        {
                db.User_profile.Remove(user);
        }
        public bool IsExist(int id)
        {
            if (db.User_profile.Any(c => c.user_id.Equals(id)))
                return true;
            else return false;
        }
        public void Delete(int id)
        {
            User_profile us_prof = db.User_profile.Find(id);
            if (us_prof != null)
                db.User_profile.Remove(us_prof);
        }
    }
}

