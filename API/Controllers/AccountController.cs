using API.DTO;
using API.Errors;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Data.Entity;
using AutoMapper;
using API.Extensions;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _tokenService=tokenService;
           _mapper=mapper;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {

            var user = await _userManager.FindByEmailClaimPrinciple(HttpContext.User);
            return new UserDTO
            {
                Email = user.Email!,
                DispalyName=user!.DisplayName,
                Token=_tokenService.CreateToken(user),
            };
        }
        [HttpGet("existEmail")]
        public async Task<ActionResult<bool>> GetExistUser([FromQuery]string email)
        {
            return await _userManager.FindByEmailAsync(email)!=null;
        }
        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var user =await _userManager.FindByEmailWithAddress(HttpContext.User);


            return _mapper.Map<Address,AddressDTO>(user.Address!);
        }
        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            var user = await _userManager.FindByEmailWithAddress(HttpContext.User);
            user.Address=_mapper.Map<AddressDTO, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded) return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
            return BadRequest("Problem updating User Address");
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            
            var user =await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return Unauthorized(new APIResponse(401));
            }
            var result = _signInManager.CheckPasswordSignInAsync(user,loginDTO.Password,false);
            if(!result.Result.Succeeded) return Unauthorized(new APIResponse(401));
            return new UserDTO
            {
                Email = loginDTO.Email,
                Token=_tokenService.CreateToken(user),
                DispalyName=user.DisplayName
            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (GetExistUser(registerDTO.Email).Result.Value)
            {
                return new BadRequestObjectResult(new APIValidationErrorResponse { Errors=new[] { "The Email is already token" } });
            }
            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName=new MailAddress(registerDTO.Email).User
            };
            var result= await _userManager.CreateAsync(user,registerDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new APIResponse(400, result.Errors.ToString()!));
            }
            return new UserDTO
            {
                Email = registerDTO.Email,
                Token=_tokenService.CreateToken(user),
                DispalyName=user.DisplayName
            };
        }

    }
}
