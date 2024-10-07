using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;
using talabat.Core.Specifications.ProductSpecifications;

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
            var spec = new ProductWithBrandAndCategorySpecifications();
            var Products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProduct(int id )
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var Product = await _productRepo.GetWithSpecAsync(spec);
            if (Product==null) { return NotFound(new { messge="Not Found", statusCode=404}); }
            return Ok(Product);
        }
    }
}
