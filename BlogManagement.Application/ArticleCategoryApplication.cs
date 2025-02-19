using _01_Framework.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exists(x=>x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }


            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, slug);

            var articleCategory = new ArticleCategory(command.Name, fileName, command.PictureAlt,command.PictureTitle,command.ShowOrder, command.Description,
                slug, command.MetaDescription,command.Keywords,command.CanonicalAddress);
            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.GetBy(command.Id);
            if (articleCategory == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }


            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, slug);

             articleCategory.Edit(command.Name, fileName, command.PictureAlt, command.PictureTitle, command.ShowOrder, command.Description,
                slug, command.MetaDescription,command.Keywords,command.CanonicalAddress);
            _articleCategoryRepository.Save();
            return operation.Succeed();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }
    }
}
