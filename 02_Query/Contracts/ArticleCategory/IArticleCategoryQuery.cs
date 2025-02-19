namespace _02_Query.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategory(string id);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
