using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities {
	public class TransactionCategory {
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsDeleted { get; set; }
		public List<Transaction> Transactions { get; set; } = new List<Transaction>();
	}
}
