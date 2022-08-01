using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Talabalar { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}
}