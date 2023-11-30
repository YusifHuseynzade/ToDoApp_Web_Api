using DAL.Concrete;
using DAL.Context;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
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
