using TestApi.Dtos.Comment;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;


/// <summary>
/// Provides methods for managing comments.
/// </summary>
public class CommentManager : ICommentService
{
    /// <summary>
    /// The comment repository used to perform database operations.
    /// </summary>
    private readonly ICommentRepository _commentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommentManager"/> class.
    /// </summary>
    /// <param name="commentRepository">The comment repository to use.</param>
    public CommentManager(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    /// <summary>
    /// Asynchronously retrieves all comments.
    /// </summary>
    /// <returns>A list of comments.</returns>
    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _commentRepository.GetAllAsync(null, "Stock", "AppUser");
    }

    /// <summary>
    /// Asynchronously retrieves a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment to retrieve.</param>
    /// <returns>The comment with the specified ID, or null if it does not exist.</returns>
    public async Task<Comment?> GetCommentAsync(int id)
    {
        return await _commentRepository.GetByIdAsync(id, "Stock", "AppUser");
    }

    /// <summary>
    /// Asynchronously creates a new comment.
    /// </summary>
    /// <param name="stockId">The ID of the stock associated with the comment.</param>
    /// <param name="userId">The ID of the user associated with the comment.</param>
    /// <param name="commentDto">The comment data to use for creating the comment.</param>
    /// <returns>The newly created comment.</returns>
    public async Task<Comment> CreateCommentAsync(int stockId, string userId, CreateCommentDto commentDto)
    {
        var comment = commentDto.ToCommentFromCreateDto(stockId, userId);
        await _commentRepository.CreateAsync(comment);
        return comment;
    }

    /// <summary>
    /// Asynchronously updates an existing comment.
    /// </summary>
    /// <param name="id">The ID of the comment to update.</param>
    /// <param name="commentDto">The updated comment data.</param>
    /// <returns>The updated comment, or null if it does not exist.</returns>
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

    /// <summary>
    /// Asynchronously deletes a comment.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Asynchronously checks if a comment exists.
    /// </summary>
    /// <param name="id">The ID of the comment to check.</param>
    /// <returns>True if the comment exists, false otherwise.</returns>
    public async Task<bool> CommentExistsAsync(int id)
    {
        return await _commentRepository.IsExistsAsync(id);
    }
}
