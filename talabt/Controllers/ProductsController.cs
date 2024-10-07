using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;
using talabat.Core.Specifications.ProductSpecifications;
using talabt.DTOs;

namespace talabt.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Products> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Products> productRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] string? sort)
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var Products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Products>,IEnumerable<ProductDTO>>(Products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id )
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var Product = await _productRepo.GetWithSpecAsync(spec);
            if (Product==null) { return NotFound(new { messge="Not Found", statusCode=404}); }
            return Ok(_mapper.Map<Products, ProductDTO>(Product));
        }
    }
}
