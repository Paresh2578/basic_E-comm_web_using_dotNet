using bulkyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace bulkyApp.Data
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<CategoryModel> categories { get; set; }
		public DbSet<ProductModel> products { get; set; }
		public DbSet<UserModel> users { get; set; }
		public DbSet<CardModel> cards { get; set; }
	}
}
