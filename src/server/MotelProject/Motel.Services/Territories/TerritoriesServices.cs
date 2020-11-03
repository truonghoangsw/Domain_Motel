
namespace Motel.Services.Territories
{
    using Motel.Core;
    using Motel.Core.Caching;
    using Motel.Domain;
    using Motel.Domain.ContextDataBase;
    using  Motel.Domain.Domain.Territories;
    using Motel.Services.Caching;
    using Motel.Services.Events;
    using Motel.Services.Logging;
    using System;
    using System.Collections.Generic;

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
        public TerritoriesServices(IRepository<Territories> territoriesRepository, IStaticCacheManager staticCacheManager, IEventPublisher eventPublisher)
        {
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
            return _staticCacheManager.Get(key,) ;
        }

        public IPagedList<Territories> GetAllFilter(string Name, int StatusId, string PackageName, int Ten, bool? OrderIndex, int PageIndex=0, int PageSize = int.MaxValue, out int totalItem)
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<Territories> GetAllParent(string Name, int StatusId, string ParentId, bool OrderIndex, bool LevelObject, int? PageIndex, int? PageSize, out int totalItem)
        {
            throw new System.NotImplementedException();
        }

        public Territories GetById(int id)
        {
            throw new System.NotImplementedException();
        }
        #endregion
     
    }
}
