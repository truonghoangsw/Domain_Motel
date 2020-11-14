using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Domain.Domain.Media;
using Motel.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class PictureController : ControllerBase
    {
        #region Fields

        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        #endregion

        #region Methods

        [HttpPost("[action]")]
        [IgnoreAntiforgeryToken]
        public virtual IActionResult AsyncUpload()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.UploadPictures))
            //    return Json(new { success = false, error = "You do not have required permissions" }, "text/plain");

            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return BadRequest();
            }

            const string qqFileNameParameter = "qqfilename";

            var qqFileName = Request.Form.ContainsKey(qqFileNameParameter)
                ? Request.Form[qqFileNameParameter].ToString()
                : string.Empty;

            var picture = _pictureService.InsertPicture(httpPostedFile, qqFileName);

            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Ok(new
            {
                success = true,
                pictureId = picture.Id,
                imageUrl = _pictureService.GetPictureUrl(ref picture, 100)
            });
        }
         [HttpPost("[action]")]
        public virtual IActionResult AsyncMutileUpload(ICollection<IFormFile> files)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.UploadPictures))
            //    return Json(new { success = false, error = "You do not have required permissions" }, "text/plain");

            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return BadRequest();
            }
            List<Picture> pictures = new List<Picture>();
            foreach (var item in files)
            {
                const string qqFileNameParameter = "qqfilename";
                var qqFileName = Request.Form.ContainsKey(qqFileNameParameter)
                    ? Request.Form[qqFileNameParameter].ToString()
                    : string.Empty;

                var picture = _pictureService.InsertPicture(item, qqFileName);
                pictures.Add(picture);
                //when returning JSON the mime-type must be set to text/plain
                //otherwise some browsers will pop-up a "Save As" dialog.
            }
            
            return Ok(pictures);
        }
        #endregion

       
    }
}
