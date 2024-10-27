using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabt.APIS.Controllers
{
    
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> productRepo)
        {
            this._productRepo = productRepo;
        }
        //get all product
        //baseurl/api/product ->get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products =await _productRepo.GetAllAsync();
            //OkObjectResult result=new OkObjectResult(products); 
            //return (result);
            return Ok(products);

        }
        //get product by id

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product =await _productRepo.GetByIdAsync(id);
            return Ok(product);
        }



    }
}
