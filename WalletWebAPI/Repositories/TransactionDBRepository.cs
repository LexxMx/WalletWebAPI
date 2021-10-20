using DataAccessLayer.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletWebAPI.Models;

namespace WalletWebAPI.Repositories {
	public class TransactionDBRepository : ITransactionRepository {
		private ApplicationContext appDBContext;

		public TransactionDBRepository(ApplicationContext dbContext) {
			appDBContext = dbContext;
		}

		public IQueryable<TransactionModel> GetAll() {
			return appDBContext.Transactions.Select(x => new TransactionModel {
				Id = x.Id,
				Amount = x.Amount,
				DayTransaction = x.DayTransaction,
				Diraction = (TransactionDirectionEnum)x.Diraction
			});
		}

		public TransactionModel Get(int id) {
			return appDBContext.Transactions.Select(x => new TransactionModel {
				Id = x.Id,
				Amount = x.Amount,
				DayTransaction = x.DayTransaction,
				Diraction = (TransactionDirectionEnum)x.Diraction
			}).Where(x => x.Id == id)?.FirstOrDefault();
		}

		public IQueryable<TransactionModel> Find(Func<TransactionModel, bool> predicate) {
			return appDBContext.Transactions.Where(x => predicate(MapTransactionModel(x))).Select(x => MapTransactionModel(x));
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

		public decimal GetSumTransaction()
		{
			return appDBContext.Transactions.Sum(x => x.Amount);
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
