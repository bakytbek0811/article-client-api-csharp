using ArticleApi.Data;
using ArticleApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Controllers;

[ApiController]
[Route("Author")]
public class AuthorController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuthorService _authorService;

    public AuthorController(AppDbContext context, IAuthorService authorService)
    {
        _context = context;
        this._authorService = authorService;
    }
    
    [HttpGet("GetAuthors")]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return await _authorService.GetAuthorsAsync();
    }
}