using _01_Framework.Application;
using _01_Framework.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    Type = x.Type,
                    OwnerRecordId = x.OwnerRecordId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    IsCanceled = x.IsCancel,
                    IsConfirmed = x.IsConfirm,
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
