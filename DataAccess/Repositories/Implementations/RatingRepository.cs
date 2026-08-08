using DataAccess.Data;
using DataAccess.Repositories.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private ApplicationDbContext _db;

        public RatingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
