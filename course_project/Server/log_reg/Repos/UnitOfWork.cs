using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log_reg.Repos
{
    class UnitOfWork : IDisposable
    {
        private Model1 db = new Model1();
        private UserRepos userRepos;
        private UserprofRepos userprofRepos;
        private UseractRepos useractRepos;
        private UserworkRepos userworkRepos;
        private NoteRepos noteRepos;

        public UserRepos us_rep
        {
            get
            {
                if (userRepos == null)
                    userRepos = new UserRepos(db);
                return userRepos;
            }
        }
        public UseractRepos usact_rep
        {
            get
            {
                if (useractRepos == null)
                    useractRepos = new UseractRepos(db);
                return useractRepos;
            }
        }
        public UserprofRepos usprof_rep
        {
            get
            {
                if (userprofRepos == null)
                    userprofRepos = new UserprofRepos(db);
                return userprofRepos;
            }
        }
        public UserworkRepos uswork_rep
        {
            get
            {
                if (userworkRepos == null)
                    userworkRepos = new UserworkRepos(db);
                return userworkRepos;
            }
        }
        public NoteRepos note_rep
        {
            get
            {
                if (noteRepos == null)
                    noteRepos = new NoteRepos(db);
                return noteRepos;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
