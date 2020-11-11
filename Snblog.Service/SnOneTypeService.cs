using Microsoft.EntityFrameworkCore;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnOneTypeService : ISnOneTypeService
    {
        private readonly snblogContext _coreDbContext;//DB
        public SnOneTypeService(snblogContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<bool> AddAsync(SnOneType Entity)
        {
           await _coreDbContext.SnOneType.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync()>0;
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnOneType.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
           //_coreDbContext.SnOneType.Remove(Entity);
           // return await _coreDbContext.SaveChangesAsync()>0;
             var todoItem = await _coreDbContext.SnOneType.FindAsync(id);
              _coreDbContext.SnOneType.Remove(todoItem);
            return  await _coreDbContext.SaveChangesAsync()>0;
        }

        public async Task<List<SnOneType>> GetAll()
        {
            return await _coreDbContext.SnOneType.ToListAsync();
        }

        public async Task<SnOneType> GetFirst(int id)
        {
            return await _coreDbContext.SnOneType.Where(s => s.Id == id).FirstAsync();
        }

        public async Task<SnOneType> GetTypeFirst(int type)
        {
          return await _coreDbContext.SnOneType.Where(s=>s.SoTypeId == type).FirstAsync();
        }

        public Task<bool> UpdateAsync(SnOneType Entity)
        {
            throw new NotImplementedException();
        }
    }
}
