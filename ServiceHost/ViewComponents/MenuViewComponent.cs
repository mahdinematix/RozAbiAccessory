using _01_Framework.Application;
using _02_Query;
using _02_Query.Contracts.ArticleCategory;
using _02_Query.Contracts.ProductCategory;
using _02_Query.Profile;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {

        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IProfileQuery _profileQuery;
        private readonly IAuthHelper _authHelper;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery, IProfileQuery profileQuery, IAuthHelper authHelper)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
            _profileQuery = profileQuery;
            _authHelper = authHelper;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                ProductCategories = _productCategoryQuery.GetProductCategories(),
                AccountDetails = _profileQuery.GetAccountDetails(_authHelper.CurrentAccountId())
            };
            return View(result);
        }
    }
}
