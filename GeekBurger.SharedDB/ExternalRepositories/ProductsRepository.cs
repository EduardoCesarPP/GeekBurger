﻿using Microsoft.EntityFrameworkCore;

namespace GeekBurger.Shared.ExternalRepositories
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductsDbContext _context;
        public ProductsRepository(ProductsDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetProductsByStoreName(string storeName)
        {
            var products = _context.Products?
                .Where(product => product.Store.Name.Equals(storeName, StringComparison.InvariantCultureIgnoreCase))
                .Include(product => product.Ingredients);
            return products;
        }
        public IEnumerable<Store> GetStores()
        {
            var stores = _context.Stores;
            return stores;
        }
        public bool Add(Product product)

        {
            product.ProductId = Guid.NewGuid();
            _context.Products.Add(product);
            return true;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        Product IProductsRepository.GetProductById(Guid id)
        {
            return _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
        }
        public IEnumerable<Ingredient> GetFullListOfIngredients()
        {
            return _context.Ingredients.ToList();
        }
        public IEnumerable<Product> GetProductsByRestrictions(List<FoodCharacteristics> restrictions)
        {
            return _context.Products.Where(p => p.Ingredients.Any(i => i.Allergens.Any(a => restrictions.Any(r => r == a.Characteristic)))).ToList();
        }
    }
}