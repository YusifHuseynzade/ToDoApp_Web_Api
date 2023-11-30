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
    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        private readonly AppDbContext _context;

        public SprintRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
