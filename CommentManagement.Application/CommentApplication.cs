using _01_Framework.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.Name, command.Message, command.Email,command.Type,command.OwnerRecordId);
            _commentRepository.Create(comment);
            _commentRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();

            var comment = _commentRepository.GetBy(id);

            if (comment == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            comment.Cancel();
            _commentRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();

            var comment = _commentRepository.GetBy(id);

            if (comment == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            comment.Confirm();
            _commentRepository.Save();
            return operation.Succeed();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
