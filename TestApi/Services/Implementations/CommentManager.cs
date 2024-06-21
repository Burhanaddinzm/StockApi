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

    public async Task<Comment> CreateCommentAsync(CreateCommentDto commentDto)
    {
        var comment = commentDto.ToCommentFromCreateDto();
        await _commentRepository.CreateAsync(comment);
        return comment;
    }
}
