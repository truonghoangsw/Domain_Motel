using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Motel.Core;
using Motel.Core.Infrastructure;
using Motel.Domain;
using Motel.Domain.Domain.Media;
using Motel.Services.Logging;
using Motel.Services.Media;
using Motel.Services.RentalPosting;
using Motel.Services.Sercurity;


namespace Motel.Api.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDownloadService _downloadService;
        private readonly IWorkContext _workContext;
        private readonly IMotelFileProvider _fileProvider;
        private readonly Motel.Services.Logging.ILogger _loggers;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
         public WeatherForecastController(IDownloadService downloadService, 
            IWorkContext workContext,IMotelFileProvider fileProvider, Motel.Services.Logging.ILogger logger)
        {
            _downloadService = downloadService;
            _workContext = workContext;
            _fileProvider=  fileProvider;
            _loggers = logger;
        }
        private readonly ILogger<WeatherForecastController> _logger;

        

        [HttpGet]
        public IEnumerable<Download> Get()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return null;
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
                return null;
            }
            catch (Exception exc)
            {
                _loggers.Error(exc.Message, exc);
                return null;
            }
        }
    }
}
