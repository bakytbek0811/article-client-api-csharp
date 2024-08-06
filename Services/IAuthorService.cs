namespace ArticleApi.Services;

public interface IAuthorService
{
    Task<Author> GetAuthorAsync(int Id);
    Task<Author[]> GetAuthorsAsync();
}