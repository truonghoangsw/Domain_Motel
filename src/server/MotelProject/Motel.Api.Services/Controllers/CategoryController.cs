using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Api.Framework.Request;
using Motel.Core;
using Motel.Domain.Domain.Post;
using Motel.Services.RentalPosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryServices;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryServices = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet("[action]")]
        public IEnumerable<Category> GetAll()
        {
            return _categoryServices.GetAllCategories();
        }

        // GET api/<CategoryController>/5
        [HttpGet("[action]")]
        public  IPagedList<Category> Get([FromQuery]RequestCategory  filter)
        {
            return _categoryServices.GetAllCategories(filter.Name,filter.PageIndex ,filter.PageSize);
        }

        // POST api/<CategoryController>
        [HttpGet("[action]")]
        public Category GetById(int Id)
        {
            return _categoryServices.GetCategoryById(Id);
        }
    }
}
