using _01_Framework.Application;
using _02_Query.Contracts.Article;
using _02_Query.Contracts.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _02_Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;


        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _blogContext.Articles.Include(x => x.Category).Where(x => x.PublishDate <= DateTime.Now).Select(
                x => new ArticleQueryModel
                {
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                }).ToList();
        }

        public ArticleQueryModel GetArticleDetails(string id)
        {
            var article = _blogContext.Articles.Include(x => x.Category).Where(x => x.PublishDate <= DateTime.Now).Select(
                x => new ArticleQueryModel
                {
                    Id= x.Id,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    CanonicalAddress = x.CanonicalAddress,
                    Description = x.Description,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    CategorySlug = x.Category.Slug,
                    CategoryName = x.Category.Name,
                    MetaDescription = x.MetaDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Keywords = x.Keywords
                   
                   
                }).FirstOrDefault(x=>x.Slug == id);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split(",").ToList();

            var comments = _commentContext.Comments
                .Where(x => !x.IsCancel)
                .Where(x => x.IsConfirm)
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId).Name;
            }

            article.Comments = comments;

            return article;
        }
    }
}
