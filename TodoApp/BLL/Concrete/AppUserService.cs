using AutoMapper;
using BLL.Abstract;
using DAL.Abstract;
using DTO.AccountDtos;
using DTO.AssignmentDtos;
using DTO.PaginationDto;
using Entity.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using Validation.AppUser;

namespace BLL.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRoleRepository _roleRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AppUserService(IJwtService jwtService, IConfiguration configuration, IAppUserRoleRepository roleRepository, IAppUserRepository appUserRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _roleRepository = roleRepository;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> RegisterUser(AppUserPostDto postDto)
        {
            if (await _appUserRepository.IsExistAsync(x => x.UserName == postDto.UserName))
                return new BadRequestObjectResult(new { error = new { field = "UserName", message = "User already exists!" } });

            if (await _appUserRepository.IsExistAsync(x => x.Email == postDto.Email))
                return new BadRequestObjectResult(new { error = new { field = "Email", message = "Email already exists!" } });

            postDto.Password = HashPassword(postDto.Password);
            var appUser = _mapper.Map<AppUser>(postDto);

            await _appUserRepository.AddAsync(appUser);
            await _appUserRepository.CommitAsync();
            
            return new ObjectResult(appUser) { StatusCode = 201 };
        }
        public async Task<List<string>> GetRoleAsync(int roleId)
        {
            var role = await _roleRepository.GetAsync(r => r.Id == roleId);
            if (role != null)
            {
                return new List<string> { role.RoleName };
            }
            return new List<string>();
        }

       
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validationResult = ValidateLoginDto(loginDto);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }

            var user = await _appUserRepository.GetAsync(u => u.UserName == loginDto.UserName);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found.");
            }

            var hashedPassword = HashPassword(loginDto.Password);

            if (user.Password != hashedPassword)
            {
                return new BadRequestObjectResult("Invalid password.");
            }
            var roles = await GetRoleAsync(user.RoleId);
            user.AppUserRole.RoleName = roles.FirstOrDefault();
            var token = _jwtService.GenerateToken(user, _configuration);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user);


            return new OkObjectResult(new { token = token, newRefreshToken = newRefreshToken });
        }

        private ValidationResult ValidateLoginDto(LoginDto loginDto)
        {
            LoginValidator validator = new LoginValidator();
            return validator.Validate(loginDto);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));


            var stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        public async Task<AppUserRefreshTokenDto> RefreshToken(AppUser user, IConfiguration config)
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
            user = await _appUserRepository.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
            {
                return new AppUserRefreshTokenDto { Message = "Invalid Refresh Token" };
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return new AppUserRefreshTokenDto { Message = "Token expired." };
            }

            var roles = await GetRoleAsync(user.RoleId);
            user.AppUserRole.RoleName = roles.FirstOrDefault();

            string token = _jwtService.GenerateToken(user, config);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user);

            return new AppUserRefreshTokenDto
            {
                Token = token,
                RefreshToken = newRefreshToken.Token,
                Expires = newRefreshToken.Expires
            };
        }

        private RefreshTokenDto GenerateRefreshToken()
        {
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            randomNumberGenerator.GetBytes(randomBytes);

            var refreshToken = new RefreshTokenDto
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(1),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private async Task SetRefreshToken(RefreshTokenDto refreshToken, AppUser user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires.ToUniversalTime(),
            };
            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created.ToUniversalTime();
            user.TokenExpires = refreshToken.Expires.ToUniversalTime();
            await _appUserRepository.CommitAsync();
        }

        public async Task<IActionResult> Edit(AppUserPutDto putDto)
        {
            AppUser appUser = await _appUserRepository.GetAsync(x => x.Id == putDto.Id);

            if (appUser == null) return new BadRequestObjectResult(new { error = new { field = "Id", message = "User not found!" } });

            if (appUser.UserName != putDto.UserName && await _appUserRepository.IsExistAsync(x => x.Id != putDto.Id && x.UserName == putDto.UserName))
                return new BadRequestObjectResult(new { error = new { field = "UserName", message = "UserName already exist!" } });

            if (appUser.Email != putDto.Email && await _appUserRepository.IsExistAsync(x => x.Id != putDto.Id && x.Email == putDto.Email))
                return new BadRequestObjectResult(new { error = new { field = "Email", message = "Email already exist!" } });


            putDto.Password = HashPassword(putDto.Password);

            _mapper.Map(putDto, appUser);

            await _appUserRepository.UpdateAsync(appUser);
            await _appUserRepository.CommitAsync();
            return new StatusCodeResult(204);
        }

        public async Task<IActionResult> GetAll(int page)
        {
            var query = _appUserRepository.GetAll(x => true).Include(p => p.AssignmentUsers).ThenInclude(p => p.Assignment); 
            var appUserDtos = _mapper.Map<List<AppUserListItemDto>>(query.Skip((page - 1) * 4).Take(4));

            PaginationListDto<AppUserListItemDto> model =
            new PaginationListDto<AppUserListItemDto>(appUserDtos, page, 4, query.Count());
            return new OkObjectResult(model);
        }

       

        public async Task<IActionResult> Get(int id)
        {
            AppUser appUser = await _appUserRepository.GetAsync(x => x.Id == id, "AppUserRole", "AssignmentUsers.Assignment");
            if (appUser == null)
                return new NotFoundResult();

            AppUserGetDto appUserDto = _mapper.Map<AppUserGetDto>(appUser);
            return new OkObjectResult(appUserDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            AppUser appUser = await _appUserRepository.GetAsync(x => x.Id == id);
            if (appUser == null)
                return new NotFoundResult();

            _appUserRepository.Remove(appUser);
            _appUserRepository.Commit();
            return new NoContentResult();
        }



    }
}