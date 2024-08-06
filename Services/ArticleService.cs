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

    public async Task UpdateArticleAsync(int Id, UpdateArticleDto Dto)
    {
        var Article = await this.GetArticleAsync(Id);

        await this._categoryService.GetCategoryAsync(Dto.CategoryId);
        await this._authorService.GetAuthorAsync(Dto.AuthorId);

        Article.CategoryId = Dto.CategoryId;
        Article.AuthorId = Dto.AuthorId;
        Article.Content = Dto.Content;
        Article.Title = Dto.Title;

        _context.Entry(Article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<Article> CreateArticleAsync(CreateArticleDto Dto)
    {
        await this._categoryService.GetCategoryAsync(Dto.CategoryId);
        await this._authorService.GetAuthorAsync(Dto.AuthorId);
        
        var NewArticle = new Article
        {
            Title = Dto.Title,
            Content = Dto.Content,
            PublishedDate = DateTime.Now.ToUniversalTime(),
            AuthorId = Dto.AuthorId,
            CategoryId = Dto.CategoryId
        };

        this._context.Articles.Add(NewArticle);
        await _context.SaveChangesAsync();

        return NewArticle;
    }

    public async Task<Article[]> GetArticlesAsync()
    {
        return await this._context.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .ToArrayAsync();
    }

    public async Task<Article> GetArticleAsync(int Id)
    {
        var article = await this._context.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (article == null)
        {
            throw new ArticleNotFoundException();
        }

        return article;
    }

    public async Task DeleteArticleAsync(int Id)
    {
        var article = await this.GetArticleAsync(Id);

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsArticleExistsByIdAsync(int Id)
    {
        return await this._context.Articles.FindAsync(Id) != null;
    }
}