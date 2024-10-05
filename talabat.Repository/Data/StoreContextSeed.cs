using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            var brandsData = File.ReadAllText("../talabat.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
            if (brands.Count() > 0)
            {
                brands = brands.Select(b => new Brand() { Name = b.Name }).ToList();
                if (dbcontext.Brands.Count() == 0)
                {
                    foreach (var brand in brands)
                    {
                        dbcontext.Set<Brand>().Add(brand);
                    }
                    await dbcontext.SaveChangesAsync();
                }

            }
            var categoryData = File.ReadAllText("../talabat.Repository/Data/DataSeeding/categories.json");
            var categoroies = JsonSerializer.Deserialize<List<Category>>(categoryData);
            if (categoroies.Count() > 0)
            {
                categoroies = categoroies.Select(c => new Category() { Name = c.Name }).ToList();
                if (dbcontext.Categoroies.Count() == 0)
                {
                    foreach (var category in categoroies)
                    {
                        dbcontext.Set<Category>().Add(category);
                    }
                    await dbcontext.SaveChangesAsync();
                }
   
            }
            var ProductData = File.ReadAllText("../talabat.Repository/Data/DataSeeding/products.json");
            var products = JsonSerializer.Deserialize<List<Products>>(ProductData);
            if (products.Count() > 0)
            {
           
                if (dbcontext.Products.Count() == 0)
                {
                    foreach (var Product in products)
                    {
                        var brandId = Product.BrandId;
                        dbcontext.Set<Products>().Add(Product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
               
            }
        }
    }
}

