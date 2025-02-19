using _01_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public string Keywords { get; private set; }
        public List<Product> Products { get; private set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }

        public ProductCategory(string name, string description, string picture, string pictureAlt, string pictureTitle, string slug, string metaDescription, string keywords)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
        }

        public void Edit(string name, string description, string picture, string pictureAlt, string pictureTitle, string slug, string metaDescription, string keywords)
        {
            Name = name;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
        }
    }
}
