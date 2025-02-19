using _01_Framework.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepositoryBase<long,Article>
    {
        EditArticle GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        Article GetWithCategory(long id);
    }
}
