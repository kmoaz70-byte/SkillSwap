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
    public class SwapRequestRepository: GenericRepository<SwapRequest>, ISwapRequestRepository
    {
        private readonly ApplicationDbContext db;
        public SwapRequestRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
        public void Update(SwapRequest obj)
        {
            db.SwapRequests.Update(obj);
        }

       

    }
}

