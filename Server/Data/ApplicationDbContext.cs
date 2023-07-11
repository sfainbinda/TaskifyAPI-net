using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}

		public DbSet<User> Users { get; set; }
	}
}
