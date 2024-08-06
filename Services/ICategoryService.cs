namespace ArticleApi.Services;

public interface ICategoryService
{
    Task<Category> GetCategoryAsync(int Id);
    Task<Category[]> GetCategoriesAsync();
}