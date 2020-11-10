using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Core;
using Motel.Core.Infrastructure;
using Motel.Domain;
using Motel.Domain.Domain.Media;
using Motel.Services.Logging;
using Motel.Services.Media;
using Motel.Services.RentalPosting;
using Motel.Services.Sercurity;

namespace Motel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly UserSettings _customerSettings;
        private readonly IDownloadService _downloadService;
        private readonly IRentalPostService _postService;
        private readonly IWorkContext _workContext;
        private readonly IMotelFileProvider _fileProvider;
        private readonly ILogger _logger;

        public DownloadController(UserSettings customerSettings, IDownloadService downloadService, 
            IRentalPostService postService, IWorkContext workContext,IMotelFileProvider fileProvider, ILogger logger)
        {
            _customerSettings = customerSettings;
            _downloadService = downloadService;
            _postService = postService;
            _workContext = workContext;
            _fileProvider=  fileProvider;
            _logger = logger;
        }
        //do not validate request token (XSRF)
        [HttpPost]
        public virtual ActionResult<Download> Post()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return NotFound();
            }

            var fileBinary = _downloadService.GetDownloadBits(httpPostedFile);
            var qqFileNameParameter = "qqfilename";
            var fileName = httpPostedFile.FileName;
            if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                fileName = Request.Form[qqFileNameParameter].ToString();
            //remove path (passed in IE)
            fileName = _fileProvider.GetFileName(fileName);

            var contentType = httpPostedFile.ContentType;
             
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            var download = new Download
            {
                DownloadGuid = Guid.NewGuid(),
                UseDownloadUrl = false,
                DownloadUrl = string.Empty,
                DownloadBinary = fileBinary,
                ContentType = contentType,
                //we store filename without extension for downloads
                Filename = _fileProvider.GetFileNameWithoutExtension(fileName),
                Extension = fileExtension,
                IsNew = true
            };

            try
            {
                _downloadService.InsertDownload(download);

                //when returning JSON the mime-type must be set to text/plain
                //otherwise some browsers will pop-up a "Save As" dialog.
                return download;
            }
            catch (Exception exc)
            {
                _logger.Error(exc.Message, exc);

                return BadRequest();
            }
        }
        [HttpPost]
        public virtual IActionResult GetFileUpload(Guid downloadId)
        {
            var download = _downloadService.GetDownloadByGuid(downloadId);
            if (download == null)
                return Content("Download is not available any more.");

            //A warning (SCS0027 - Open Redirect) from the "Security Code Scan" analyzer may appear at this point. 
            //In this case, it is not relevant. Url may not be local.
            if (download.UseDownloadUrl)
                return new RedirectResult(download.DownloadUrl);

                //binary download
            if (download.DownloadBinary == null)
                return Content("Download data is not available any more.");

            //return result
            var fileName = !string.IsNullOrWhiteSpace(download.Filename) ? download.Filename : downloadId.ToString();
            var contentType = !string.IsNullOrWhiteSpace(download.ContentType) ? download.ContentType : MimeTypes.ApplicationOctetStream;
            return new FileContentResult(download.DownloadBinary, contentType) { FileDownloadName = fileName + download.Extension };
        }
    }
}
