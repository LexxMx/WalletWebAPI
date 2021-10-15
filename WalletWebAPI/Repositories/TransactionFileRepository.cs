using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WalletWebAPI.Models;
using System.Text.Json;

namespace WalletWebAPI.Repositories {
	public class TransactionFileRepository : IRepository<TransactionModel> {

		private List<TransactionModel> transactions;
		private string transactionsFilePath = "transactions.json";

		public TransactionFileRepository() {
			var fileValue = File.ReadAllText(transactionsFilePath);

			if (!string.IsNullOrEmpty(fileValue)) {
				transactions = JsonSerializer.Deserialize<List<TransactionModel>>(fileValue);
			} else {
				transactions = new List<TransactionModel>();
			}
		}

		public IEnumerable<TransactionModel> GetAll() {
			return transactions;
		}

		public TransactionModel Get(int id) {
			return transactions.FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<TransactionModel> Find(Func<TransactionModel, bool> predicate) {
			return transactions.Where(predicate);
		}

		public void Create(TransactionModel item) {
			if (item.Id == 0) {
				item.Id = transactions.Max(x => x.Id) + 1;
			}
			transactions.Add(item);
			SaveChanges();
		}

		public void Update(TransactionModel item) {
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

		private void SaveChanges() {
			var fileValue = JsonSerializer.Serialize(transactions);
			File.WriteAllText(transactionsFilePath, fileValue);
		}

	}
}
