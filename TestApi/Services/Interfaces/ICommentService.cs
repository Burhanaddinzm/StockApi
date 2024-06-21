using TestApi.Dtos.Comment;
using TestApi.Models;

namespace TestApi.Services.Interfaces;

public interface ICommentService
{
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment?> GetCommentAsync(int id);
    Task<Comment> CreateCommentAsync(CreateCommentDto commentDto);
}
