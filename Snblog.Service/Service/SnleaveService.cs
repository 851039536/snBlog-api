using Microsoft.EntityFrameworkCore;
using Snblog.IService.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class SnleaveService : ISnleaveService
    {
        private readonly snblogContext _coreDbContext;//DB

        public SnleaveService(snblogContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<bool> AddAsync(SnLeave Entity)
        {
            await _coreDbContext.SnLeave.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnLeave.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _coreDbContext.SnLeave.FindAsync(id);
            _coreDbContext.SnLeave.Remove(todoItem);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnLeave>> GetAllAsync()
        {
            return await _coreDbContext.SnLeave.ToListAsync();
        }

        public async Task<List<SnLeave>> GetAllAsync(int id)
        {
            return await _coreDbContext.SnLeave.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<List<SnLeave>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _coreDbContext.SnLeave.Where(s => true)
              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            }
            else
            {
                return await _coreDbContext.SnLeave.Where(s => true)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
            }
        }

        public async Task<bool> UpdateAsync(SnLeave Entity)
        {
            _coreDbContext.SnLeave.Update(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }
    }
}
