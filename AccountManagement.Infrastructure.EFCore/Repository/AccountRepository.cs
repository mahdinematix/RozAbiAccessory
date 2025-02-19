using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long,Account> , IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context) 
        {
            _context = context;
        }

        public EditAccount GetDetails(long id)
        {
            var account =  _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Email = x.Email,
                Address = x.Address,
                Zipcode = x.Zipcode,
                MappedPermissions = MapPermissions(x.Permissions)

            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            account.Permissions = account.MappedPermissions.Select(x => x.Code).ToList();

            return account;
        }

        public EditAccountByUser GetDetailsQuery(long id)
        {
            var account = _context.Accounts.Select(x => new EditAccountByUser
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                Email = x.Email,
                Address = x.Address,
                Zipcode = x.Zipcode,
                RegisterDate = x.CreationDate.ToFarsi()
            }).FirstOrDefault(x => x.Id == id);


            return account;
        }

        private static List<PermissionDto> MapPermissions(IEnumerable<AccountPermission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                Email = x.Email,
                Role = x.Role.Name,
                RoleId = x.RoleId,
                Address = x.Address,
                Zipcode = x.Zipcode,
                CreationDate = x.CreationDate.ToFarsi(),
                ProfilePhoto = x.ProfilePhoto
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
            {
                query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
            {
                query = query.Where(x => x.Username.Contains(searchModel.Username));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }

            if (searchModel.RoleId > 0)
            {
                query = query.Where(x => x.RoleId == searchModel.RoleId);
            }


            return query.OrderByDescending(x => x.Id).ToList();

        }

        public Account GetByUsername(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname
            }).ToList();
        }
    }
}
