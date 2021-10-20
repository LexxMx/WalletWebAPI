using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletWebAPI.Models;
using WalletWebAPI.Repositories;

namespace WalletWebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase {
		private readonly ITransactionRepository _repository;

		public TransactionController(ITransactionRepository repository) {
			this._repository = repository;
		}

		// GET: api/<TransactionController>
		[HttpGet]
		public IEnumerable<TransactionModel> Get() {
			return _repository.GetAll();
		}

		// GET api/<TransactionController>/balance
		[HttpGet("balance")]
		public decimal Balance()
		{
			return _repository.GetSumTransaction();
		}

		// GET api/<TransactionController>/from/to
		[HttpGet("{id}")]
		public TransactionModel GetOne(int id) {
			return _repository.Get(id);
		}

		// GET api/<TransactionController>/from/to
		[HttpGet("{from}/{to}")]
		public IEnumerable<TransactionModel> Get(DateTime from, DateTime to) {
			return _repository.Find(x => x.DayTransaction >= from && x.DayTransaction <= to);
		}

		// POST api/<TransactionController>/value
		[HttpPost]
		public void Post([FromBody] TransactionModel value) {
			if (value == null) {
				return;
			}
			_repository.Create(value);
		}

		// PUT api/<TransactionController>/value
		[HttpPut]
		public void Put([FromBody] TransactionModel value) {
			if (value == null) {
				return;
			}
			_repository.Update(value);
		}

		// DELETE api/<TransactionController>/id
		[HttpDelete("{id}")]
		public void Delete(int id) {
			_repository.Delete(id);
		}
	}
}
