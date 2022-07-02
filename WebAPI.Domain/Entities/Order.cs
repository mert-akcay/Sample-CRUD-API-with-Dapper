namespace WebAPI.Domain.Entities;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public int StoreId { get; set; }
    public Customer Customer { get; set; }
    public Store Store { get; set; }
}