using ArticleApi.Data;
using ArticleApi.Dto;
using ArticleApi.Exceptions;
using ArticleApi.Models;
using ArticleApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Controllers;

[ApiController]
[Route("Article")]
public class ArticleController : Controller
{
    private readonly AppDbContext _context;
    private readonly IArticleService _articleService;

    public ArticleController(AppDbContext context, IArticleService articleService)
    {
        this._context = context;
        this._articleService = articleService;
    }

    [HttpGet("GetArticles")]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        try
        {
            return Ok(await _articleService.GetArticlesAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(500, ex.Message));
        }
    }

    [HttpGet("GetArticle/{id}")]
    public async Task<ActionResult<Article>> GetArticle([FromRoute] int id)
    {
        try
        {
            return Ok(await this._articleService.GetArticleAsync(id));
        }
        catch (ArticleNotFoundException ex)
        {
            return NotFound(new ErrorResponse(404, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(500, ex.Message));
        }
    }

    [HttpPost("CreateArticle")]
    public async Task<ActionResult<Article>> CreateArticle([FromBody] CreateArticleDto article)
    {
        try
        {
            await _articleService.CreateArticleAsync(article);

            return Created();
        }
        catch (Exception ex) when (ex is AuthorNotFoundException or CategoryNotFoundException)
        {
            return NotFound(new ErrorResponse(404, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(500, ex.Message));
        }
    }

    [HttpDelete("DeleteArticle/{id}")]
    public async Task<ActionResult<Article>> DeleteArticle([FromRoute] int id)
    {
        try
        {
            await this._articleService.DeleteArticleAsync(id);

            return NoContent();
        }
        catch (ArticleNotFoundException ex)
        {
            return NotFound(new ErrorResponse(404, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(500, ex.Message));
        }
    }

    [HttpPatch("UpdateArticle/{id}")]
    public async Task<ActionResult<Article>> UpdateArticle([FromRoute] int id, [FromBody] UpdateArticleDto dto)
    {
        try
        {
            await this._articleService.UpdateArticleAsync(id, dto);

            return NoContent();
        }
        catch (Exception ex) when (ex is AuthorNotFoundException or CategoryNotFoundException
                                       or ArticleNotFoundException)
        {
            return NotFound(new ErrorResponse(404, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(500, ex.Message));
        }
    }
}