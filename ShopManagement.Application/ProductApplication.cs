using _01_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x=>x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slug = command.Slug.Slugify();
            var categorySlug = _categoryRepository.GetSlugBy(command.CategoryId);
            var folderName = $"{categorySlug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);

            var product = new Product(command.Name, command.Code, command.CategoryId, command.ShortDescription, command.Description, fileName, command.PictureAlt,
                command.PictureTitle, slug, command.Keywords, command.MetaDescription);
            _productRepository.Create(product);
            _productRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();
            var folderName = $"{product.Category.Slug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);

        product.Edit(command.Name, command.Code, command.CategoryId, command.ShortDescription, command.Description,fileName, command.PictureAlt,
                command.PictureTitle, slug, command.Keywords, command.MetaDescription);
            _productRepository.Save();
            return operation.Succeed();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
