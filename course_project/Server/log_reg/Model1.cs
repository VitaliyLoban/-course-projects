namespace log_reg
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_activity> User_activity { get; set; }
        public virtual DbSet<User_profile> User_profile { get; set; }
        public virtual DbSet<user_work> user_work { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.User_activity)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.id_stud);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.User_profile)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasMany(e => e.user_work)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.stud_id)
                .WillCascadeOnDelete(false);
        }
    }
}
