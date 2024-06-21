namespace TestApi.Dtos.Comment;

public class CreateCommentDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int? StockId { get; set; }
    public string AppUserId { get; set; } = null!;
}
