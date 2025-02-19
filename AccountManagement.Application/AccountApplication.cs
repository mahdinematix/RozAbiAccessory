using _01_Framework.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x=>x.Username == command.Username  || x.Mobile == command.Mobile || x.Email == command.Email))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            if (command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);
            }
            var password = _passwordHasher.Hash(command.Password);
            var fileName = _fileUploader.Upload(command.ProfilePhoto, "ProfilePhotos");

            var account = new Account(command.Fullname, command.Username, password, fileName, command.RoleId,
                command.Mobile, command.Email,command.Address,command.Zipcode, new List<AccountPermission>());

            _accountRepository.Create(account);
            _accountRepository.Save();
            return operation.Succeed();

        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            if (_accountRepository.Exists(x => (x.Username == command.Username || x.Mobile == command.Mobile || x.Email == command.Email) && x.Id !=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var fileName = _fileUploader.Upload(command.ProfilePhoto, "ProfilePhotos");

            var permissions = new List<AccountPermission>();
            command.Permissions.ForEach(code => permissions.Add(new AccountPermission(code)));


            account.Edit(command.Fullname, command.Username, fileName, command.RoleId,
                command.Mobile, command.Email, command.Address, command.Zipcode, permissions);

            _accountRepository.Save();
            return operation.Succeed();

        }

        public OperationResult EditByUser(EditAccountByUser command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            if (_accountRepository.Exists(x => (x.Username == command.Username || x.Mobile == command.Mobile || x.Email == command.Email) && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var fileName = _fileUploader.Upload(command.ProfilePhoto, "ProfilePhotos");

            account.EditByUser(command.Fullname, command.Username, fileName,
                command.Mobile, command.Email, command.Address, command.Zipcode);

            _accountRepository.Save();
            return operation.Succeed();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            if (command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);
            }

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.Save();
            return operation.Succeed();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public EditAccountByUser GetDetailsQuery(long id)
        {
            return _accountRepository.GetDetailsQuery(id);

        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetByUsername(command.Username);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.WrongPasswordOrUsername);
            }

            var result = _passwordHasher.Check(account.Password, command.Password);

            if (!result.Verified)
            {
                return operation.Failed(ApplicationMessages.WrongPasswordOrUsername);
            }

            var permissions = new List<int>();
            var rolePermissions = _roleRepository.GetBy(account.RoleId).Permissions.Select(x=>x.Code).ToList();

            var accountPermissions = _accountRepository.GetBy(account.Id).Permissions.Select(x => x.Code).ToList();

            if (accountPermissions.Count > 0)
            {
                permissions = accountPermissions;
            }
            else
            {
                permissions = rolePermissions;
            }

            var authViewModel = new AuthViewModel(account.Id,account.RoleId,account.Username,account.Fullname,account.Mobile,account.Email,account.Address,account.Zipcode,account.ProfilePhoto, permissions);

            _authHelper.Signin(authViewModel);
            return operation.Succeed();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public AccountViewModel GetAccountBy(long id)
        {
            var account = _accountRepository.GetBy(id);
            return new AccountViewModel
            {
                Fullname = account.Fullname,
                Mobile = account.Mobile
            };
        }
    }
}
