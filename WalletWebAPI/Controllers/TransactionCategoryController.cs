using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletWebAPI.Repositories;

namespace WalletWebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionCategoryController : Controller {
		private readonly ITransactionCategoryRepository _repository;

		public TransactionCategoryController(ITransactionCategoryRepository repository) {
			_repository = repository;
		}

		/// <summary>
		/// Gets the list of all Transaction Category.
		/// </summary>
		// GET: api/<TransactionCategoryController>
		[HttpGet]
		public IEnumerable<TransactionCategory> Get() {
			return _repository.GetAll();
		}

		/// <summary>
		/// Gets a Transactions Category.
		/// </summary>
		// GET api/<TransactionCategoryController>/id
		[HttpGet("{id}")]
		public TransactionCategory Get(int id) {
			return _repository.Get(id);
		}

		/// <summary>
		/// Creates a Transaction Category.
		/// </summary>
		// POST api/<TransactionCategoryController>/value
		[HttpPost]
		public void Post([FromBody] TransactionCategory value) {
			if (value == null) {
				return;
			}
			_repository.Create(value);
		}

		/// <summary>
		/// Updates a Transaction Category.
		/// </summary>
		// PUT api/<TransactionCategoryController>/value
		[HttpPut]
		public void Put([FromBody] TransactionCategory value) {
			if (value == null) {
				return;
			}
			_repository.Update(value);
		}

		/// <summary>
		/// Delete a Transaction Category.
		/// </summary>
		// DELETE api/<TransactionCategoryController>/id
		[HttpDelete("{id}")]
		public void Delete(int id) {
			_repository.Delete(id);
		}
	}
}
