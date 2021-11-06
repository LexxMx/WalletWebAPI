using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WalletWebAPI.Repositories {
	public class TransactionCategoryRepository : ITransactionCategoryRepository {

		private ApplicationContext appDBContext;

		public TransactionCategoryRepository(ApplicationContext dbContext) {
			appDBContext = dbContext;
		}

		public void Create(TransactionCategory item) {
			appDBContext.TransactionCategories.Add(item);
			appDBContext.SaveChanges();
		}

		public void Delete(int id) {
			var category = appDBContext.TransactionCategories.FirstOrDefault(x => x.Id == id);
			if (category != null) {
				category.IsDeleted = true;
				appDBContext.TransactionCategories.Update(category);
				appDBContext.SaveChanges();
			}
		}

		public IEnumerable<TransactionCategory> Find(Expression<Func<TransactionCategory, bool>> predicate) {
			return appDBContext.TransactionCategories.Include(x => x.Transactions).Where(x => !x.IsDeleted).Where(predicate).ToList();
		}

		public TransactionCategory Get(int id) {
			return appDBContext.TransactionCategories.Include(x => x.Transactions).Where(x => !x.IsDeleted && x.Id == id)?.FirstOrDefault();
		}

		public IEnumerable<TransactionCategory> GetAll() {
			return appDBContext.TransactionCategories.Include(x => x.Transactions).Where(x => !x.IsDeleted).ToList();
		}

		public void Update(TransactionCategory item) {
			appDBContext.TransactionCategories.Update(item);
			appDBContext.SaveChanges();
		}
	}
}
