using System.ComponentModel.DataAnnotations;

public class Store {
    [Key] 
    public Guid StoreId { get; set; } 
    public string Name { get; set; } 
}