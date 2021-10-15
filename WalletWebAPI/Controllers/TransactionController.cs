using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletWebAPI.Models;
using WalletWebAPI.Repositories;

namespace WalletWebAPI.Controlers {
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase {
		private IRepository<TransactionModel> repository;

		public TransactionController(IRepository<TransactionModel> repository) {
			this.repository = repository;
		}

		// GET: api/<TransactionController>
		[HttpGet]
		public IEnumerable<TransactionModel> Get() {
			return repository.GetAll();
		}

		// GET api/<TransactionController>/balance
		[HttpGet("balance")]
		public decimal Balance() {
			return repository.GetAll().Sum(x => x.Amount);
		}

		// GET api/<TransactionController>/from/to
		[HttpGet("{from}/{to}")]
		public IEnumerable<TransactionModel> Get(DateTime from, DateTime to) {
			return repository.Find(x => x.DayTransaction >= from && x.DayTransaction <= to);
		}

		// POST api/<TransactionController>/value
		[HttpPost]
		public void Post([FromBody] TransactionModel value) {
			if (value == null) {
				return;
			}
			repository.Create(value);
		}

		// PUT api/<TransactionController>/value
		[HttpPut]
		public void Put([FromBody] TransactionModel value) {
			if (value == null) {
				return;
			}
			repository.Update(value);
		}

		// DELETE api/<TransactionController>/id
		[HttpDelete("{id}")]
		public void Delete(int id) {
			repository.Delete(id);
		}
	}
}
