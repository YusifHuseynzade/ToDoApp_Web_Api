using DAL.Context;
using Domain.Entities;
using Domain.IRepositories;

namespace DAL.Repositories
{
    public class AssignmentUserRepository : Repository<AssignmentUser>, IAssignmentUserRepository
    {
        private readonly AppDbContext _context;

        public AssignmentUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
