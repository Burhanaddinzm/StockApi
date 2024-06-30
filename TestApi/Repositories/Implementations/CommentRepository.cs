using TestApi.Data;
using TestApi.Models;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;

/// <summary>
/// Represents the repository for <see cref="Comment"/> entities. Provides methods for querying and filtering the data.
/// </summary>
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CommentRepository(AppDbContext context) : base(context)
    {
    }
}
