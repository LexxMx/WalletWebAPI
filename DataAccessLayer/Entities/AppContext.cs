using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Etities {
	public class ApplicationContext : DbContext {
		public DbSet<Transaction> Transactions { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
			Database.EnsureCreated();
		}
	}
}
