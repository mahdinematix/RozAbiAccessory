using _02_Query.Contracts.ArticleCategory;
using _02_Query.Contracts.ProductCategory;
using _02_Query.Profile;

namespace _02_Query
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
        public ProfileQueryModel AccountDetails { get; set; }
    }
}
