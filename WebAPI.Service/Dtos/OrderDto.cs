namespace WebAPI.Service.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int StoreId { get; set; }
    public CustomerDto Customer { get; set; }
    public StoreDto Store { get; set; }
}