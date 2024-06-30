using TestApi.Dtos.Comment;
using TestApi.Models;

namespace TestApi.Services.Interfaces;

/// <summary>
/// Interface for the Comment Service. Provides methods for interacting with the Comment repository.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Asynchronously gets all Comments from the repository.
    /// </summary>
    /// <returns>A list of Comment objects.</returns>
    Task<List<Comment>> GetAllCommentsAsync();

    /// <summary>
    /// Asynchronously gets a Comment by its ID from the repository.
    /// </summary>
    /// <param name="id">The ID of the Comment.</param>
    /// <returns>The Comment object with the specified ID, or null if no such Comment exists.</returns>
    Task<Comment?> GetCommentAsync(int id);

    /// <summary>
    /// Asynchronously creates a new Comment in the repository.
    /// </summary>
    /// <param name="stockId">The ID of the Stock associated with the Comment.</param>
    /// <param name="userId">The ID of the User associated with the Comment.</param>
    /// <param name="commentDto">The CommentDto object containing the data for the new Comment.</param>
    /// <returns>The newly created Comment object.</returns>
    Task<Comment> CreateCommentAsync(int stockId, string userId, CreateCommentDto commentDto);

    /// <summary>
    /// Asynchronously updates a Comment in the repository.
    /// </summary>
    /// <param name="id">The ID of the Comment to update.</param>
    /// <param name="commentDto">The UpdateCommentDto object containing the updated data for the Comment.</param>
    /// <returns>The updated Comment object, or null if no Comment with the specified ID exists.</returns>
    Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentDto);

    /// <summary>
    /// Asynchronously deletes a Comment from the repository.
    /// </summary>
    /// <param name="id">The ID of the Comment to delete.</param>
    Task DeleteCommentAsync(int id);

    /// <summary>
    /// Asynchronously checks if a Comment exists in the repository.
    /// </summary>
    /// <param name="id">The ID of the Comment to check.</param>
    /// <returns>True if a Comment with the specified ID exists, false otherwise.</returns>
    Task<bool> CommentExistsAsync(int id);
}
