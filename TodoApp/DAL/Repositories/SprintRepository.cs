using DAL.Context;
using Entity.Entities;
using Entity.IRepositories;

namespace DAL.Repositories
{
    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        private readonly AppDbContext _context;

        public SprintRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
