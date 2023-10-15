namespace GeekBurger.Service.Contract
{
    public class FoodRestrictionsToGet
    {
   
        public Guid UserId { get; set; }
        public List<string> Restrictions { get; set; }
    }
}
