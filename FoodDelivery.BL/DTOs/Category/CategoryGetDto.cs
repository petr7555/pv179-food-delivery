namespace FoodDelivery.BL.DTOs.Category;

public class CategoryGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<DAL.EntityFramework.Models.Product> Products { get; set; }
}
