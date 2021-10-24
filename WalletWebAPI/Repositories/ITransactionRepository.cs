using DataAccessLayer.Entities;

namespace WalletWebAPI.Repositories {
	public interface ITransactionRepository : IRepository<Transaction>
	{
		decimal GetSumTransaction();
	}
}
