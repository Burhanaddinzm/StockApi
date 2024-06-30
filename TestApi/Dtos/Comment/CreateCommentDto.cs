using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Comment;

/// <summary>
/// Data transfer object representing a comment to be created.
/// </summary>
public class CreateCommentDto
{
    /// <summary>
    /// Gets or sets the title of the comment.
    /// </summary>
    /// <value>
    /// The title of the comment, which must be at least 5 characters long and cannot exceed 280 characters.
    /// </value>
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Title can't exceed 280 characters.")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the content of the comment.
    /// </summary>
    /// <value>
    /// The content of the comment, which must be at least 5 characters long and cannot exceed 280 characters.
    /// </value>
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Content can't exceed 280 characters.")]
    public string Content { get; set; } = null!;
}
