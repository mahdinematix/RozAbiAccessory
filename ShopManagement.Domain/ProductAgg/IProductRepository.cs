using _01_Framework.Domain;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepositoryBase<long,Product>
    {
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();

        EditProduct GetDetails(long id);

        Product GetProductWithCategory(long id);


    }
}
