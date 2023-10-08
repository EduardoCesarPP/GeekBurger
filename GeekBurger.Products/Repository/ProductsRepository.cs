using GeekBurger.Products.Contract;
using GeekBurger.Products.Repository.Interfaces;

namespace GeekBurger.Products.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private IList<Product> Products = new List<Product>();

        public ProductsRepository()
        {
            var paulistaStore = "Paulista";
            var morumbiStore = "Morumbi";

            var beef = new Item { ItemId = Guid.NewGuid(), Name = "beef" };
            var pork = new Item { ItemId = Guid.NewGuid(), Name = "pork" };
            var mustard = new Item { ItemId = Guid.NewGuid(), Name = "mustard" };
            var ketchup = new Item { ItemId = Guid.NewGuid(), Name = "ketchup" };
            var bread = new Item { ItemId = Guid.NewGuid(), Name = "bread" };
            var wBread = new Item { ItemId = Guid.NewGuid(), Name = "whole bread" };

            Products = new List<Product>()
            {
                new Product { ProductId = Guid.NewGuid(), Name = "Darth Bacon",
                    Image = "hamb1.png", StoreName = paulistaStore, Price = 1.5,
                    Ingredients = new List<Item> {beef, ketchup, bread }
                },
                new Product { ProductId = Guid.NewGuid(), Name = "Cap. Spork",
                    Image = "hamb2.png", StoreName = paulistaStore, Price = 2.5,
                    Ingredients = new List<Item> { pork, mustard, wBread }
                },
                new Product { ProductId = Guid.NewGuid(), Name = "Beef Turner",
                    Image = "hamb3.png", StoreName = morumbiStore, Price = 3.5,
                    Ingredients = new List<Item> {beef, mustard, bread }
                }
            };
        }

        public List<Product> GetProductsbyStoreName(string storeName)
        {
            return Products.Where(p => p.StoreName == storeName).ToList();
        }
    }
}
