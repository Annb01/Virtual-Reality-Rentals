using Microsoft.EntityFrameworkCore;
using System.Data.Common;
namespace VR_Rentals.Models
{
    public class RentalContext : DbContext
    {
        public RentalContext(DbContextOptions<RentalContext> options) : base(options) { }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<VrEquipment> VrEquipments { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .HasKey(r => r.RentalId);

            modelBuilder.Entity<VrEquipment>()
                .HasKey(r => r.EquipmentId);

            modelBuilder.Entity<Customer>()
                .HasKey(r => r.CustomerId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.VrEquipment)
                .WithMany(e => e.Rentals)
                .HasForeignKey(r => r.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
