using BLL.Abstract;
using DTO.AccountDtos;
using Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IConfiguration _configuration;

        public AccountsController(IAppUserService appUserService, IConfiguration configuration)
        {
            _appUserService = appUserService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AppUserPostDto postDto)
        {
            return await _appUserService.RegisterUser(postDto);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return await _appUserService.Login(loginDto);
        }

        [Authorize(Roles = "Project Manager")]
        [HttpPut]
        public async Task<IActionResult> Edit(AppUserPutDto putDto)
        {
            return await _appUserService.Edit(putDto);
        }

        [Authorize(Roles = "Project Manager")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            return await _appUserService.GetAll(page);
        }
        [Authorize(Roles = "Project Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _appUserService.Get(id);

        }

        [Authorize(Roles = "Project Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _appUserService.Delete(id);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AppUserRefreshTokenDto>> RefreshToken()
        {
            var user = new AppUser();
            if (user == null)
            {
                return BadRequest("Invalid user or configuration not found.");
            }
            var result = await _appUserService.RefreshToken(user, _configuration);
            return result;
        }
    }
}