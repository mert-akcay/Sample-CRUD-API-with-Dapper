namespace WebAPI.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
}