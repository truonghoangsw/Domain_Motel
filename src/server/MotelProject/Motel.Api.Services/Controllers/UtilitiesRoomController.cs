using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Domain.Domain.UtilitiesRoom;
using Motel.Services.UtilitiesRoom;

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilitiesRoomController : ControllerBase
    {
        private readonly IUtilitiesRoomServices _utilitiesRoomServices;
        public UtilitiesRoomController(IUtilitiesRoomServices utilitiesRoomServices)
        {
            _utilitiesRoomServices = utilitiesRoomServices;
        }
        [HttpGet("[action]")]
        public IEnumerable<UtilitiesRoom> GetAll()
        {
            return _utilitiesRoomServices.GetAll();
        }
    }
}
