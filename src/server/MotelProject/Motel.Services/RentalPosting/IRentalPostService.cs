
namespace Motel.Services.RentalPosting
{
    using Motel.Domain.Domain.Media;
    using Motel.Domain.Domain.Post;
    using Motel.Domain.Domain.UtilitiesRoom;
    using System.Collections.Generic;
    public interface IRentalPostService
    {
        #region Post
        IEnumerable<RentalPost>GetList(string titlePost, int? toMonthlyPrice, int? fromMonthlyPrice, 
            int? numberRoom, string address, int? PageIndex=0, int? PageSize=int.MaxValue);
        IEnumerable<RentalPost> GetListOfLester(int lesterId, int? PageIndex=0, int? PageSize=int.MaxValue);
        RentalPost GetById(int Id);
        void InsertPost(RentalPost post);
        void UppdatePost(RentalPost post);
        #endregion

        #region Post picture
        void InsertPictureForPost(int PictureId,int PostId);
        void InsertPicturesForPost(int[] PictureIds,int PostId);
        void DeletePictureForPost(int postId);
         IList<Picture> GetImageOfPost(int Post);
        #endregion
        #region Post Utilities
        void InsertUtilitieForPost(int UtilitieId,int PostId);
        void InsertUtilitiesForPost(int[] UtilitieIds,int PostId);
        void DeleteUtilitiesOfPost(int postId);
        IList<UtilitiesRoom> GetUtilitiesOfPost(int Post);
        #endregion

        #region Post_Rental_Posting
        IList<RentalPost> GetListOfCategory(int categoryId);
        #endregion
    }
}
