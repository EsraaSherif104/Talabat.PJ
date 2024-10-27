using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{
    
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,IMapper mapper)
        {
            this._productRepo = productRepo;
            this._mapper = mapper;
        }
        //get all product
        //baseurl/api/product ->get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductWithBrandAndTypeSpecification();
            var products =await _productRepo.GetAllWithSpecAsync(spec);
            var MappedProduct=_mapper.Map<IEnumerable<Product>, IEnumerable< ProductToReturnDTO>>(products);   
            
            //OkObjectResult result=new OkObjectResult(products); 
            //return (result);
            return Ok(MappedProduct);

        }
        //get product by id

        [HttpGet("{id}")]
       [ProducesResponseType(typeof(ProductToReturnDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductbYID(int id)
        {
            var spec=new ProductWithBrandAndTypeSpecification(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (product is null) return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product, ProductToReturnDTO>(product);
            return Ok(MappedProduct);
        }



    }
}
