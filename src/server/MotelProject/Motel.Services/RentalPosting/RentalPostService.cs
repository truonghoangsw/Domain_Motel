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
        protected readonly IRepository<PostCategoryMapping> _postCategoryRepository;
        protected readonly IRepository<PostPictureMaping> _postPictureRepository;
        protected readonly IPermissionService permission;
        protected readonly IPictureService _pictureService;
        protected readonly ILogger _logger;
        protected readonly IRepository<RentalPost> _rentalPostRepository;
        protected readonly IStaticCacheManager _staticCacheManager;
        protected readonly IWorkContext _workContext;


        #endregion
        #region Ctor
        public RentalPostService(ICacheKeyService cacheKeyService,IEventPublisher eventPublisher,IWorkContext workContext,
            IPictureService pictureService,IRepository<PostCategoryMapping> postCategoryRepository,IRepository<PostPictureMaping> postPictureRepository,
            IRepository<PostComment> postCommentRepository, IRepository<RentalPost> rentalPostRepository
            ,IStaticCacheManager staticCacheManager,ILogger logger)
        {
            _logger = logger;
            _postCategoryRepository = postCategoryRepository;
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _postCommentRepository = postCommentRepository;
            _rentalPostRepository = rentalPostRepository;
            _staticCacheManager = staticCacheManager;
            _pictureService = pictureService;
            _workContext = workContext;
            _postPictureRepository = postPictureRepository;
        }
        #endregion

        #region Post Rentel
        public IPagedList<RentalPost> GetAllRentalPost(string nameWard, string nameDistrict, string nameProvincial, string tag,
            DateTime? dateFrom = null, DateTime? dateTo = null, int monthlyPrice = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, string title = null)
        {

        }
        #endregion

        #region Post Picture

        #endregion

         #region Post Category

        #endregion
    }
}
