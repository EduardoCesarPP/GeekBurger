using GeekBurger.Products.Contract.Model;

public static class ProductsContextExtensions
{
    public static void Seed(this ProductsDbContext context)
    {
        context.Items.RemoveRange(context.Items);
        context.Products.RemoveRange(context.Products);
        context.Stores.RemoveRange(context.Stores);
        context.SaveChanges();
        context.Stores.AddRange(
            new List<Store> {
                new Store { Name = "Paulista", StoreId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e") },
                new Store { Name = "Morumbi", StoreId = new Guid("8d618778-85d7-411e-878b-846a8eef30c0") }
            });
        context.SaveChanges();

        //context.Products.AddRange(

        //    new List<Product> {
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1"), Name ="Darth Bacon",       Image ="hamb1.png", StoreId=new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e"), Price = 2.5M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod2"), Name ="Cap. Spork",        Image ="hamb2.png", StoreId=new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e"), Price = 3.5M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod3"), Name ="Beef Turner",       Image ="hamb3.png", StoreId=new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e"), Price = 4.0M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod4"), Name ="The Big Bang Meal", Image ="hamb4.png", StoreId=new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e"), Price = 6.5M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod5"), Name ="Out of this World", Image ="hamb5.png", StoreId=new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e"), Price = 4.0M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod6"), Name ="Darth Bacon",       Image ="hamb1.png", StoreId=new Guid("8d618778-85d7-411e-878b-846a8eef30c0"), Price = 2.5M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod7"), Name ="Cap. Spork",        Image ="hamb2.png", StoreId=new Guid("8d618778-85d7-411e-878b-846a8eef30c0"), Price = 5.0M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod8"), Name ="Beef Turner",       Image ="hamb3.png", StoreId=new Guid("8d618778-85d7-411e-878b-846a8eef30c0"), Price = 5.5M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod9"), Name ="The Big Bang Meal", Image ="hamb4.png", StoreId=new Guid("8d618778-85d7-411e-878b-846a8eef30c0"), Price = 8.5M},
        //        new Product { ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod0"), Name ="Out of this World", Image ="hamb5.png", StoreId=new Guid("8d618778-85d7-411e-878b-846a8eef30c0"), Price = 5.5M}
        //    });
        //context.SaveChanges();

        //context.Items.AddRange(

        //   new List<Ingredient> {
        //        new Ingredient {  Name ="Beef",    ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Bread",   ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000002"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Ketchup", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000003"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Pork", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod2")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod2")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod2")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //        new Ingredient {  Name ="Darth Bacon", ItemId = new Guid("8048e9ec-80fe-4bad-bc2a-ing000000001"), ProductId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75prod1")},
        //   });
        //context.SaveChanges();


    }
}