using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Api.Framework;
using Motel.Api.Framework.Middleware;
using Motel.Api.Framework.Request;
using Motel.Core;
using Motel.Domain.Domain.Lester;
using Motel.Domain.Domain.Post;
using Motel.Services.Lester;
using Motel.Services.Logging;
using Motel.Services.RentalPosting;

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationCustomFilter]
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
        [HttpGet("[action]")]
        public  Lesters GetUser()
        {
            try
            {
                Lesters lesters = new Lesters();

                if(AccessControl.User != null)
                {
                     lesters = _lesterServices.GetByUserId(AccessControl.User.Id);
                }
                return lesters;
            }
            catch (Exception ex)
            {
                _logger.Error("Error GetById",ex);
                return null;
            }
            
        }

        [HttpGet("[action]")]
        public IActionResult GetPostOfUser([FromQuery]RequesPost  filter)
        {
            try
            {
                if(AccessControl.User != null)
                {
                    var user = AccessControl.User;
                    if(filter == null)
                    {
                        _logger.Warning("Get RentalPost filternull");
                        return BadRequest(new PagedList<RentalPost>());
                    }
                    var lester = _lesterServices.GetByUserId(user.Id);
                    var lst= _rentalPostService.GetList(filter.TitlePost,filter.ToMonthlyPrice,
                        filter.FromMonthlyPrice,filter.NumberRoom,filter.Address,
                        filter.PageIndex,filter.PageSize,filter.CatalogIds,filter.UtilitieIds,lester.Id).ToList();
                    lst.ForEach(x =>
                    {
                        x.UtilitiesRooms = _rentalPostService.GetUtilitiesOfPost(x.Id);
                        x.PostPictures = _rentalPostService.GetImageOfPost(x.Id);
                    });
                    return  Ok(new PagedList<RentalPost>(lst,filter.PageIndex.Value,filter.PageSize.Value));
                }
                else
                {
                    _logger.Warning("");
                    return  Unauthorized(); 
                }
            }
            catch (Exception ex) 
            {
                _logger.Error("Get RentalPost error",ex);
                return BadRequest(new PagedList<RentalPost>());
            }
        }
        #endregion
    }
}
