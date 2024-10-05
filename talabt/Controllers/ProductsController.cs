using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;

namespace talabt.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Products> _productRepo;

        public ProductsController(IGenericRepository<Products> productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var Products = await _productRepo.GetAllAsync();
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProduct(int id )
        {
            var Product = await _productRepo.GetAsync(id);
            if (Product==null) { return NotFound(new { messge="Not Found", statusCode=404}); }
            return Ok(Product);
        }
    }
}
