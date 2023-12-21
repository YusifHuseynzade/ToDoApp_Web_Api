using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.IRepositories
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<List<Assignment>> GetExpiredAssignmentsAsync();
    }
}
