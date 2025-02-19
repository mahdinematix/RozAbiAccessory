using _01_Framework.Infrastructure;

namespace AccountManagement.Application.Contract.Account;

public class EditAccount : CreateAccount
{
    public long Id { get; set; }
    public List<PermissionDto> MappedPermissions { get; set; }

    public EditAccount()
    {
        Permissions = new List<int>();
    }

}