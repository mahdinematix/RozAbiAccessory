using _01_Framework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepositoryBase<long,Inventory>
    {
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationsViewModel> GetLog(long inventoryId);
        Inventory GetByProductId(long productId);
    }
}
