using TestApi.Models;

namespace TestApi.Repositories.Interfaces;

/// <summary>
/// Represents a repository for <see cref="Comment"/> entities that provides methods for querying and filtering the data.
/// </summary>
public interface ICommentRepository : IRepository<Comment>
{
}
