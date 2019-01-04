using Blog.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>()
            //.HasMany(e => e.Claims)
            //.WithOne()
            //.HasForeignKey(e => e.UserId)
            //.IsRequired()
            //.OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ApplicationUser>()
            //  .HasMany(e => e.Logins)
            //  .WithOne()
            //  .HasForeignKey(e => e.UserId)
            //  .IsRequired()
            //  .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ApplicationUser>()
            //  .HasMany(e => e.UserRoles)
            //  .WithOne()
            //  .HasForeignKey(e => e.Id)
            //  .IsRequired()
            //  .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<IdentityUserLogin<string>>().HasKey("userId");
        }


        public DbSet<Post> Posts { get; set; }



    }
}
