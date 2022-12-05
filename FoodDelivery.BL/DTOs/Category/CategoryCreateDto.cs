namespace FoodDelivery.BL.DTOs.Category;

public class CategoryCreateDto
{
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
