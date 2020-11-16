using Motel.Core;
using Motel.Core.Caching;
using Motel.Core.Enum;
using Motel.Domain;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Post;
using Motel.Services.Caching;
using Motel.Services.Caching.Extensions;
using Motel.Services.Events;
using Motel.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motel.Services.RentalPosting
{
    public class CategoryService : ICategoryService
    {
         #region Fields

        private readonly ICacheKeyService _cacheKeyService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<RentalPost> _postRepository;
        private readonly IRepository<PostCategoryMapping> _postCategoryRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;
        private readonly ILogger _logger;

        public CategoryService(ICacheKeyService cacheKeyService, IEventPublisher eventPublisher, IRepository<Category> categoryRepository, IRepository<RentalPost> postRepository, 
            IRepository<PostCategoryMapping> postCategoryRepository, IStaticCacheManager staticCacheManager, IWorkContext workContext,ILogger logger)
        {
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
            _postCategoryRepository = postCategoryRepository;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
            _logger = logger;
        }

        public void DeleteCategories(IList<Category> categories)
        {
             if (categories == null)
                throw new ArgumentNullException(nameof(categories));

            foreach (var category in categories)
            {
                DeleteCategory(category);
            }
        }

        #endregion
        public void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            category.Deleted = true;
            UpdateCategory(category);

            //event notification
            _eventPublisher.EntityDeleted(category);
          
        }

        public void DeletePostCategory(PostCategoryMapping productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException(nameof(productCategory));
            _postCategoryRepository.Delete(productCategory);
            _eventPublisher.EntityDeleted(productCategory);
        }

        public IPagedList<Category> GetAllCategories(string categoryName, int? pageIndex = 0, int? pageSize = int.MaxValue, bool showHidden = false, bool? overridePublished = null)
        {
            var query = _categoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!string.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Name.Contains(categoryName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            if (overridePublished.HasValue)
                query = query.Where(c => c.Published == overridePublished.Value);
            query = query.Distinct().OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder).ThenBy(c => c.Id);

            var unsortedCategories = query.ToList();

            return new PagedList<Category>(unsortedCategories, pageIndex.Value, pageSize.Value);

        }

        public IList<Category> GetAllCategories( bool showHidden = false)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelCatalogDefaults.CategoriesAllCacheKey,showHidden);

            var categories = _staticCacheManager.Get(key, () => GetAllCategories(string.Empty,showHidden: showHidden).ToList());

            return categories;
        }

        public IList<Category> GetAllCategoriesDisplayedOnHomepage(bool showHidden = false)
        {
              var query = from c in _categoryRepository.Table
                        orderby c.DisplayOrder, c.Id
                        where c.Published &&
                        !c.Deleted &&
                        c.ShowOnHomepage
                        select c;

            var categories = query.ToCachedList(_cacheKeyService.PrepareKeyForDefaultCache(MotelCatalogDefaults.CategoriesAllDisplayedOnHomepageCacheKey));
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
           if (categoryId == 0)
                return null;

            return _categoryRepository.ToCachedGetById(categoryId);
        }

        public IPagedList<PostCategoryMapping> GetPostCategoriesByCategoryId(int categoryId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
             if (categoryId == 0)
                return new PagedList<PostCategoryMapping>(new List<PostCategoryMapping>(), pageIndex, pageSize);
            var query = from pc in _postCategoryRepository.Table
                    join p in _postRepository.Table on pc.RentalPostId equals p.Id
                    where pc.CategoryId == categoryId &&
                            p.Status != (int)StatusPost.Delete &&
                            (showHidden || p.Status == (int)StatusPost.Approved)
                    orderby pc.DisplayOrder, pc.Id
                    select pc;
             query = query.Distinct().OrderBy(pc => pc.DisplayOrder).ThenBy(pc => pc.Id);
             var postCategories = new PagedList<PostCategoryMapping>(query, pageIndex, pageSize);
            return postCategories;

        }

        public IList<PostCategoryMapping> GetPostCategoriesByPostId(int postId, bool showHidden = false)
        {
            if (postId == 0)
                return new List<PostCategoryMapping>();
            var query = from pc in _postCategoryRepository.Table
                    join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                    where pc.RentalPostId == postId &&
                            !c.Deleted &&
                            (showHidden || c.Published)
                    orderby pc.DisplayOrder, pc.Id
                    select pc;
             query = query.Distinct().OrderBy(pc => pc.DisplayOrder).ThenBy(pc => pc.Id);
            return query.ToList();
        }

        public PostCategoryMapping GetPostCategoryMappingById(int PostCategoryMappingId)
        {
              if (PostCategoryMappingId == 0)
                return new PostCategoryMapping();
            return _postCategoryRepository.ToCachedGetById(PostCategoryMappingId);
        }

        public void InsertCategory(Category category)
        {
             if (category == null)
                throw new ArgumentNullException(nameof(category));

            _categoryRepository.Insert(category);

            //event notification
            _eventPublisher.EntityInserted(category);
        }

        public void InsertPostCategoryMapping(PostCategoryMapping postCategoryMapping)
        {
           if (postCategoryMapping == null)
                throw new ArgumentNullException(nameof(postCategoryMapping));

            _postCategoryRepository.Insert(postCategoryMapping);

            //event notification
            _eventPublisher.EntityInserted(postCategoryMapping);
        }

        public void UpdateCategory(Category category)
        {
             if (category == null)
                throw new ArgumentNullException(nameof(category));

            //validate category hierarchy
            var parentCategory = GetCategoryById(category.ParentCategoryId);
            while (parentCategory != null)
            {
                if (category.Id == parentCategory.Id)
                {
                    category.ParentCategoryId = 0;
                    break;
                }

                parentCategory = GetCategoryById(parentCategory.ParentCategoryId);
            }

            _categoryRepository.Update(category);

            //event notification
            _eventPublisher.EntityUpdated(category);
        }

        public void UpdatePostCategoryMapping(PostCategoryMapping postCategoryMapping)
        {
             if (postCategoryMapping == null)
                throw new ArgumentNullException(nameof(postCategoryMapping));

            _postCategoryRepository.Update(postCategoryMapping);

            //event notification
            _eventPublisher.EntityUpdated(postCategoryMapping);
        }
    }
}
