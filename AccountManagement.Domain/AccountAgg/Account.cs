using _01_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ProfilePhoto { get; private set; }
        public long RoleId { get; private set; }
        public string Mobile { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string Zipcode { get; private set; }
        public Role Role { get; private set; }
        public List<AccountPermission> Permissions { get; private set; }


        protected Account()
        {
        }
        public Account(string fullname, string username, string password, string profilePhoto, long roleId, string mobile, string email,string address,string zipcode, List<AccountPermission> permissions)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            ProfilePhoto = profilePhoto;
            RoleId = roleId;

            if (roleId == 0)
            {
                RoleId = 5;
            }
            Mobile = mobile;
            Email = email;
            if (string.IsNullOrWhiteSpace(address))
                Address = "";
            else
                Address = address;
            if (string.IsNullOrWhiteSpace(zipcode))
                Zipcode = "";
            else
                Zipcode = zipcode;
            Permissions = permissions;
        }

        public void Edit(string fullname, string username, string profilePhoto, long roleId, string mobile, string email, string address, string zipcode, List<AccountPermission> permissions)
        {
            Fullname = fullname;
            Username = username;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
            {
                ProfilePhoto = profilePhoto;
            }
            RoleId = roleId;
            Mobile = mobile;
            Email = email;

            if (string.IsNullOrWhiteSpace(address))
                Address = "";
            else
                Address = address;

            if (string.IsNullOrWhiteSpace(zipcode))
                Zipcode = "";

            Permissions = permissions;
        }
        public void EditByUser(string fullname, string username, string profilePhoto, string mobile, string email, string address, string zipcode)
        {
            Fullname = fullname;
            Username = username;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
            {
                ProfilePhoto = profilePhoto;
            }
            Mobile = mobile;
            Email = email;

            if (string.IsNullOrWhiteSpace(address))
                Address = "";
            else
                Address = address;

            if (string.IsNullOrWhiteSpace(zipcode))
                Zipcode = "";
            else
                Zipcode = zipcode;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
