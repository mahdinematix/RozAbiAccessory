
using ShopManagement.Application.Contract.Order;

namespace _02_Query.Contracts
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
