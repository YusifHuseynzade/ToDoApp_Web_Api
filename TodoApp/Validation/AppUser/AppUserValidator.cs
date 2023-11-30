using DTO.AccountDtos;
using FluentValidation;

namespace Validation.AppUser
{
    public class AppUserValidator : AbstractValidator<AppUserPostDto>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserName).NotNull().NotEmpty().MaximumLength(20).MinimumLength(6);
            RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(100).MinimumLength(6);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}