using ArticleApi.Dto;

namespace ArticleApi.Services;

public interface IArticleService
{
    Task UpdateArticleAsync(int Id, UpdateArticleDto Dto);
    Task<Article> CreateArticleAsync(CreateArticleDto Dto);
    Task<Article[]> GetArticlesAsync();
    Task<Article> GetArticleAsync(int Id);
    Task DeleteArticleAsync(int Id);
}