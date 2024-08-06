namespace ArticleApi.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException() : base("Category not found.")
    {
    }
}