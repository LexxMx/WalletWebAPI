using DataAccessLayer.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletWebAPI.Models;

namespace WalletWebAPI.Repositories {
	public class TransactionDBRepository : IRepository<TransactionModel> {
		private ApplicationContext appDBContext;

		public TransactionDBRepository(ApplicationContext dbContext) {
			appDBContext = dbContext;
		}

		public IEnumerable<TransactionModel> GetAll() {
			return appDBContext.Transactions.Select(x => MapTransactionModel(x));
		}

		public TransactionModel Get(int id) {
			return appDBContext.Transactions.Select(x => MapTransactionModel(x)).FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<TransactionModel> Find(Func<TransactionModel, bool> predicate) {
			return appDBContext.Transactions.Select(x => MapTransactionModel(x)).Where(predicate);
		}

		public void Create(TransactionModel item) {
			appDBContext.Transactions.Add(MapTransaction(item));
			appDBContext.SaveChanges();
		}

		public void Update(TransactionModel item) {
			appDBContext.Transactions.Update(MapTransaction(item));
			appDBContext.SaveChanges();
		}

		public void Delete(int id) {
			var transaction = appDBContext.Transactions.FirstOrDefault(x => x.Id == id);
			if (transaction != null) {
				appDBContext.Transactions.Remove(transaction);
				appDBContext.SaveChanges();
			}
		}

		public static TransactionModel MapTransactionModel(Transaction source) {
			return new TransactionModel {
				Id = source.Id,
				Amount = source.Amount,
				DayTransaction = source.DayTransaction,
				Diraction = (TransactionDirectionEnum)source.Diraction
			};
		}

		public static Transaction MapTransaction(TransactionModel source) {
			return new Transaction {
				Id = source.Id,
				Amount = source.Amount,
				DayTransaction = source.DayTransaction,
				Diraction = (TransactionDirection)source.Diraction
			};
		}
	}
}
