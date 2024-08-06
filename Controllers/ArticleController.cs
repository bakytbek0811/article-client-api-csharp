using ArticleApi.Data;
using ArticleApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Controllers;

[ApiController]
[Route("Article")]
public class ArticleController : Controller
{
    private readonly AppDbContext _context;

    public ArticleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetArticles")]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        return await _context.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .ToListAsync();
    }

    [HttpGet("GetArticle/{Id}")]
    public async Task<ActionResult<Article>> GetArticle([FromRoute] int Id)
    {
        var article = await _context.Articles
            .Include(a => a.Author)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (article == null)
        {
            return NotFound("Article not found");
        }

        return Ok(article);
    }

    [HttpPost("CreateArticle")]
    public async Task<ActionResult<Article>> CreateArticle([FromBody] CreateArticleDto article)
    {
        Category category = _context.Categories.Find(article.CategoryId);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        Author author = _context.Authors.Find(article.AuthorId);
        if (author == null)
        {
            return NotFound("Author not found");
        }

        Article newArticle = new Article();
        newArticle.AuthorId = article.AuthorId;
        newArticle.CategoryId = article.CategoryId;
        newArticle.Content = article.Content;
        newArticle.Title = article.Title;
        newArticle.PublishedDate = DateTime.Now.ToUniversalTime();

        try
        {
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();

            return Ok(newArticle);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("DeleteArticle/{Id}")]
    public async Task<ActionResult<Article>> DeleteArticle([FromRoute] int Id)
    {
        Article article = await _context.Articles.FindAsync(Id);
        if (article == null)
        {
            return NotFound("Article not found");
        }

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("UpdateArticle/{Id}")]
    public async Task<ActionResult<Article>> UpdateArticle([FromRoute] int Id, [FromBody] UpdateArticleDto dto)
    {
        Article article = await _context.Articles.FindAsync(Id);
        if (article == null)
        {
            return NotFound("Article not found");
        }

        var authorExists = await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);

        if (!authorExists || !categoryExists)
        {
            return BadRequest("Invalid AuthorId or CategoryId.");
        }

        article.Title = dto.Title;
        article.Content = dto.Content;
        article.AuthorId = dto.AuthorId;
        article.CategoryId = dto.CategoryId;
        article.PublishedDate = article.PublishedDate.ToUniversalTime();

        try
        {
            _context.Entry(article).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArticleExists(Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool ArticleExists(int id)
    {
        return _context.Articles.Any(e => e.Id == id);
    }
}