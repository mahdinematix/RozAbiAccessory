using _01_Framework.Application;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var folderName = $"{product.Category.Slug}/{product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);
            var productPicture = new ProductPicture(command.ProductId, fileName, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetProductPictureWithProductAndCategory(command.Id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }
            var folderName = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);
            productPicture.Edit(command.ProductId,fileName,command.PictureAlt,command.PictureTitle);
            _productPictureRepository.Save();
            return operation.Succeed();

        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }
            productPicture.Remove();
            _productPictureRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }
            productPicture.Restore();
            _productPictureRepository.Save();
            return operation.Succeed();
        }
    }
}
