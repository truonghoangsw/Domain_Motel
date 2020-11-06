using Motel.Core;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Common;
using Motel.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motel.Services.Common
{
    public partial class GenericAttributeService : IGenericAttributeService
    {
        #region Fields
        protected readonly IRepository<GenericAttribute> _genericAttributeRepository;
        protected readonly IEventPublisher _eventPublisher;
        #endregion

        #region Ctor
        public GenericAttributeService(IEventPublisher eventPublisher,
            IRepository<GenericAttribute> genericAttributeRepository)
        {
            _eventPublisher = eventPublisher;
            _genericAttributeRepository = genericAttributeRepository;
        }

        #endregion



        #region Method
        public void DeleteAttribute(GenericAttribute attribute)
        {
             if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            _genericAttributeRepository.Delete(attribute);
            
            //event notification
            _eventPublisher.EntityDeleted(attribute);
        }

        public void DeleteAttributes(IList<GenericAttribute> attributes)
        {
             if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            _genericAttributeRepository.Delete(attributes);
            
            //event notification
            foreach (var attribute in attributes)
            {
                _eventPublisher.EntityDeleted(attribute);
            }
        }

        public TPropType GetAttribute<TPropType>(BaseEntity entity, string key,  TPropType defaultValue = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var keyGroup = entity.GetType().Name;

            var props = GetAttributesForEntity(entity.Id, keyGroup);

            //little hack here (only for unit testing). we should write expect-return rules in unit tests for such cases
            if (props == null)
                return defaultValue;

            if (!props.Any())
                return defaultValue;

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return defaultValue;

            return CommonHelper.To<TPropType>(prop.Value);
        }

        public TPropType GetAttribute<TEntity, TPropType>(int entityId, string key, TPropType defaultValue = default) where TEntity : BaseEntity
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            entity.Id = entityId;

            return GetAttribute(entity, key, defaultValue);
        }

        public GenericAttribute GetAttributeById(int attributeId)
        {
            if (attributeId == 0)
                return null;

            return _genericAttributeRepository.GetById(attributeId);
        }

        public IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup)
        {
            throw new NotImplementedException();
        }

        public void InsertAttribute(GenericAttribute attribute)
        {
            throw new NotImplementedException();
        }

        public void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value, int storeId = 0)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var keyGroup = entity.GetType().Name;

            var props = GetAttributesForEntity(entity.Id, keyGroup)
                .ToList();
            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    //delete
                    DeleteAttribute(prop);
                }
                else
                {
                    //update
                    prop.Value = valueStr;
                    UpdateAttribute(prop);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(valueStr)) 
                    return;

                //insert
                prop = new GenericAttribute
                {
                    EntityId = entity.Id,
                    Key = key,
                    KeyGroup = keyGroup,
                    Value = valueStr,
                };

                InsertAttribute(prop);
            }
        }

        public void UpdateAttribute(GenericAttribute attribute)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
