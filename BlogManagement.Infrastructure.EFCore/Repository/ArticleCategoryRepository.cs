using _01_Framework.Application;
using _01_Framework.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {

        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }


        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories
                .Include(x=>x.Articles)
                .Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ShowOrder = x.ShowOrder,
                Description = x.Description.Substring(0,Math.Min(x.Description.Length,50)) + "...",
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
                ArticlesCount = x.Articles.Count
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(x=>x.ShowOrder).ToList();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Name = x.Name,
                ShowOrder = x.ShowOrder,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                CanonicalAddress = x.CanonicalAddress,
                
                
            }).FirstOrDefault(x=>x.Id == id);
        }

        public string GetSlugBy(long id)
        {
            return _context.ArticleCategories.Select(x => new {x.Id, x.Slug}).First(x => x.Id == id).Slug;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
