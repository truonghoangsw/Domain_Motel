using Motel.Core;
using Motel.Domain;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Logging;
using Motel.Services.Caching;
using Motel.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motel.Services.Logging
{
    public class UserActionService:IUserActionService
    {
        private readonly ILogger _log; 
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<ActivityLog> _activityLogRepository;
        private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
         public UserActionService(ICacheKeyService cacheKeyService,
            IEventPublisher eventPublisher,
            IRepository<ActivityLog> activityLogRepository,
            IRepository<ActivityLogType> activityLogTypeRepository,
            IWebHelper webHelper,
            IWorkContext workContext,ILogger log)
        {
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _activityLogRepository = activityLogRepository;
            _activityLogTypeRepository = activityLogTypeRepository;
            _webHelper = webHelper;
            _workContext = workContext;
            _log = log;
        }

        public void ClearAllActivities()
        {
            _activityLogRepository.Truncate();
        }

        public void DeleteActivity(ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));

            _activityLogRepository.Delete(activityLog);

            //event notification
            _eventPublisher.EntityDeleted(activityLog);
        }

        public void DeleteActivityType(ActivityLogType activityLogType)
        {
            if(activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));

            _activityLogTypeRepository.Delete(activityLogType);

            //event notification
            _eventPublisher.EntityDeleted(activityLogType);
        }

        public ActivityLog GetActivityById(int activityLogId)
        {
            if(activityLogId == 0)
                return null;
            return _activityLogRepository.GetById(activityLogId);
        }

        public ActivityLogType GetActivityTypeById(int activityLogTypeId)
        {
            if(activityLogTypeId == 0)
                return null;
            return _activityLogTypeRepository.GetById(activityLogTypeId);
        }

        public IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom = null, DateTime? createdOnTo = null
            , int? customerId = null, int? activityLogTypeId = null, string ipAddress = null, string entityName = null
            , int? entityId = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                 var query = _activityLogRepository.Table;

                if(!string.IsNullOrEmpty(ipAddress))
                    query  =query.Where(logitem => logitem.IpAddress.Contains(ipAddress));
                if(!string.IsNullOrEmpty(entityName))
                    query  =query.Where(logitem => logitem.EntityName.Contains(entityName));
                if(createdOnFrom.HasValue)
                    query = query.Where(logitem=>logitem.CreatedOnUtc >= createdOnFrom);
                if(createdOnTo.HasValue)
                    query = query.Where(logitem => logitem.CreatedOnUtc <= createdOnTo);
                if(activityLogTypeId.HasValue && activityLogTypeId.Value > 0)
                    query = query.Where(logitem => activityLogTypeId.Value == logitem.ActivityLogTypeId);
                    if(entityId.HasValue && entityId.Value > 0)
                    query = query.Where(logitem => entityId.Value == logitem.EntityId);
                query = query.OrderByDescending(logItem => logItem.CreatedOnUtc).ThenBy(logItem => logItem.Id);
                return new PagedList<ActivityLog>(query, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _log.Error("GetAllActivities is error is Expection: {0}",ex);
                return new PagedList<ActivityLog>();
            }
           
        }

        public IList<ActivityLogType> GetAllActivityTypes()
        {
            try
            {
                var query = _activityLogRepository.Table;
                return new PagedList<ActivityLogType>();
            }
            catch (Exception ex)
            {
                _log.Error("GetAllActivityTypes is error is Expection: {0}",ex);
                return new PagedList<ActivityLogType>();
            }
        }

        public ActivityLog InsertActivity(string systemKeyword, string comment, BaseEntity entity = null)
        {
             return InsertActivity(_workContext.CurrentUser, systemKeyword, comment, entity);
        }

        public ActivityLog InsertActivity(Auth_User customer, string systemKeyword, string comment, BaseEntity entity = null)
        {
           if (customer == null)
                return null;

            //try to get activity log type by passed system keyword
            var activityLogType = GetAllActivityTypes().FirstOrDefault(type => type.SystemKeyword.Equals(systemKeyword));
            if (!activityLogType?.Enabled ?? true)
                return null;

            //insert log item
            var logItem = new ActivityLog
            {
                ActivityLogTypeId = activityLogType.Id,
                EntityId = entity?.Id,
                EntityName = entity?.GetType().Name,
                CustomerId = customer.Id,
                Comment = CommonHelper.EnsureMaximumLength(comment ?? string.Empty, 4000),
                CreatedOnUtc = DateTime.UtcNow,
                IpAddress = _webHelper.GetCurrentIpAddress()
            };
            _activityLogRepository.Insert(logItem);

            //event notification
            _eventPublisher.EntityInserted(logItem);

            return logItem;
        }

        public void InsertActivityType(ActivityLogType activityLogType)
        {
            if(activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));
            
            _activityLogTypeRepository.Insert(activityLogType);
            _eventPublisher.EntityInserted(activityLogType);

        }

        public void UpdateActivityType(ActivityLogType activityLogType)
        {
            if(activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));
            _activityLogTypeRepository.Update(activityLogType);

            //event notification
            _eventPublisher.EntityUpdated(activityLogType);
        }
    }
}
