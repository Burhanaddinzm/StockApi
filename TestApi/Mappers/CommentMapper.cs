using TestApi.Dtos.Comment;
using TestApi.Models;

namespace TestApi.Mappers;


/// <summary>
/// Provides extension methods to map between Comment and CommentDto objects.
/// </summary>
public static class CommentMapper
{
    /// <summary>
    /// Maps a Comment object to a CommentDto object.
    /// </summary>
    /// <param name="comment">The Comment object to map from.</param>
    /// <returns>The mapped CommentDto object.</returns>
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            StockId = comment.StockId,
            AppUserId = comment.AppUserId,
            IP = comment.IP,
            CreatedAt = comment.CreatedAt,
            CreatedBy = comment.CreatedBy,
            ModifiedAt = comment.ModifiedAt,
            ModifiedBy = comment.ModifiedBy,
        };
    }

    /// <summary>
    /// Maps a CommentDto object to a Comment object using the provided stockId and userId.
    /// </summary>
    /// <param name="commentDto">The CommentDto object to map from.</param>
    /// <param name="stockId">The stock ID to assign to the resulting Comment object.</param>
    /// <param name="userId">The user ID to assign to the resulting Comment object.</param>
    /// <returns>The mapped Comment object.</returns>
    public static Comment ToCommentFromCreateDto(this CreateCommentDto commentDto, int stockId, string userId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId,
            AppUserId = userId,
        };
    }
}
