using System.Data.Entity;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class TheatreUZContext : DbContext
    {
        public TheatreUZContext() : base("TheatreUZDB")
        {
            Database.SetInitializer(new TheatreUZInitializer());
        }

        public DbSet<State> States { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Spectacle> Spectacles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<State>()
                .Property(d => d.RegDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Role>()
                .Property(d => d.RegDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<User>()
                .Property(d => d.RegDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Notification>()
                .Property(d => d.RegDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Genre>()
                .Property(d => d.RegDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Spectacle>()
                .Property(d => d.PlayDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Spectacle>()
                .Property(d => d.RegDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Sale>()
                .Property(d => d.RegDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Role>()
                .HasRequired(t => t.State)
                .WithMany(t => t.Roles)
                .HasForeignKey(d => d.StateID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasRequired(t => t.State)
                .WithMany(t => t.Genres)
                .HasForeignKey(d => d.StateID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasRequired(t => t.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasRequired(t => t.State)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.StateID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Spectacle>()
                .HasRequired(t => t.Genre)
                .WithMany(t => t.Spectacles)
                .HasForeignKey(d => d.GenreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Spectacle>()
                .HasRequired(t => t.State)
                .WithMany(t => t.Spectacles)
                .HasForeignKey(d => d.StateID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasRequired(t => t.User)
                .WithMany(t => t.Sales)
                .HasForeignKey(d => d.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasRequired(t => t.Spectacle)
                .WithMany(t => t.Sales)
                .HasForeignKey(d => d.SpectacleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasRequired(t => t.State)
                .WithMany(t => t.Sales)
                .HasForeignKey(d => d.StateID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(t => t.User)
                .WithMany(t => t.Notifications)
                .HasForeignKey(d => d.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(t => t.State)
                .WithMany(t => t.Notifications)
                .HasForeignKey(d => d.StateID)
                .WillCascadeOnDelete(false);
        }
    }
}