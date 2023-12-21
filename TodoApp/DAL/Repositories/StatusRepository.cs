using DAL.Context;
using Entity.Entities;
using Entity.IRepositories;

namespace DAL.Repositories
{
    public class StatusRepository : Repository<Status>, IStatusRepository
    {
        private readonly AppDbContext _context;

        public StatusRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
