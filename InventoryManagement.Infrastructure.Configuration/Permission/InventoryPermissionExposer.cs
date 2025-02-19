using _01_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.Permission
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.CreateInventory, "CreateInventory"),
                        new PermissionDto(InventoryPermissions.EditInventory,"EditInventory"),
                        new PermissionDto(InventoryPermissions.ListInventory, "ListInventory"),
                        new PermissionDto(InventoryPermissions.ReduceInventory,"ReduceInventory"),
                        new PermissionDto(InventoryPermissions.IncreaseInventory,"IncreaseInventory"),
                        new PermissionDto(InventoryPermissions.OperationLog,"OperationLog")

                    }
                }
            };
        }
    }
}
