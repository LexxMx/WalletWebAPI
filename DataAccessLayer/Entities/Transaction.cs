using System;

namespace DataAccessLayer.Entities {
	public class Transaction {
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public DateTime DayTransaction { get; set; }
		public TransactionDirection Diraction { get; set; }
	}

	public enum TransactionDirection {
		Withdraw,
		Deposit
	}
}
