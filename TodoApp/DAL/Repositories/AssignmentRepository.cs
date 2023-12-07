using DAL.Context;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        private readonly AppDbContext _context;

        public AssignmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Assignment>> GetExpiredAssignmentsAsync()
        {
            var currentDate = DateTime.UtcNow.AddHours(4);
            return await _context.Assignments
                .Where(a => a.ExpirationDate < currentDate && a.Status.Name != "Failed")
                .ToListAsync();
        }
    }
}
