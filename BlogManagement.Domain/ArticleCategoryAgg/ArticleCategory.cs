using _01_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public int ShowOrder { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public List<Article> Articles { get; private set; }


        public ArticleCategory(string name, string picture,string pictureAlt,string pictureTitle, int showOrder, string description, string slug, string metaDescription, string keywords,string canonicalAddress)
        {
            Name = name;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ShowOrder = showOrder;
            Description = description;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
            Articles = new List<Article>();
        }

        public void Edit(string name, string picture, string pictureAlt, string pictureTitle, int showOrder, string description, string slug,
            string metaDescription, string keywords, string canonicalAddress)
        {
            Name = name;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ShowOrder = showOrder;
            Description = description;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
        }
    }


}
