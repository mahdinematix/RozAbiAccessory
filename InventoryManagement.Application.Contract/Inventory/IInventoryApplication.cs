﻿using _01_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(ReduceInventory command);
        OperationResult ReduceFromOrder(List<ReduceInventory> command);
        List<InventoryOperationsViewModel> GetLog(long inventoryId);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel  searchModel);

    }
}
