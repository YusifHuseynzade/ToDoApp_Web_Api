using DAL.Context;
using Domain.Entities;
using Domain.IRepositories;

namespace DAL.Repositories
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
