using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletWebAPI.Models;

namespace WalletWebAPI.Repositories {
	public interface ITransactionRepository : IRepository<TransactionModel>
	{
		decimal GetSumTransaction();
	}
}
