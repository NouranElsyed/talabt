using AutoMapper;
using Microsoft.Extensions.Configuration;
using talabat.Core.Entities;
using talabt.DTOs;
namespace talabt.Helper

{
    public class ProductPictureUrlResolver : IValueResolver<Products, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;
        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Products source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
