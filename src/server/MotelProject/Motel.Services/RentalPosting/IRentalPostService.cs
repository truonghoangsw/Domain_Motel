using Motel.Core;
using Motel.Domain.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.RentalPosting
{
    public interface IRentalPostService
    {
        #region Post
        RentalPost GetById(int Id);
        void InsertPost(RentalPost post);
        void UppdatePost(RentalPost post);
        #endregion

        #region Post picture
        void InsertPictureForPost(int PictureId,int PostId);
        void InsertPicturesForPost(int[] PictureIds,int PostId);
        void DeletePictureForPost(int postId);
        #endregion
        #region Post Utilities
        void InsertUtilitieForPost(int UtilitieId,int PostId);
        void InsertUtilitiesForPost(int[] UtilitieIds,int PostId);
        void DeleteUtilitiesOfPost(int postId);
        #endregion
    }
}
