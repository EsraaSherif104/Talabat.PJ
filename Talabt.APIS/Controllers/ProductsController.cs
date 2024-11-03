using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;
using Talabt.APIS.helpers;

namespace Talabt.APIS.Controllers
{
    
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product> productRepo,
            IMapper mapper
            ,IGenericRepository<ProductType> TypeRepo,
            IGenericRepository<ProductBrand> BrandRepo)
                 
        {
            this._productRepo = productRepo;
            this._mapper = mapper;
            _typeRepo = TypeRepo;
           _brandRepo = BrandRepo;
        }
        //get all product
        //baseurl/api/product ->get
        [Authorize]  
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecParam param )
        {
            var spec = new ProductWithBrandAndTypeSpecification(param);
            var products =await _productRepo.GetAllWithSpecAsync(spec);

            var MappedProduct=_mapper.Map<IReadOnlyList<Product>, IReadOnlyList< ProductToReturnDTO>>(products);
            var CountSpec = new ProductWithFiltrationForCountAsync(param);
            var count=await _productRepo.GetCountWithSpecAsync(CountSpec);
            
            #region comment
            //var ReturedObject = new Pagination<ProductToReturnDTO>()
            //{
            //    PageIndex=param.PageIndex,
            //    PageSize=param.PageSize,
            //    Data=MappedProduct


            //};




            //OkObjectResult result=new OkObjectResult(products); 
            //return (result); 
            #endregion
            return Ok(new Pagination<ProductToReturnDTO>(param.PageIndex,param.PageSize,MappedProduct, count));

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

        //getall types
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types =await _typeRepo.GetAllAsync();
            return Ok(Types);
        }

        //get all brands
        [HttpGet("Brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var brand =await _brandRepo.GetAllAsync();
            return Ok(brand);

        }
    }
}
