using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contract;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long,Order> , IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext; 

        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var result = _context.Orders.Select(x => new {x.PayAmount, x.Id}).FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                return result.PayAmount;
            }
            return 0;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x=> new {x.Id,x.Fullname});
            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                CreationDate = x.CreationDate.ToFarsi(),
                IsPayed = x.IsPayed,
                IssueTrackingNo = x.IssueTrackingNo,
                PaymentMethodId = x.PaymentMethod,
                PayAmount = x.PayAmount,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount, 
            });

            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);

            if (!string.IsNullOrWhiteSpace(searchModel.IssueTrackingNo))
            {
                query = query.Where(x => x.IssueTrackingNo.Contains(searchModel.IssueTrackingNo));
            }

            var orders = query.OrderByDescending(x => x.Id).ToList();
            foreach (var order in orders)
            {
                order.AccountFullname = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
                order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId).Name;
            }

            return orders;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                Id = x.Id,
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return items;
        }
    }
}
