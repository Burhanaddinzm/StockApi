using TestApi.Data;
using TestApi.Models;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }
}
