using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Comment;

/// <summary>
/// Data transfer object representing the data needed to update a comment.
/// </summary>
public class UpdateCommentDto
{
    /// <summary>
    /// Gets or sets the title of the comment.
    /// It must be at least 5 characters long and cannot exceed 280 characters.
    /// </summary>
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Title can't exceed 280 characters.")]
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the content of the comment.
    /// It must be at least 5 characters long and cannot exceed 280 characters.
    /// </summary>
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Content can't exceed 280 characters.")]
    public string Content { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the ID of the user who created the comment.
    /// </summary>
    public string AppUserId { get; set; } = null!;
}
