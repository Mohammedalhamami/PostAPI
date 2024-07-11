using Microsoft.EntityFrameworkCore;
using Posts.API.Models.Domain;
using System.Data.Common;

namespace Posts.API.Data
{
	public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Post>().Property(p => p.DateTimePosted).HasDefaultValueSql("get_current_datetime_utc()");
		}
		public DbSet<Post> Posts { get; set; }
    }
}
