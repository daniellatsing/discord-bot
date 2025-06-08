using Microsoft.EntityFrameworkCore;
using BirthdayBot.Models;

namespace BirthdayBot.Data
{
  public class AppDbContext : DbContext
  {
    public DbSet<UserBirthday> UserBirthdays => Set<UserBirthday>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  }
}
