using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletWebAPI.Repositories;

namespace WalletWebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase {
		private readonly ITransactionRepository _repository;

		public TransactionController(ITransactionRepository repository) {
			this._repository = repository;
		}

		/// <summary>
		/// Gets the list of all Transaction.
		/// </summary>
		// GET: api/<TransactionController>
		[HttpGet]
		public IEnumerable<Transaction> Get() {
			return _repository.GetAll();
		}

		/// <summary>
		/// Gets Balance of Transactions.
		/// </summary>
		// GET api/<TransactionController>/balance
		[HttpGet("balance")]
		public decimal Balance()
		{
			return _repository.GetSumTransaction();
		}

		/// <summary>
		/// Gets a Transactions.
		/// </summary>
		// GET api/<TransactionController>/id
		[HttpGet("{id}")]
		public Transaction GetOne(int id) {
			return _repository.Get(id);
		}

		/// <summary>
		/// Gets the list of Transaction by period.
		/// </summary>
		// GET api/<TransactionController>/from/to
		[HttpGet("{from}/{to}")]
		public IEnumerable<Transaction> Get(DateTime from, DateTime to) {
			return _repository.Find(x => x.DayTransaction >= from && x.DayTransaction <= to);
		}

		/// <summary>
		/// Creates a Transaction.
		/// </summary>
		// POST api/<TransactionController>/value
		[HttpPost]
		public void Post([FromBody] Transaction value) {
			if (value == null) {
				return;
			}
			_repository.Create(value);
		}

		/// <summary>
		/// Updates a Transaction.
		/// </summary>
		// PUT api/<TransactionController>/value
		[HttpPut]
		public void Put([FromBody] Transaction value) {
			if (value == null) {
				return;
			}
			_repository.Update(value);
		}

		/// <summary>
		/// Delete a Transaction.
		/// </summary>
		// DELETE api/<TransactionController>/id
		[HttpDelete("{id}")]
		public void Delete(int id) {
			_repository.Delete(id);
		}
	}
}
