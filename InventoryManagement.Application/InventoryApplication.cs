using _01_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IAuthHelper _authHelper;

        public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
        {
            _inventoryRepository = inventoryRepository;
            _authHelper = authHelper;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x=>x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.Save();
            return operation.Succeed();

        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.Id);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id !=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            inventory.Edit(command.ProductId,command.UnitPrice);
            _inventoryRepository.Save();
            return operation.Succeed();


        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            var operatorId = _authHelper.CurrentAccountId();
            inventory.Increase(command.Count, operatorId,command.Description);
            _inventoryRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            var operatorId = _authHelper.CurrentAccountId();
            inventory.Reduce(command.Count, operatorId, command.Description,command.OrderId);
            _inventoryRepository.Save();
            return operation.Succeed();
        }

        public OperationResult ReduceFromOrder(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            var operatorId = _authHelper.CurrentAccountId();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetByProductId(item.ProductId);
                inventory.Reduce(item.Count,operatorId,item.Description,item.OrderId);
            }

            _inventoryRepository.Save();
            return operation.Succeed();
        }

        public List<InventoryOperationsViewModel> GetLog(long inventoryId)
        {
            return _inventoryRepository.GetLog(inventoryId);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}
