using _01_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class CreateModel : PageModel
    {
        public CreateArticle Command;
        public SelectList ArticleCategories;

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _categoryApplication;

        public CreateModel(IArticleApplication articleApplication, IArticleCategoryApplication categoryApplication)
        {
            _articleApplication = articleApplication;
            _categoryApplication = categoryApplication;
        }

        public void OnGet()
        {
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");
        }

        [NeedsPermissions(BlogPermissions.CreateArticle)]
        public IActionResult OnPost(CreateArticle command)
        {
            var result = _articleApplication.Create(command);
            return RedirectToPage("./Index");

        }
    }
}
