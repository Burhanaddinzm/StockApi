using TestApi.Dtos.Comment;
using TestApi.Models;

namespace TestApi.Services.Interfaces;

public interface ICommentService
{
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment?> GetCommentAsync(int id);
    Task<Comment> CreateCommentAsync(int stockId, string userId, CreateCommentDto commentDto);
    Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentDto);
    Task DeleteCommentAsync(int id);
    Task<bool> CommentExistsAsync(int id);
}
