using ArticleApi.Data;
using ArticleApi.Dto;
using ArticleApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Services;

public class ArticleService : IArticleService
{
    private readonly AppDbContext _context;
    private readonly ICategoryService _categoryService;
    private readonly IAuthorService _authorService;

    public ArticleService(AppDbContext context, ICategoryService categoryService, IAuthorService authorService)
    {
        this._context = context;
        this._categoryService = categoryService;
        this._authorService = authorService;
    }

    public async Task UpdateArticleAsync(int id, UpdateArticleDto dto)
    {
        var article = await this.GetArticleAsync(id);

        await this._categoryService.GetCategoryAsync(dto.CategoryId);
        await this._authorService.GetAuthorAsync(dto.AuthorId);

        article.CategoryId = dto.CategoryId;
        article.AuthorId = dto.AuthorId;
        article.Content = dto.Content;
        article.Title = dto.Title;

        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<Article> CreateArticleAsync(CreateArticleDto dto)
    {
        await this._categoryService.GetCategoryAsync(dto.CategoryId);
        await this._authorService.GetAuthorAsync(dto.AuthorId);

        var newArticle = new Article
        {
            Title = dto.Title,
            Content = dto.Content,
            PublishedDate = DateTime.Now.ToUniversalTime(),
            AuthorId = dto.AuthorId,
            CategoryId = dto.CategoryId
        };

        this._context.Articles.Add(newArticle);
        await _context.SaveChangesAsync();

        return newArticle;
    }

    public async Task<Article[]> GetArticlesAsync()
    {
        return await this._context.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .ToArrayAsync();
    }

    public async Task<Article> GetArticleAsync(int id)
    {
        var article = await this._context.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (article == null)
        {
            throw new ArticleNotFoundException();
        }

        return article;
    }

    public async Task DeleteArticleAsync(int id)
    {
        var article = await this.GetArticleAsync(id);

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }
}