using _01_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var folderName = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);
            var productCategory = new ProductCategory(command.Name, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, slug, command.MetaDescription, command.Keywords);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.Save();
            return operation.Succeed();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(command.Id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var folderName = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);
            productCategory.Edit(command.Name, command.Description, fileName,
    command.PictureAlt, command.PictureTitle, slug, command.MetaDescription, command.Keywords);
            _productCategoryRepository.Save();
            return operation.Succeed();
        }

        public EditProductCategory Getdetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }
    }
}
