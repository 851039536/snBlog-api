using Microsoft.EntityFrameworkCore;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snblog.Repository.Repository;

namespace Snblog.Service
{
    public class SnTalkService : ISnTalkService
    {
        private readonly snblogContext _coreDbContext;//DB

        public SnTalkService(snblogContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<bool> AddAsync(SnTalk Entity)
        {
            await _coreDbContext.SnTalk.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnTalk.CountAsync();
        }

        public async Task<int> CountAsync(int type)
        {
            return await _coreDbContext.SnTalk.Where(s => s.TalkTypeId == type).CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _coreDbContext.SnTalk.FindAsync(id);
            if (todoItem == null) return false;
            _coreDbContext.SnTalk.Remove(todoItem);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnTalk>> GetAllAsync()
        {
            return await _coreDbContext.SnTalk.ToListAsync();
        }

        public async Task<List<SnTalk>> GetAllAsync(int id)
        {
            return await _coreDbContext.SnTalk.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<List<SnTalk>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _coreDbContext.SnTalk.Where(s => true)
              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            }
            else
            {
                return await _coreDbContext.SnTalk.Where(s => true)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
            }
        }

        public async Task<List<SnTalk>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _coreDbContext.SnTalk.Where(s => s.TalkTypeId == type)
              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            }
            else
            {
                return await _coreDbContext.SnTalk.Where(s => s.TalkTypeId == type)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
            }
        }

        public async Task<bool> UpdateAsync(SnTalk Entity)
        {
            _coreDbContext.SnTalk.Update(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }
    }
}
