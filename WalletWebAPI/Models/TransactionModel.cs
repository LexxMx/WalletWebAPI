using System;

namespace WalletWebAPI.Models {
	public class TransactionModel {
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public DateTime DayTransaction { get; set; }
		public TransactionDirectionEnum Diraction { get; set; }
	}

	public enum TransactionDirectionEnum {
		Withdraw,
		Deposit
	}
}
