using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Domain.Domain.Lester;
using Motel.Services.Lester;
using Motel.Services.Logging;
using Motel.Services.RentalPosting;

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LestersController : ControllerBase
    {
        #region Fields
        protected readonly ILesterServices _lesterServices;
        protected readonly IRentalPostService _rentalPostService;
        protected readonly ILogger _logger;

      
        #endregion

        #region Ctor
        public LestersController(
            ILesterServices lesterServices, 
            IRentalPostService rentalPostService, 
            ILogger logger
        )
        {
            _lesterServices = lesterServices;
            _rentalPostService = rentalPostService;
            _logger = logger;
        }
        #endregion

        #region Method
        public  Lesters GetById(int Id)
        {
            try
            {
                Lesters lesters = new Lesters();
                lesters = _lesterServices.GetByUserId(Id);
                return lesters;
            }
            catch (Exception ex)
            {
                _logger.Error("Error GetById",ex);
                return null;
            }
            
        }

        public IEnumerable<Lesters>  
        #endregion
    }
}
