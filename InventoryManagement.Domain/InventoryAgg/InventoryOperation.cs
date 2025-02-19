namespace InventoryManagement.Domain.InventoryAgg;

public class InventoryOperation
{
    public long Id { get; private set; }
    public long OrderId { get; private set; }
    public long OperatorId { get; private set; }
    public bool Operation { get; private set; }
    public long InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }
    public string Description { get; private set; }
    public long Count { get; private set; }
    public DateTime OperationDate { get; private set; }
    public long CurrentCount { get; private set; }

    public InventoryOperation(long orderId, long operatorId, bool operation, long inventoryId, string description, long count, long currentCount)
    {
        OrderId = orderId;
        OperatorId = operatorId;
        Operation = operation;
        InventoryId = inventoryId;
        Description = description;
        Count = count;
        CurrentCount = currentCount;
        OperationDate=DateTime.Now;
    }
}