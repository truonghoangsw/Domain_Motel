using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Api.Framework;
using Motel.Api.Framework.Middleware;
using Motel.Api.Framework.Request;
using Motel.Api.Framework.Response;
using Motel.Core;
using Motel.Core.Enum;
using Motel.Domain.Domain.Post;
using Motel.Services.Lester;
using Motel.Services.Logging;
using Motel.Services.Media;
using Motel.Services.RentalPosting;
using Motel.Services.Territories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationCustomFilter]
    public class PostRentalController : ControllerBase
    {
        #region Fields
        private IRentalPostService _rentalPostService { get; set;}
        private ILesterServices _lesterServices { get; set;}
        private ITerritoriesServices _territoriesServices { get; set;}
         private IPictureService _pictruetService { get; set;}
        private ILogger _logger { get; set;}
        #endregion

        #region Ctor
        public PostRentalController(
            IRentalPostService rentalPostService, 
            ITerritoriesServices territoriesServices, 
            IPictureService pictruetService,
            ILesterServices lesterServices,
            ILogger logger)
        {
            _lesterServices = lesterServices;
            _rentalPostService = rentalPostService;
            _territoriesServices = territoriesServices;
            _pictruetService = pictruetService;
            _logger = logger;
        }
        #endregion

        #region Method
        // GET: api/<PostRentalController>
        [HttpGet]
        public IPagedList<RentalPost> Get([FromQuery]RequesPost  filter)
        {
            try
            {
                if(filter == null)
                {
                    _logger.Warning("Get RentalPost filternull");
                    return new PagedList<RentalPost>();
                }
                var lst= _rentalPostService.GetList(filter.TitlePost,filter.ToMonthlyPrice,
                    filter.FromMonthlyPrice,filter.NumberRoom,filter.Address,
                    filter.PageIndex,filter.PageSize).ToList();
                lst.ForEach(x =>
                {
                    x.UtilitiesRooms = _rentalPostService.GetUtilitiesOfPost(x.Id);
                    x.PostPictures = _rentalPostService.GetImageOfPost(x.Id);
                });
                return  new PagedList<RentalPost>(lst,filter.PageIndex.Value,filter.PageSize.Value);
            }
            catch (Exception ex) 
            {
                _logger.Error("Get RentalPost error",ex);
                return new PagedList<RentalPost>();
            }
        }

        // GET api/<PostRentalController>/5
        [HttpGet("{id}")]
        public ResponseDetailPage Get(int id)
        {
            ResponseDetailPage response = new ResponseDetailPage();
            var entity = _rentalPostService.GetById(id);
            response.pictures = _pictruetService.GetPicturesByProductId(entity.Id).ToList();
            if(entity.ProvincialId != 0)
            {
                response.ProvincialName=_territoriesServices.GetById(entity.ProvincialId)?.Name;
            }
            if(entity.DistrictId != 0)
            {
                response.DistrictName=_territoriesServices.GetById(entity.DistrictId)?.Name;
            }
            if(entity.WardId != 0)
            {
                response.WardName=_territoriesServices.GetById(entity.WardId)?.Name;
            }
            response.Post = entity;
            response.utilitiesRooms = _rentalPostService.GetUtilitiesOfPost(entity.Id).ToList();
            return response;
        }

        // POST api/<PostRentalController>
        [HttpPost]
        public IActionResult Post([FromBody]PostModel model)
        {
            ResponsePostRental response = new ResponsePostRental();
            try
            {
                RentalPost entity = new RentalPost();
                entity  = model.ConvertSetp(entity);
                var provincial = _territoriesServices.GetById(entity.ProvincialId);
                var Ward = _territoriesServices.GetById(entity.WardId);
                var District = _territoriesServices.GetById(entity.DistrictId);
                string addressDeatail = string.Format("{0} , {1} , {2} , {3}",entity.AddressDetail,provincial == null ? "": provincial.Name,District == null ? "": District.Name,Ward == null ? "": Ward.Name);
                entity.AddressDetail = addressDeatail;
                var lester =   _lesterServices.GetByUserId(AccessControl.User.Id);
                if(lester == null)
                {
                   response = ResponseMessage(StatusPost.Error);
                   return BadRequest(response);
                }
                entity.CreateBy = lester.Id;
                entity.LesterId = lester.Id;
                _rentalPostService.InsertPost(entity);
              
                if(model.PictureIds != null)
                {
                    _rentalPostService.InsertPicturesForPost(model.PictureIds,entity.Id);
                    
                }
                if(model.UtilitiesIds != null)
                {
                    _rentalPostService.InsertUtilitiesForPost(model.UtilitiesIds,entity.Id);
                }
                response = ResponseMessage(StatusPost.Susscess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseMessage(StatusPost.Error);
               _logger.Error("Post PostRentalController", ex);
                return BadRequest(response);
            }
            
        }

        // PUT api/<PostRentalController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PostModel model)
        {
            ResponsePostRental response = new ResponsePostRental();
            StatusPost statusPost = (StatusPost)1;
            if(model == null && model?.Status == null)
            {
                response= ResponseMessage(statusPost);
                return BadRequest(response);
            }
            try
            {
                RentalPost updatePost = new RentalPost();
                RentalPost rentalPost = _rentalPostService.GetById(id);
                switch (statusPost)
                {
                    case StatusPost.Pending:
                        break;
                    case StatusPost.Approved:
                        break;
                    case StatusPost.Cancel:
                        break;
                    case StatusPost.Block:
                        break;
                    case StatusPost.Setp2:
                        updatePost = model.ConvertSetp(rentalPost);
                        _rentalPostService.UppdatePost(updatePost);
                        break;
                    case StatusPost.Setp3:
                        _rentalPostService.DeletePictureForPost(rentalPost.Id);
                        _rentalPostService.DeleteUtilitiesOfPost(rentalPost.Id);
                        _rentalPostService.InsertPicturesForPost(model.PictureIds,rentalPost.Id);
                        _rentalPostService.InsertUtilitiesForPost(model.UtilitiesIds,rentalPost.Id);
                        break;
                    case StatusPost.Setp4:
                         updatePost = model.ConvertSetp(rentalPost);
                        _rentalPostService.UppdatePost(updatePost);
                        break;
                    case StatusPost.Delete:
                        break;
                    default:
                        break;
                }
                response= ResponseMessage(StatusPost.Susscess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseMessage(StatusPost.Susscess);
               _logger.Error("Post PostRentalController", ex);
                return BadRequest(response);
            }

           
        }

        // DELETE api/<PostRentalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

        #region Utilities
        private  ResponsePostRental ResponseMessage(StatusPost codeMessage)
        {
            ResponsePostRental response = new ResponsePostRental();
            switch (codeMessage)
            {
                case StatusPost.Pending:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Approved:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Cancel:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Block:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Setp1:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Setp2:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Setp3:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Setp4:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Delete:
                    response.MessageCode = (int)StatusPost.Pending;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Pending);
                    break;
                case StatusPost.Error:
                    response.MessageCode = (int)StatusPost.Error;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Error);
                    break;
                case StatusPost.Susscess:
                    response.MessageCode = (int)StatusPost.Susscess;
                    response.Message = CommonHelper.DescriptionEnum(StatusPost.Susscess);
                    break;
                default:
                    response.MessageCode = 0;
                    response.Message = "Không tìm được, dữ liệu phù hợp";
                    break;
            }
            return response;
        }
        #endregion
    }
}
