namespace TestApi.Dtos.Comment;

/// <summary>
/// A data transfer object (DTO) representing a comment.
/// </summary>
public class CommentDto
{
    /// <summary>
    /// The ID of the comment.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The title of the comment.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// The content of the comment.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// The ID of the stock associated with the comment.
    /// </summary>
    public int? StockId { get; set; }

    /// <summary>
    /// The ID of the user associated with the comment.
    /// </summary>
    public string AppUserId { get; set; } = null!;

    /// <summary>
    /// The IP address of the comment.
    /// </summary>
    public string IP { get; set; } = null!;

    /// <summary>
    /// The date and time the comment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The user who created the comment.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// The date and time the comment was last modified.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// The user who last modified the comment.
    /// </summary>
    public string? ModifiedBy { get; set; }
}
