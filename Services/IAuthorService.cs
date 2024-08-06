namespace ArticleApi.Services;

public interface IAuthorService
{
    Task<Author> GetAuthorAsync(int id);
    Task<Author[]> GetAuthorsAsync();
}