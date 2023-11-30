using DTO.AccountDtos;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BLL.Abstract
{
    public interface IAppUserService
    {
        Task<IActionResult> RegisterUser(AppUserPostDto postDto);
        Task<IActionResult> Login(LoginDto loginDto);
        Task<IActionResult> Edit(AppUserPutDto putDto);
        Task<IActionResult> GetAll(int page);
        Task<IActionResult> Get(int id);
        Task<IActionResult> Delete(int id);
        Task<AppUserRefreshTokenDto> RefreshToken(AppUser user, IConfiguration config);
    }
}
