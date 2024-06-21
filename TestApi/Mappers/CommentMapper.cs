using TestApi.Dtos.Comment;
using TestApi.Models;

namespace TestApi.Mappers;

public static class CommentMapper
{
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

    public static Comment ToCommentFromCreateDto(this CreateCommentDto commentDto, int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId,
            AppUserId = commentDto.AppUserId,
        };
    }
}
