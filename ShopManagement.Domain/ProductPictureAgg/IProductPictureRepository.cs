using _01_Framework.Domain;
using ShopManagement.Application.Contract.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepositoryBase<long ,ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        ProductPicture GetProductPictureWithProductAndCategory(long  id);
    }
}
