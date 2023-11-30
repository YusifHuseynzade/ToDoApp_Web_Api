using DAL.Abstract;
using DAL.Context;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
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
