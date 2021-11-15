using Microsoft.EntityFrameworkCore;
using Snblog.Enties.Models;
using Snblog.IService;
using Snblog.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnTalkTypeService : ISnTalkTypeService
    {
        private readonly snblogContext _coreDbContext;//DB

        public SnTalkTypeService(snblogContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<bool> AddAsync(SnTalkType Entity)
        {
            await _coreDbContext.SnTalkTypes.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnTalkTypes.CountAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _coreDbContext.SnTalkTypes.FindAsync(id);
            _coreDbContext.SnTalkTypes.Remove(todoItem);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnTalkType>> GetAllAsync()
        {
            return await _coreDbContext.SnTalkTypes.ToListAsync();
        }

        public async Task<List<SnTalkType>> GetAllAsync(int id)
        {
            return await _coreDbContext.SnTalkTypes.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<List<SnTalkType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _coreDbContext.SnTalkTypes.Where(s => true)
              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            }
            else
            {
                return await _coreDbContext.SnTalkTypes.Where(s => true)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
            }
        }

        public Task<List<SnTalkType>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(SnTalkType Entity)
        {
            _coreDbContext.SnTalkTypes.Update(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }
    }
}
