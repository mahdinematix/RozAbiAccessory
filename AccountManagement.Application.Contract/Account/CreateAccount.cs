using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contract.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string RePassword { get; set; }

        [MaxFileSize(3 *1024*1024)]
        public IFormFile ProfilePhoto { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long RoleId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public List<RoleViewModel> Roles { get; set; }

        public string Address { get; set; }
        public string Zipcode { get; set; }

        public List<int> Permissions { get; set; }
    }
}
