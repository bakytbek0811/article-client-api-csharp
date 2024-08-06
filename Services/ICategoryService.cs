namespace ArticleApi.Services;

public interface ICategoryService
{
    Task<Category> GetCategoryAsync(int id);
    Task<Category[]> GetCategoriesAsync();
}