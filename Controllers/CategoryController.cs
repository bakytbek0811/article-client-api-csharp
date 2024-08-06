using ArticleApi.Data;
using ArticleApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Controllers;

[ApiController]
[Route("Category")]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly ICategoryService _categoryService;

    public CategoryController(AppDbContext context, ICategoryService categoryService)
    {
        _context = context;
        this._categoryService = categoryService;
    }

    [HttpGet("GetCategories")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await this._categoryService.GetCategoriesAsync();
    }
}