using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Domain.Domain.Territories;
using Motel.Services.Territories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoriesController : ControllerBase
    {
        private readonly ITerritoriesServices _territoriesServices;

        public TerritoriesController(ITerritoriesServices territoriesServices)
        {
            _territoriesServices = territoriesServices;
        }

        // GET: api/<TerritoriesController>
        [HttpGet("[action]")]
        public IEnumerable<Territories> GetAll()
        {
            return _territoriesServices.GetAll();
        }

        // GET api/<TerritoriesController>/5
        [HttpGet("[action]")]
        public IEnumerable<Territories> GetParnet(int idParnet)
        {
            return _territoriesServices.GetAllParent(idParnet);
        }

        // POST api/<TerritoriesController>
        [HttpGet("[action]")]
        public IEnumerable<Territories> GetProvincial()
        {
            return _territoriesServices.GetAllParent(0);
        }
    }
}
