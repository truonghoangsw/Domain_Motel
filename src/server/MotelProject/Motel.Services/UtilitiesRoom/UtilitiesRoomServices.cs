namespace Motel.Services.UtilitiesRoom
{
    using Motel.Core;
    using System;
    using Domain.Domain.UtilitiesRoom;
    using Motel.Domain.ContextDataBase;
    using System.Linq;
    using System.Collections.Generic;

    public class UtilitiesRoomServices : IUtilitiesRoomServices
    {
        private readonly IRepository<UtilitiesRoom> _utilitiesRoomRepository;

        public UtilitiesRoomServices(IRepository<UtilitiesRoom> utilitiesRoomRepository)
        {
            _utilitiesRoomRepository = utilitiesRoomRepository;
        }

        public IEnumerable<UtilitiesRoom> GetAll()
        {
            return _utilitiesRoomRepository.Table.ToList();
        }
    }
}
