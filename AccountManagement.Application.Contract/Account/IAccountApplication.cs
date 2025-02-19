﻿using _01_Framework.Application;

namespace AccountManagement.Application.Contract.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult EditByUser(EditAccountByUser command);
        OperationResult ChangePassword(ChangePassword command);
        EditAccount GetDetails(long id);
        EditAccountByUser GetDetailsQuery(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        OperationResult Login(Login command);
        void Logout();
        List<AccountViewModel> GetAccounts();
        AccountViewModel GetAccountBy(long id);
    }
}
