using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Domain;
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

        public DownloadController(UserSettings customerSettings, IDownloadService downloadService, 
            IRentalPostService postService, IWorkContext workContext)
        {
            _customerSettings = customerSettings;
            _downloadService = downloadService;
            _postService = postService;
            _workContext = workContext;
        }

        public virtual IActionResult Sample(int postId)
        {
             var post = _postService.GetRentalPostById(postId);
             if (post == null)
                return InvokeHttp404();
        }
    }
}
