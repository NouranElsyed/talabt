using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using talabat.Core;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;
using talabat.Core.Specifications.ProductSpecifications;
using talabt.DTOs;
using talabtAPIs.DTOs;
using talabtAPIs.Helper;

namespace talabt.Controllers
{

    public class ProductsController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Cached(300)]
        [HttpGet]
        public async Task<ActionResult<ProductsToBeReturnDto>> GetProducts([FromQuery]ProductSpecParams param)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(param.sort, param.BrandId, param. CategoryId);
            var Products = await _unitOfWork.Repository<Products>().GetAllWithSpecAsync(spec);
            var MappedProducts = _mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(Products);

            var response = new ProductsToBeReturnDto()
            {
                Data = MappedProducts,
                TotalCount = MappedProducts.Count(),
                Success = true,
                Message = "Products fetched successfully"
            };

            return Ok(response);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<Brand>> Getbrands()
        {
            
            var brands = await _unitOfWork.Repository<Brand>().GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<Category>> Getcategories()
        {

            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id ) 
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var Product = await _unitOfWork.Repository<Products>().GetWithSpecAsync(spec);
            if (Product==null) { return NotFound(new { messge="Not Found", statusCode=404}); }
            return Ok(_mapper.Map<Products, ProductDTO>(Product));
        }
    }
}
