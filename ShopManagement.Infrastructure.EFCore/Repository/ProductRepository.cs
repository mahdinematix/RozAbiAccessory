using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long,Product> , IProductRepository

    {
        private readonly ShopContext _shopContext;

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _shopContext.Products.Include(x => x.Category).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Category = x.Category.Name,
                Code = x.Code,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.CategoryId !=0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<ProductViewModel> GetProducts()
        {
            return _shopContext.Products.Select(x => new ProductViewModel
            {
Id = x.Id,
Name = x.Name
            }).ToList();
        }

        public EditProduct GetDetails(long id)
        {
            return _shopContext.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Description =  x.Description,
                CategoryId = x.CategoryId,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Code = x.Code,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                MetaDescription = x.MetaDescription,
                Keywords = x.Keywords
            }).FirstOrDefault(z => z.Id == id);
        }

        public Product GetProductWithCategory(long id)
        {
            return _shopContext.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }


        public ProductRepository(ShopContext context) : base(context)
        {
            _shopContext = context;
        }
    }
}
