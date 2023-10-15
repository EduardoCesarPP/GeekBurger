namespace GeekBurger.Service.Contract
{
    public class IngredientToGet
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public List<AllergenToGet> Allergens { get; set; } = new List<AllergenToGet>();
    }

    public class AllergenToGet
    {
        public FoodCharacteristicsToGet Characteristic { get; set; }
    }

    public class FoodCharacteristicsToGet
    {
        public string Name { get; set; }

    }
}