using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasOne(r => r.roomCategory)
                .WithMany(c => c.rooms)
                .HasForeignKey(r => r.category_id);

            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.client)
                .WithMany()
                .HasForeignKey(res => res.client_id);

            modelBuilder.Entity<Reservation>()
                .HasOne(res => res.room)
                .WithMany()
                .HasForeignKey(res => res.room_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}