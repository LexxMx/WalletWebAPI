using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using DataAccessLayer.Entities;

namespace WalletWebAPI.Repositories {
	public class TransactionFileRepository : ITransactionRepository {

		private List<Transaction> transactions;
		private string transactionsFilePath = "transactions.json";

		public TransactionFileRepository() {
			var fileValue = File.ReadAllText(transactionsFilePath);

			if (!string.IsNullOrEmpty(fileValue)) {
				transactions = JsonSerializer.Deserialize<List<Transaction>>(fileValue);
			} else {
				transactions = new List<Transaction>();
			}
		}

		public IEnumerable<Transaction> GetAll() {
			return transactions;
		}

		public Transaction Get(int id) {
			return transactions.FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> predicate) {
			return transactions.Where(predicate.Compile());
		}

		public void Create(Transaction item) {
			if (item.Id == 0) {
				item.Id = transactions.Max(x => x.Id) + 1;
			}
			transactions.Add(item);
			SaveChanges();
		}

		public void Update(Transaction item) {
			var transaction = transactions.FirstOrDefault(x => x.Id == item.Id);

			if (transaction != null) {
				transactions.Remove(transaction);
				transactions.Add(item);
				SaveChanges();
			}
		}

		public void Delete(int id) {
			var transaction = transactions.FirstOrDefault(x => x.Id == id);

			if (transaction != null) {
				transactions.Remove(transaction);
				SaveChanges();
			}
		}

		public decimal GetSumTransaction()
		{
			return transactions.Sum(x => x.Amount);
		}

		private void SaveChanges() {
			var fileValue = JsonSerializer.Serialize(transactions);
			File.WriteAllText(transactionsFilePath, fileValue);
		}

	}
}
