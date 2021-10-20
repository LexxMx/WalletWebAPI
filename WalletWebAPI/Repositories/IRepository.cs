using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletWebAPI.Repositories {
	public interface IRepository<T> where T : class {
		IQueryable<T> GetAll();
		T Get(int id);
		IQueryable<T> Find(Func<T, bool> predicate);
		void Create(T item);
		void Update(T item);
		void Delete(int id);
	}
}
