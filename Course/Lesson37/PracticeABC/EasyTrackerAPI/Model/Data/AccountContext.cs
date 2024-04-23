using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class AccountContext :  IdentityDbContext<IdentityUser>
{
 
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
        Console.WriteLine("AccountContext created");
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.Entity<User>().HasKey(u=>u.Email);
    }
}