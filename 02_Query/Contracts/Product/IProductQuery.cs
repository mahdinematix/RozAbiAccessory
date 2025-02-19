
using ShopManagement.Application.Contract.Order;

namespace _02_Query.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetDetails(string slug);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);

    }
}
