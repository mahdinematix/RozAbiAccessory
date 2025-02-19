using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contract.ProductCategory
{
    //this model will recieve from view actually these properties fill by user(admin)
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtensionLimitation([ ".jpeg",".jpg",".png"],ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }
    }
}
