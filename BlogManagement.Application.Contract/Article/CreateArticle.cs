﻿using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contract.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get; set; }
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PublishDate { get; set; }
        public string CanonicalAddress { get; set; }
    }
}
