namespace AccountManagement.Domain.AccountAgg;

public class AccountPermission
{
    public long Id { get; private set; }
    public int Code { get; private set; }
    public string Name { get; private set; }
    public long AccountId { get; private set; }
    public Account Account { get; private set; }
    public AccountPermission(int code, string name)
    {
        Code = code;
        Name = name;
    }

    public AccountPermission(int code)
    {
        Code = code;
    }
}