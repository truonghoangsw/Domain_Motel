using Motel.Core.Caching;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Lester;
using Motel.Domain.Domain.Post;
using Motel.Services.Logging;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motel.Services.Lester
{
    public class LesterServices: ILesterServices
    {
        #region Fields
        private readonly CachingSettings _cachingSettings;
        private readonly LesterSettings _customerSettings;
        private readonly ILogger _logger;
        private readonly IRepository<Lesters> _lestersRepository;
        private readonly IRepository<RentalPost> _rentalPostRepository;
        #endregion

        #region Ctor
        public LesterServices(
            ILogger logger,
            CachingSettings cachingSettings, 
            LesterSettings customerSettings, 
            IRepository<Lesters> lestersRepository, IRepository<RentalPost> rentalPostRepository)
        {
            _logger = logger;
            _cachingSettings = cachingSettings;
            _customerSettings = customerSettings;
            _lestersRepository = lestersRepository;
            _rentalPostRepository = rentalPostRepository;
        }
        #endregion


        #region Utilities

        #endregion


        #region Method
        public Lesters GetByUserId(int userId)
        {
            try
            {
                return _lestersRepository.Table.FirstOrDefault(x=>x.UserId ==userId);
            }
            catch (Exception ex)
            {
                _logger.Error("GetByUserId userId = {0},error:{1}",ex);
                return null; 
            }
        }

        public IEnumerable<RentalPost> GetPostOfLester(int LesterId,int? PageIndex=0,int? PageSize= int.MaxValue)
        {
            var query = _rentalPostRepository.Table.Where(x=>x.LesterId == LesterId);
            return query;
        }
        #endregion
    }
}
