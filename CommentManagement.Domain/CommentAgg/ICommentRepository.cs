using _01_Framework.Domain;
using CommentManagement.Application.Contract.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepositoryBase<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
