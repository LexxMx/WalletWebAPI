using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WalletWebAPI.Repositories {
	public interface IRepository<T> where T : class {
		IEnumerable<T> GetAll();
		T Get(int id);
		IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
		void Create(T item);
		void Update(T item);
		void Delete(int id);
	}
}
