using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Api.Framework.Response;
using Motel.Core;
using Motel.Core.Enum;
using Motel.Domain.Domain.Post;
using Motel.Services.Logging;
using Motel.Services.RentalPosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostRentalController : ControllerBase
    {
        #region Fields
        private IRentalPostService _rentalPostService { get; set;}
        private ILogger _logger { get; set;}
        #endregion

        #region Ctor
        public PostRentalController(
            IRentalPostService rentalPostService,
            ILogger logger
        ){
            _rentalPostService = rentalPostService;
            _logger = logger;
        }
        #endregion

        #region Method
        // GET: api/<PostRentalController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PostRentalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostRentalController>
        [HttpPost]
        public IActionResult Post([FromBody] PostModel model)
        {
            ResponsePostRental response = new ResponsePostRental();
            try
            {
                RentalPost rentalPost = new RentalPost();
                StatusPost statusPost = (StatusPost)model.Status;
                switch (statusPost)
                {
                    case StatusPost.Setp1:
                        rentalPost = model.ConvertSetp(rentalPost);
                        _rentalPostService.InsertPost(rentalPost);
                        break;
                    default:
                        rentalPost = null;
                        break;
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
            StatusPost statusPost = (StatusPost)model.Status;
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
