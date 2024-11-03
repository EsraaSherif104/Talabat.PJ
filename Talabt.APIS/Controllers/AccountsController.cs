using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.Core.Entities.identity;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{
    
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        //register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            
            
            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
           var result=  await  _userManager.CreateAsync(User, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            var returneduser = new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = "this will be token"
            };
            return Ok(returneduser);
        
        }


        //login
        [HttpPost("Login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var User =await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

          var result= await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = "this will be token"
            });
        }






    }
}
