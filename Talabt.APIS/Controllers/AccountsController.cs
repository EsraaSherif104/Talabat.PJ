using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Talabat.Core.Entities.identity;
using Talabat.Core.Services;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;
using Talabt.APIS.Extention;

namespace Talabt.APIS.Controllers
{

    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
           ITokenServices tokenServices
            , IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenServices = tokenServices;
            this._mapper = mapper;
        }

        //register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            if (CheckedEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "email is already in use"));
            }

            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(User, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            var returneduser = new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenServices.CreateTokenAsync(User, _userManager)
            };
            return Ok(returneduser);

        }


        //login
        [HttpPost("Login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenServices.CreateTokenAsync(User, _userManager)
            });
        }

        [Authorize]

        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var returnedobject = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            };
            return Ok(returnedobject);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            //  var email = User.FindFirstValue(ClaimTypes.Email);
            //   var user = await _userManager.FindByEmailAsync(email);

            var user = await _userManager.finduserwithAddressAsync(User);
            var MappedAddress = _mapper.Map<Addres, AddressDTO>(user.addres);
            return Ok(MappedAddress);
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>>UpdateAddree(AddressDTO updatedAddress)
        {
           // var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.finduserwithAddressAsync(User);
            var MappedAddress = _mapper.Map<AddressDTO, Addres>(updatedAddress);
            MappedAddress.Id = user.addres.Id;
            user.addres = MappedAddress;
         var result=  await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(updatedAddress);
        }
        [HttpGet("emailExists")]
        //baseurl/api/account//emailexisys
        public async Task<ActionResult<bool>>CheckedEmailExists(string Email)
        {
          //  var user = await _userManager.FindByEmailAsync(Email);
          //  if (user is null) return false;
           // else return true;
            return await _userManager.FindByEmailAsync(Email) is not null;
        }

    }


}
