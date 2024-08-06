using ArticleApi.Dto;

namespace ArticleApi.Services;

public interface IArticleService
{
    Task UpdateArticleAsync(int id, UpdateArticleDto dto);
    Task<Article> CreateArticleAsync(CreateArticleDto dto);
    Task<Article[]> GetArticlesAsync();
    Task<Article> GetArticleAsync(int id);
    Task DeleteArticleAsync(int id);
}