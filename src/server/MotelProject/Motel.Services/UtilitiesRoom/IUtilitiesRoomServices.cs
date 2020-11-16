
namespace Motel.Services.UtilitiesRoom
{
    using  Motel.Domain.Domain.UtilitiesRoom;
    using System.Collections.Generic;

    public interface IUtilitiesRoomServices
    {
         IEnumerable<UtilitiesRoom> GetAll();
    }
}
