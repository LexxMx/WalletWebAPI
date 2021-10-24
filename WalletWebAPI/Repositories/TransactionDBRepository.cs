using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Entities;

namespace WalletWebAPI.Repositories {
	public class TransactionDBRepository : ITransactionRepository {
		private ApplicationContext appDBContext;

		public TransactionDBRepository(ApplicationContext dbContext) {
			appDBContext = dbContext;
		}

		public IEnumerable<Transaction> GetAll() {
			return appDBContext.Transactions.ToList();
		}

		public Transaction Get(int id) {
			return appDBContext.Transactions.Where(x => x.Id == id)?.FirstOrDefault();
		}

		public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> predicate) {
			return (appDBContext.Transactions.Select(x => x).Where(predicate)).ToList();
		}

		public void Create(Transaction item) {
			appDBContext.Transactions.Add(item);
			appDBContext.SaveChanges();
		}

		public void Update(Transaction item) {
			appDBContext.Transactions.Update(item);
			appDBContext.SaveChanges();
		}

		public void Delete(int id) {
			var transaction = appDBContext.Transactions.FirstOrDefault(x => x.Id == id);
			if (transaction != null) {
				appDBContext.Transactions.Remove(transaction);
				appDBContext.SaveChanges();
			}
		}

		public decimal GetSumTransaction()
		{
			return appDBContext.Transactions.Sum(x => x.Amount);
		}
	}
}
