using talabt.DTOs;

namespace talabtAPIs.DTOs
{
    public class ProductsToBeReturnDto
    {
        public IEnumerable<ProductDTO> Data { get; set; }
        public int TotalCount { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
