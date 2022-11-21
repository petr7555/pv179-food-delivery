namespace FoodDelivery.BL.DTOs.Category;

public class CategoryCreateDto
{
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}
