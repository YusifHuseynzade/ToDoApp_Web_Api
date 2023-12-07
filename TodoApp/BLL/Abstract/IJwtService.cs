using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace BLL.Abstract
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user, IConfiguration config);
    }
}
