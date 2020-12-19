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
    public class SnTalkTypeService : ISnTalkTypeService
    {
        private readonly snblogContext _coreDbContext;//DB

        public SnTalkTypeService(snblogContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<bool> AddAsync(SnTalkType Entity)
        {
            await _coreDbContext.SnTalkType.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnTalkType.CountAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _coreDbContext.SnTalkType.FindAsync(id);
            _coreDbContext.SnTalkType.Remove(todoItem);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnTalkType>> GetAllAsync()
        {
            return await _coreDbContext.SnTalkType.ToListAsync();
        }

        public async Task<List<SnTalkType>> GetAllAsync(int id)
        {
            return await _coreDbContext.SnTalkType.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<List<SnTalkType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _coreDbContext.SnTalkType.Where(s => true)
              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            }
            else
            {
                return await _coreDbContext.SnTalkType.Where(s => true)
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
            _coreDbContext.SnTalkType.Update(Entity);
            return await _coreDbContext.SaveChangesAsync()>0;
        }
    }
}
