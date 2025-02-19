using _02_Query.Profile;
using AccountManagement.Infrastructure.EFCore;

namespace _02_Query.Query;

public class ProfileQuery : IProfileQuery
{
    private readonly AccountContext _accountContext;

    public ProfileQuery(AccountContext accountContext)
    {
        _accountContext = accountContext;
    }

    public ProfileQueryModel GetAccountDetails(long id)
    {
        return _accountContext.Accounts.Select(x => new ProfileQueryModel
        {
            Id = x.Id,
            Address = x.Address,
            Email = x.Email,
            Fullname = x.Fullname,
            Mobile = x.Mobile,
            Zipcode = x.Zipcode,
            ProfilePhoto = x.ProfilePhoto,
            Username = x.Username
        }).FirstOrDefault(x => x.Id == id);
    }
}