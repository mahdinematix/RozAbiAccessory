using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditAccountByUser Command;
        private readonly IAccountApplication _accountApplication;

        public ProfileModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(long id)
        {
            Command = _accountApplication.GetDetailsQuery(id);
        }

        public IActionResult OnPost(EditAccountByUser command, string accountId)
        {
            var result = _accountApplication.EditByUser(command);
            Message = result.Message;
            return RedirectToPage("/Profile", new { Id = accountId });
        }
    }
}
