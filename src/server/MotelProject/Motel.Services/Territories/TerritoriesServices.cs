
namespace Motel.Services.Territories
{
    using Motel.Core;
    using Motel.Core.Caching;
    using Motel.Domain;
    using Motel.Domain.ContextDataBase;
    using  Motel.Domain.Domain.Territories;
    using Motel.Services.Caching;
    using Motel.Services.Caching.Extensions;
    using Motel.Services.Events;
    using Motel.Services.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TerritoriesServices : ITerritoriesServices
    {
        #region Fields
        protected readonly IRepository<Territories> _territoriesRepository;
        protected readonly IStaticCacheManager _staticCacheManager;
        protected readonly IEventPublisher _eventPublisher;
        protected readonly ILogger _logger;
        protected readonly IWorkContext _workContext;
        protected readonly ICacheKeyService _cacheKeyService;
        #endregion

        #region Ctor
        public TerritoriesServices(IRepository<Territories> territoriesRepository,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager, 
            IEventPublisher eventPublisher)
        {
            _cacheKeyService = cacheKeyService;
            _territoriesRepository = territoriesRepository;
            _staticCacheManager = staticCacheManager;
            _eventPublisher = eventPublisher;
        }
        #endregion

        #region Method
        public void Create(Territories territories)
        {
            if(territories == null)
                throw new ArgumentNullException(nameof(territories));

           _territoriesRepository.Insert(territories);

            _eventPublisher.EntityInserted(territories);

        }

        public void Delete(int id)
        {
           if(id == 0)
                throw new ArgumentNullException(nameof(id));
            var territories = _territoriesRepository.GetById(id);
            if(territories == null)
               throw new ArgumentNullException(nameof(id));
            _territoriesRepository.Delete(territories);
            _eventPublisher.EntityDeleted(territories);
        }

        public void Edit(Territories territories)
        {
            if(territories == null)
                throw new ArgumentNullException(nameof(territories));

            _territoriesRepository.Update(territories);
            _eventPublisher.EntityUpdated(territories);

        }

        public IList<Territories> GetAll()
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(TerritoriesDefaults.TerritoriesAllCacheKey);
            return _staticCacheManager.Get(key,() => GetAllFilter().ToList() ) ;
        }

        public IPagedList<Territories> GetAllFilter( bool? OrderIndex =null,int? StatusId = null,
            string Name=default(string),  int PageIndex=0, int PageSize = int.MaxValue)
        {
            var query = _territoriesRepository.Table;
            if(StatusId.HasValue)
                query = query.Where(x=>x.Status == StatusId);
            if(!string.IsNullOrWhiteSpace(Name))
                query = query.Where(x=>x.Name.Contains(Name));
            query = query.Distinct();
            var unsortedTerritories = query.ToList();

            return new PagedList<Territories>(unsortedTerritories, PageIndex, PageSize);
        }

        public IPagedList<Territories> GetAllParent(int? ParentId ,int? StatusId = null, 
            int? PageIndex = 0, int? PageSize = int.MaxValue,string Name ="")
        {
            var query = _territoriesRepository.Table;
            if(StatusId.HasValue)
                query = query.Where(x=>x.Status == StatusId);
            if(string.IsNullOrEmpty(Name))
                query = query.Where(x=>x.Name.Contains(Name));
            if (ParentId.HasValue)
            {
                if(ParentId.Value != 0) query = query.Where(x => x.Parent != 0 && x.Parent == ParentId.Value);
                else query = query.Where(x => x.Parent == 0);
            }
            else  query = query.Where(x => x.Parent == 0);

            if (StatusId.HasValue)
            {
                query = query.Where(x => x.Status == StatusId.Value);
            }
            var unsortedTerritories = query.ToList();

            return new PagedList<Territories>(unsortedTerritories, PageIndex.Value, PageSize.Value);

        }

        public Territories GetById(int id)
        {
             if (id == 0)
                return null;

            return _territoriesRepository.ToCachedGetById(id);
        }
        #endregion
     
    }
}
