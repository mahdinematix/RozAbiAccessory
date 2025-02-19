using _01_Framework.Application;
using _01_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                CanonicalAddress = x.CanonicalAddress,
                Title = x.Title
            }).First(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription.Substring(0,Math.Min(x.ShortDescription.Length,50)) + "...",
                Title = x.Title
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }

            if (searchModel.CategoryId > 0) 
            {
                query = query.Where(x=>x.CategoryId == searchModel.CategoryId);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public Article GetWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category).First(x=>x.CategoryId==id);
            
        }
    }
}
