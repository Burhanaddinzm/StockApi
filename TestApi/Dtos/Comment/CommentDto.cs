namespace TestApi.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int? StockId { get; set; }
    public string AppUserId { get; set; } = null!;
    public string IP { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}
