using DAL.Context;
using Domain.Entities;
using Domain.IRepositories;

namespace DAL.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
