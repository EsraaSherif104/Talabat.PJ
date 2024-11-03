using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.identity;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{
    
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
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





    }
}
