using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comment
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> Comments;
        public CommentSearchModel Search;
        private readonly ICommentApplication _commentApplication;


        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);   

        }

        public IActionResult OnGetCancel(long id)
        {
            var result = _commentApplication.Cancel(id);
            if (result.IsSucceed)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");

        }
        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);
            if (result.IsSucceed)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
