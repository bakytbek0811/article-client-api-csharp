namespace ArticleApi.Dto;

public class UpdateArticleDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
}