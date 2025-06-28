using AdminApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;
namespace AdminApplication.Tests;
using AdminApplication.Models;

public class HotelDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=HotelManagement;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}