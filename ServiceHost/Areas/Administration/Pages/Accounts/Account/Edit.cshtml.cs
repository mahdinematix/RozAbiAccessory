using _01_Framework.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Infrastructure.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class EditModel : PageModel
    {
        public EditAccount Command;
        private readonly IAccountApplication _accountApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;
        private readonly IRoleApplication _roleApplication;
        public List<SelectListItem> Permissions = new List<SelectListItem>();
        public SelectList Roles;


        public EditModel(IAccountApplication accountApplication, IEnumerable<IPermissionExposer> exposers, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _exposers = exposers;
            _roleApplication = roleApplication;
        }

        public void OnGet(long id)
        {
            Roles = new SelectList(_roleApplication.GetAllRoles(), "Id", "Name");
            Command = _accountApplication.GetDetails(id);
            var permissions = new List<PermissionDto>();
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    permissions.AddRange(value);
                    var group = new SelectListGroup
                    {
                        Name = key
                    };

                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        if (Command.MappedPermissions.Any(x => x.Code == permission.Code))
                            item.Selected = true;

                        Permissions.Add(item);
                    }
                }
            }
        }

        [NeedsPermissions(AccountPermissions.EditAccount)]
        public IActionResult OnPost(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
