namespace ShopManagement.Infrastructure.Configuration.Permission
{
    public static class ShopPermissions
    {
        //Product
        public const int ListProducts = 10;
        public const int CreateProduct = 11;
        public const int EditProduct = 12;


        //ProductCategory
        public const int ListProductCategories = 20;
        public const int CreateProductCategory = 21;
        public const int EditProductCategory = 22;

        //Slide
        public const int ListSlides = 30;
        public const int CreateSlide = 31;
        public const int EditSlide = 32;
        public const int RemoveSlide = 33;
        public const int RestoreSlide = 34;

        //ProductPictures
        public const int ListProductPictures = 40;
        public const int CreateProductPicture = 41;
        public const int EditProductPicture = 42;
        public const int RemoveProductPicture = 43;
        public const int RestoreProductPicture = 44;
    }
}
