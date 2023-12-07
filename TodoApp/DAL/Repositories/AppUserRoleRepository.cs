using DAL.Context;
using Domain.Entities;
using Domain.IRepositories;

namespace DAL.Repositories
{
    public class AppUserRoleRepository : Repository<AppUserRole>, IAppUserRoleRepository
    {
        private readonly AppDbContext _context;

        public AppUserRoleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
