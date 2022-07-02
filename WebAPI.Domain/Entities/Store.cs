namespace WebAPI.Domain.Entities;

public class Store : BaseEntity
{
    public string StoreName { get; set; }
    public string Address { get; set; }
}