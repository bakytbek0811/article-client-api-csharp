using ArticleApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Controllers;

[ApiController]
[Route("Author")]
public class AuthorController : Controller
{
    private readonly AppDbContext _context;

    public AuthorController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("GetAuthors")]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return await _context.Authors.ToListAsync();
    }
}