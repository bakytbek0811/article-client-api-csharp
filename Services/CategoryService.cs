using ArticleApi.Data;
using ArticleApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<Category> GetCategoryAsync(int id)
    {
        var category = await this._context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new CategoryNotFoundException();
        }

        return category;
    }

    public async Task<Category[]> GetCategoriesAsync()
    {
        return await this._context.Categories.ToArrayAsync();
    }
}