﻿using _01_Framework.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {

        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();

            if (_articleRepository.Exists(x => x.Title == command.Title))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var folderName = $"{categorySlug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);
            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var article = new Article(command.Title, fileName, command.PictureAlt, command.PictureTitle,
                command.ShortDescription, command.Description, slug, command.Keywords, command.MetaDescription,
                command.CategoryId, publishDate,command.CanonicalAddress);

            _articleRepository.Create(article);
            _articleRepository.Save();
            return operation.Succeed();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.GetWithCategory(command.CategoryId);
            if (article == null)
            {
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            }

            if (_articleRepository.Exists(x=>x.Title == command.Title && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slug = command.Slug.Slugify();
            var folderName = $"{article.Category.Slug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, folderName);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, fileName, command.PictureAlt, command.PictureTitle,
                command.ShortDescription, command.Description, slug, command.Keywords, command.MetaDescription,
                command.CategoryId, publishDate,command.CanonicalAddress);

            _articleRepository.Save();
            return operation.Succeed();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
