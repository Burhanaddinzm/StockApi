using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Comment;

public class UpdateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Title can't exceed 280 characters.")]
    public string Title { get; set; } = null!;
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Content can't exceed 280 characters.")]
    public string Content { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
}
