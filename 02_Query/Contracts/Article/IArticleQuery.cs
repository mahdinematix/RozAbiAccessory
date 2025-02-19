namespace _02_Query.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticles();
        ArticleQueryModel GetArticleDetails(string id);

    }
}
