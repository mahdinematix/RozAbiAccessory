using _01_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permission
{
    public class ShopPermissionExposer : IPermissionExposer 
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateProduct, "CreateProduct"),
                        new PermissionDto(ShopPermissions.EditProduct, "EditProduct"),
                        new PermissionDto(ShopPermissions.ListProducts, "ListProducts"),
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateProductCategory,"CreateProductCategory"),
                        new PermissionDto(ShopPermissions.EditProductCategory,"EditProductCategory"),
                        new PermissionDto(ShopPermissions.ListProductCategories,"ListProductCategories"),
                    }
                },
                {
                    "Slide", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateSlide,"CreateSlide"),
                        new PermissionDto(ShopPermissions.EditSlide,"EditSlide"),
                        new PermissionDto(ShopPermissions.ListSlides,"ListSlides"),
                        new PermissionDto(ShopPermissions.RemoveSlide,"RemoveSlide"),
                        new PermissionDto(ShopPermissions.RestoreSlide,"RestoreSlide"),
                    }
                },
                {
                    "ProductPictures", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.CreateProductPicture,"CreateProductPicture"),
                        new PermissionDto(ShopPermissions.EditProductPicture,"EditProductPicture"),
                        new PermissionDto(ShopPermissions.ListProductPictures,"ListProductPictures"),
                        new PermissionDto(ShopPermissions.RemoveProductPicture,"RemoveProductPicture"),
                        new PermissionDto(ShopPermissions.RestoreProductPicture,"RestoreProductPicture"),
                    }
                }
            };
        }
    }
}
