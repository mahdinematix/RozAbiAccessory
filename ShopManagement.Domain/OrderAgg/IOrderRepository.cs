using _01_Framework.Domain;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepositoryBase<long , Order>
    {
        double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);


    }
}
