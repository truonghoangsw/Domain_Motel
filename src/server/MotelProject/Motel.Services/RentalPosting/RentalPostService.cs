namespace Motel.Services.RentalPosting
{
    using Motel.Core.Caching;
    using Motel.Domain;
    using Motel.Domain.ContextDataBase;
    using Motel.Domain.Domain.Post;
    using Motel.Services.Caching;
    using Motel.Services.Events;
    using Motel.Services.Logging;
    using Motel.Services.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Motel.Domain.Domain.Territories;
    using Motel.Services.Territories;
    using Motel.Domain.Domain.Media;
    using Motel.Domain.Domain.UtilitiesRoom;
    using Motel.Core.Enum;

    public class RentalPostService : IRentalPostService
    {
        #region Fields

        protected readonly ICacheKeyService _cacheKeyService;
        protected readonly IEventPublisher _eventPublisher;
        protected readonly IRepository<PostComment> _postCommentRepository;
        protected readonly IRepository<PostPictureMaping> _postPictureRepository;
        protected readonly ITerritoriesServices _territoriesServices;
        protected readonly IRepository<UtilitiesPostRental> _utilitiesPostRentalRepository;
        protected readonly IPermissionService permission;
        protected readonly ILogger _logger;
        protected readonly IRepository<UtilitiesRoom> _utilitiesRoomRepository;
        protected readonly IRepository<RentalPost> _rentalPostRepository;
        protected readonly IStaticCacheManager _staticCacheManager;
        protected readonly IRepository<Picture>_pictureRepository;
        protected readonly IWorkContext _workContext;
        private readonly IRepository<UtilitiesPostRental> _utilitiesMapRepository;

        #endregion

        #region Ctor
        public RentalPostService(ICacheKeyService cacheKeyService,IEventPublisher eventPublisher,IWorkContext workContext,
            IRepository<UtilitiesPostRental> utilitiesPostRentalRepository
            ,IRepository<PostPictureMaping> postPictureRepository,
            ITerritoriesServices territoriesServices,
            IRepository<UtilitiesRoom> utilitiesRoomRepository,
            IRepository<PostComment> postCommentRepository, 
            IRepository<Picture> pictureRepository,
            IRepository<UtilitiesPostRental> utilitiesMapRepository,
            IRepository<RentalPost> rentalPostRepository
            ,IStaticCacheManager staticCacheManager,ILogger logger)
        {
            _pictureRepository=pictureRepository;
            _territoriesServices= territoriesServices;
            _logger = logger;
            _utilitiesPostRentalRepository = utilitiesPostRentalRepository;
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _utilitiesRoomRepository = utilitiesRoomRepository;
            _postCommentRepository = postCommentRepository;
            _rentalPostRepository = rentalPostRepository;
            _utilitiesMapRepository =utilitiesMapRepository;

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
         public IEnumerable<int> GetPostCotainerUtilities(int[] Id)
        {
            var query = from ur in _utilitiesRoomRepository.Table
                        join urm in _utilitiesMapRepository.Table
                        on ur.Id equals urm.UtilitiesId
                        where Id.Contains(ur.Id)
                        select urm.PostRental;
            return query.ToList();
        }
        public IEnumerable<RentalPost> GetList(string titlePost, int? toMonthlyPrice, int? fromMonthlyPrice, 
            int? numberRoom, string address, int? PageIndex=0, int? PageSize=int.MaxValue,int[] Category = null,int[] Utilities = null,int LesterId = 0)
        {
          
            var query = _rentalPostRepository.Table;
            if(Category != null)
                query = query.Where(x=>Category.Contains(x.CategoryId));
            if(!string.IsNullOrEmpty(titlePost))
                query = query.Where(x=>x.TitlePost.Contains(titlePost));
            if(toMonthlyPrice.HasValue)
                query  = query.Where(x=>x.MonthlyPrice <= toMonthlyPrice);
             if(fromMonthlyPrice.HasValue)
                query  = query.Where(x=>x.MonthlyPrice >= fromMonthlyPrice);
             if(LesterId != 0)
                query  = query.Where(x=>x.LesterId == LesterId);
            if(!string.IsNullOrEmpty(address))
            {
                var lstTerritories = _territoriesServices.GetAllByName(address);
                query  = query.Where(x=>x.AddressDetail.ToLower().Contains(address.ToLower()));
            }
            if(Utilities != null)
            {
                var lstPostId = GetPostCotainerUtilities(Utilities);
                if(lstPostId?.Count() != null)
                    query = query.Where(x=>lstPostId.Contains(x.Id));
            }
            query  = query.Where(x=>x.Status == (byte)StatusPost.Approved);
           return query;
        }
        public IEnumerable<RentalPost> GetListOfLester(int lesterId, int? PageIndex=0, int? PageSize = int.MaxValue)
        {
            var query = _rentalPostRepository.Table.Where(x=>x.LesterId == lesterId);
            query = query.Take(PageSize.Value).Skip(PageSize.Value*PageIndex.Value);
            return query.ToList();
        }
        public void InsertPost(RentalPost post)
        {
            post.CreateDate =DateTime.Now;
            post.UpdateDate =DateTime.Now;
            post.Status =(byte)StatusPost.Approved;
            _rentalPostRepository.Insert(post);
        }

       
        public void UppdatePost(RentalPost post)
        {
            try
            {
                 post.UpdateDate =DateTime.Now;
                _rentalPostRepository.Update(post);
            }
            catch (Exception ex)
            {
                return ;
            }
           
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

        public IList<Picture> GetImageOfPost(int PostId)
        {
            var query = from pm in _postPictureRepository.Table join pt in  _pictureRepository.Table
                        on pm.PictureId equals pt.Id where pm.PostId ==PostId select pt;
            return query.ToList();
        }

        public IList<UtilitiesRoom> GetUtilitiesOfPost(int PostId)
        {
            var query = from ut in _utilitiesPostRentalRepository.Table join ur in  _utilitiesRoomRepository.Table
                        on ut.UtilitiesId equals  ur.Id where ut.PostRental ==PostId select ur;
            return query.ToList();
        }

        public IList<RentalPost> GetListOfCategory(int categoryId)
        {
            throw new NotImplementedException();
        }




        #endregion

    }
}
