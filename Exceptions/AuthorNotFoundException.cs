namespace ArticleApi.Exceptions;

public class AuthorNotFoundException : NotFoundException
{
    public AuthorNotFoundException() : base("Author not found.")
    {
    }
}