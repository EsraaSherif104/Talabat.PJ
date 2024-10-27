using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : APIBaseController
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        [HttpGet("NotFound")]
        //BUGGY/NOTFOUND
       public ActionResult GetNotFoundRequest()
        {
            var product = _dbcontext.Products.Find(100);
            if (product == null)return NotFound(new ApiResponse(404));
            return Ok(product);
        }
        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = _dbcontext.Products.Find(100);
            var producttoreturn = product.ToString();//error
          //throw null reference exceoption
            return Ok(producttoreturn);

        }
        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

    }
}
