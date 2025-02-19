using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contract.Account
{
    public class EditAccountByUser
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public string RegisterDate { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
    }
}
