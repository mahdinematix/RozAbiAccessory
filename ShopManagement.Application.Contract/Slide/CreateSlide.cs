﻿using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contract.Slide
{
    public class CreateSlide
    {
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtensionLimitation([".jpeg", ".jpg", ".png"], ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Heading { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Text { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string BtnText { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Link { get; set; }
    }
}
