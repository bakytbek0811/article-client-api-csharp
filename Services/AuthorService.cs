using ArticleApi.Data;
using ArticleApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ArticleApi.Services;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;
    
    public async Task<Author> GetAuthorAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            throw new AuthorNotFoundException();
        }

        return author;
    }

    public async Task<Author[]> GetAuthorsAsync()
    {
        return await this._context.Authors.ToArrayAsync();
    }
}