using TestApi.Dtos.Comment;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;

public class CommentManager : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentManager(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _commentRepository.GetAllAsync(null, "Stock", "AppUser");
    }

    public async Task<Comment?> GetCommentAsync(int id)
    {
        return await _commentRepository.GetByIdAsync(id, "Stock", "AppUser");
    }

    public async Task<Comment> CreateCommentAsync(int stockId, string userId, CreateCommentDto commentDto)
    {
        var comment = commentDto.ToCommentFromCreateDto(stockId, userId);
        await _commentRepository.CreateAsync(comment);
        return comment;
    }

    public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentDto)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment != null)
        {
            comment.Title = commentDto.Title;
            comment.Content = commentDto.Content;
            comment.AppUserId = commentDto.AppUserId;

            await _commentRepository.UpdateAsync(comment);
        }
        return comment;
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }

    public async Task<bool> CommentExistsAsync(int id)
    {
        return await _commentRepository.IsExistsAsync(id);
    }
}
