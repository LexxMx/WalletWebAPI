using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities {
	public class ApplicationContext : DbContext {
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<TransactionCategory> TransactionCategories { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
		}
	}
}