using _01_Framework.Application;

namespace CommentManagement.Application.Contract.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        OperationResult Cancel(long id);
        OperationResult Confirm(long id);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
