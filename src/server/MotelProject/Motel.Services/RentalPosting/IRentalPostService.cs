using Motel.Core;
using Motel.Domain.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.RentalPosting
{
    public interface IRentalPostService
    {
        #region Rental post
        /// <summary>
        /// Deletes a blog post
        /// </summary>
        /// <param name="rentalPost">Blog post</param>
        void DeleteRentalPost(RentalPost rentalPost);

        /// <summary>
        /// Gets a Rental Post
        /// </summary>
        /// <param name="blogPostId">Blog post identifier</param>
        /// <returns>Blog post</returns>
        RentalPost GetRentalPostById(int rentalPost);

        /// <summary>
        /// Gets all Rental Post
        /// </summary>

        /// <param name="dateFrom">Filter by created date; null if you want to get all records</param>
        /// <param name="dateTo">Filter by created date; null if you want to get all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="title">Filter by blog post title</param>
        /// <returns>Blog posts</returns>
        IPagedList<RentalPost> GetAllRentalPost(string nameWard, string nameDistrict, string nameProvincial, string tag,
            DateTime? dateFrom = null, DateTime? dateTo = null, int monthlyPrice = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, string title = null);

        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <param name="storeId">The store identifier; pass 0 to load all records</param>
        /// <param name="languageId">Language identifier. 0 if you want to get all blog posts</param>
        /// <param name="tag">Tag</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Blog posts</returns>   
        IPagedList<RentalPost> GetAllRentalPostByTag(string tag = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Gets all blog post tags
        /// </summary>
        /// <param name="storeId">The store identifier; pass 0 to load all records</param>
        /// <param name="languageId">Language identifier. 0 if you want to get all blog posts</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Blog post tags</returns>
        IList<PostTag> GetAllRentalPostTags(int languageId, bool showHidden = false);

        /// <summary>
        /// Inserts a blog post
        /// </summary>
        /// <param name="blogPost">Blog post</param>
        void InsertRentalPost(RentalPost rentalPost);

        /// <summary>
        /// Updates the blog post
        /// </summary>
        /// <param name="blogPost">Blog post</param>
        void UpdateRentelPost(RentalPost rentalPost);

        /// <summary>
        /// Returns all posts published between the two dates.
        /// </summary>
        /// <param name="blogPosts">Source</param>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateTo">Date to</param>
        /// <returns>Filtered posts</returns>
        IList<RentalPost> GetPostsByDate(IList<RentalPost> rentalPost, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Parse tags
        /// </summary>
        /// <param name="blogPost">Blog post</param>
        /// <returns>Tags</returns>
        IList<string> ParseTags(RentalPost rentalPost);

        /// <summary>
        /// Get a value indicating whether a blog post is available now (availability dates)
        /// </summary>
        /// <param name="blogPost">Blog post</param>
        /// <param name="dateTime">Datetime to check; pass null to use current date</param>
        /// <returns>Result</returns>
        bool BlogPostIsAvailable(RentalPost rentalPost, DateTime? dateTime = null);

        #endregion

        #region pictures post

        void DeletedPostPicture(PostPictureMaping postPicture);



        #endregion
    }
}
