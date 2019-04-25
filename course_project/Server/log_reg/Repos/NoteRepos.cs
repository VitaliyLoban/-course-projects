using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_reg.Repos
{
    class NoteRepos : IRepository<Note>
    {
        private Model1 db;

        public NoteRepos(Model1 context)
        {
            this.db = context;
        }

        public IEnumerable<Note> GetAll()
        {
            return db.Notes;
        }

        public Note Get(int id)
        {
            return db.Notes.Find(id);
        }

        public void Create(Note user_a)
        {
            db.Notes.Add(user_a);
        }

        public void Update(Note user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(Note user)
        {
            db.Notes.Remove(user);
        }
        public void Delete(int id)
        {
            Note us_a = db.Notes.Find(id);
            if (us_a != null)
                db.Notes.Remove(us_a);
        }
    }
}
