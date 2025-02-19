using _01_Framework.Infrastructure;

namespace DiscountManagement.Infrastructure.Configuration.Permission
{
    public class DiscountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "ColleagueDiscount", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.ListColleagueDiscounts,"ListColleagueDiscounts"),
                        new PermissionDto(DiscountPermissions.DefineColleagueDiscount,"DefineColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.EditColleagueDiscount,"EditColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.RemoveColleagueDiscount,"RemoveColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.RestoreColleagueDiscount,"RestoreColleagueDiscount")
                    }
                },
                {
                    "CustomerDiscount",new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.ListCustomerDiscounts,"ListCustomerDiscounts"),
                        new PermissionDto(DiscountPermissions.DefineCustomerDiscount,"DefineCustomerDiscount"),
                        new PermissionDto(DiscountPermissions.EditCustomerDiscount,"EditCustomerDiscount"),
                    }
                }
            };
        }
    }
}
