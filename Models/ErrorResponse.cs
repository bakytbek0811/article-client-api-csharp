namespace ArticleApi.Models;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ErrorResponse(int StatusCode, string Message)
    {
        this.Message = Message;
        this.StatusCode = StatusCode;
    }
}