using BLL.Dtos.AccountDtos;
using DTO.AccountDtos;
using FluentValidation;

namespace BLL.Validation.AppUser
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().MaximumLength(20).MinimumLength(6);
            RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(20).MinimumLength(6);
        }
    }
}