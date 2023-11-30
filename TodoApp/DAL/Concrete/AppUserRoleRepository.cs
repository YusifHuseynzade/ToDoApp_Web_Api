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
    public class AppUserRoleRepository : Repository<AppUserRole>, IAppUserRoleRepository
    {
        private readonly AppDbContext _context;

        public AppUserRoleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
