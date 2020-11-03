using Motel.Core;
using Motel.Domain.Domain.Post;
using System.Collections.Generic;

namespace Motel.Services.RentalPosting
{
    public interface ICategoryService
    {
        IList<Category> GetAllCategoriesDisplayedOnHomepage(bool showHidden = false);

        void DeletePostCategory(PostCategoryMapping productCategory);
        void DeleteCategories(IList<Category> categories);
        void UpdateCategory(Category category);
        void InsertCategory(Category category);
        Category GetCategoryById(int categoryId);

         IPagedList<Category> GetAllCategories(string categoryName, 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? overridePublished = null);
        IList<Category> GetAllCategories(bool showHidden = false);

        void DeleteCategory(Category category);

        IPagedList<PostCategoryMapping> GetPostCategoriesByCategoryId(int categoryId,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        IList<PostCategoryMapping> GetPostCategoriesByPostId(int productId, bool showHidden = false);
        PostCategoryMapping GetPostCategoryMappingById(int PostCategoryMappingId);
        void InsertPostCategoryMapping(PostCategoryMapping postCategoryMapping);
        void UpdatePostCategoryMapping(PostCategoryMapping postCategoryMapping);

    }
}