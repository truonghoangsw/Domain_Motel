using Motel.Core;
using Motel.Core.Caching;
using Motel.Domain;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Media;
using Motel.Domain.Domain.Post;
using Motel.Services.Caching;
using Motel.Services.Events;
using Motel.Services.Logging;
using Motel.Services.Media;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace Motel.Services.RentalPosting
{
    public class RentalPostService : IRentalPostService
    {
        #region Fields

        protected readonly ICacheKeyService _cacheKeyService;
        protected readonly IEventPublisher _eventPublisher;
        protected readonly IRepository<PostComment> _postCommentRepository;
        protected readonly IRepository<PostPictureMaping> _postPictureRepository;
        protected readonly IRepository<UtilitiesPostRental> _utilitiesPostRentalRepository;
        protected readonly IPermissionService permission;
        protected readonly ILogger _logger;
        protected readonly IRepository<RentalPost> _rentalPostRepository;
        protected readonly IStaticCacheManager _staticCacheManager;
        protected readonly IWorkContext _workContext;


        #endregion

        #region Ctor
        public RentalPostService(ICacheKeyService cacheKeyService,IEventPublisher eventPublisher,IWorkContext workContext,
            IRepository<UtilitiesPostRental> utilitiesPostRentalRepository
            ,IRepository<PostPictureMaping> postPictureRepository,
            IRepository<PostComment> postCommentRepository, 
            IRepository<RentalPost> rentalPostRepository
            ,IStaticCacheManager staticCacheManager,ILogger logger)
        {
            _logger = logger;
            _utilitiesPostRentalRepository = utilitiesPostRentalRepository;
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _postCommentRepository = postCommentRepository;
            _rentalPostRepository = rentalPostRepository;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
            _postPictureRepository = postPictureRepository;
        }
        #endregion

        #region Post Rentel
        public RentalPost GetById(int Id)
        {
            return _rentalPostRepository.GetById(Id);
        }

        public void InsertPost(RentalPost post)
        {
            post.LesterId = 15;
            post.CreateDate =DateTime.Now;
            post.UpdateDate =DateTime.Now;
            _rentalPostRepository.Insert(post);
        }

       
        public void UppdatePost(RentalPost post)
        {
            post.UpdateDate =DateTime.Now;
             _rentalPostRepository.Update(post);
        }

        #endregion

        #region Post picture
        public void InsertPictureForPost(int PictureId, int PostId)
        {
            PostPictureMaping pictureMaping = new PostPictureMaping()
            {
                PictureId = PictureId,
                PostId = PostId,
            };
            _postPictureRepository.Insert(pictureMaping);
        }
        public void InsertPicturesForPost(int[] PictureIds, int PostId)
        {
            foreach (var Id in PictureIds)
            {
                InsertPictureForPost(Id,PostId);
            }
        }

        public void DeletePictureForPost(int postId)
        {
            var lstPicture = _postPictureRepository.Table.Where(x=>x.PostId == postId).ToList();
            lstPicture.ForEach(picture=> _postPictureRepository.Delete(picture));
        }
        #endregion
        #region Post Utilities
        public void InsertUtilitieForPost(int UtilitieId, int PostId)
        {
            UtilitiesPostRental utilitiesPostRental = new UtilitiesPostRental()
            {
                PostRental = PostId,
                UtilitiesId = UtilitieId
            };
              _utilitiesPostRentalRepository.Insert(utilitiesPostRental);
        }

        public void InsertUtilitiesForPost(int[] UtilitieIds, int PostId)
        {
            foreach (var Id in UtilitieIds)
            {
                InsertUtilitieForPost(Id,PostId);
            }
        }

       

        public void DeleteUtilitiesOfPost(int postId)
        {
             var lstUtilities = _utilitiesPostRentalRepository.Table.Where(x=>x.PostRental == postId).ToList();
            lstUtilities.ForEach(utilitie=> _utilitiesPostRentalRepository.Delete(lstUtilities));
        }
        #endregion



    }
}
