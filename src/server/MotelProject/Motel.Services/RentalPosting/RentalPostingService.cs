using Motel.Core;
using Motel.Core.Caching;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Media;
using Motel.Domain.Domain.Post;
using Motel.Services.Caching;
using Motel.Services.Events;
using Motel.Services.Logging;
using Motel.Services.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.RentalPosting
{
    public class RentalPostingService : IRentalPostingService
    {
         #region Fields

        private readonly ICacheKeyService _cacheKeyService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<PostComment> _postCommentRepository;
        private readonly IPictureService _pictureService;
        private readonly ILogger _logger;
        private readonly IRepository<RentalPost> _rentalPostRepository;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion
        #region Ctor
        public RentalPostingService(ICacheKeyService cacheKeyService,IEventPublisher eventPublisher, IPictureService pictureService,
            IRepository<PostComment> postCommentRepository, IRepository<RentalPost> rentalPostRepository,IStaticCacheManager staticCacheManager,ILogger logger)
        {
            _logger = logger;
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _postCommentRepository = postCommentRepository;
            _rentalPostRepository = rentalPostRepository;
            _staticCacheManager = staticCacheManager;
            _pictureService = pictureService;
        }
        #endregion
       
        public bool BlogPostIsAvailable(RentalPost rentalPost, DateTime? dateTime = null)
        {
            try
            {
                if(rentalPost == null)
                    return false;

            }
            catch (Exception ex)
            {
                _logger.Error("Error BlogPostIsAvailable",ex);
            }
        }

        public void DeleteRentalPost(RentalPost rentalPost)
        {
            throw new NotImplementedException();
        }

        public IPagedList<RentalPost> GetAllRentalPost(string nameWard, string nameDistrict, string nameProvincial, string tag, DateTime? dateFrom = null, DateTime? dateTo = null, int monthlyPrice = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, string title = null)
        {
            throw new NotImplementedException();
        }

        public IPagedList<RentalPost> GetAllRentalPostByTag(string tag = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<PostTag> GetAllRentalPostTags(int languageId, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<RentalPost> GetPostsByDate(IList<RentalPost> rentalPost, DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }

        public RentalPost GetRentalPostById(int rentalPost)
        {
            throw new NotImplementedException();
        }

        public void InsertBlogPost(RentalPost rentalPost)
        {
            throw new NotImplementedException();
        }

        public IList<string> ParseTags(RentalPost rentalPost)
        {
            throw new NotImplementedException();
        }

        public void UpdateBlogPost(RentalPost rentalPost)
        {
            throw new NotImplementedException();
        }
    }
}
