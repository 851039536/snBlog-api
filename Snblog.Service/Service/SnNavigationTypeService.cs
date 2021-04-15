using Microsoft.EntityFrameworkCore;
using Snblog.IService.IService;
using Snblog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class SnNavigationTypeService : ISnNavigationTypeService
    {
        private readonly snblogContext _coreDbContext;//DB

        public SnNavigationTypeService(snblogContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<bool> AddAsync(SnNavigationType entity)
        {
            await _coreDbContext.SnNavigationType.AddAsync(entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnNavigationType.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _coreDbContext.SnNavigationType.FindAsync(id);
            _coreDbContext.SnNavigationType.Remove(todoItem);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnNavigationType>> GetAllAsync()
        {
            return await _coreDbContext.SnNavigationType.ToListAsync();
        }

        public async Task<List<SnNavigationType>> GetAllAsync(int id)
        {
            return await _coreDbContext.SnNavigationType.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<List<SnNavigationType>> GetFyTypeAllAsync(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                if (type.Equals("999"))
                {
                    return await _coreDbContext.SnNavigationType.Where(s => true)
                 .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize).ToListAsync();
                }
                else
                {
                    return await _coreDbContext.SnNavigationType.Where(s => s.NavType == type)
                  .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (type.Equals("999"))
                {
                    return await _coreDbContext.SnNavigationType.Where(s => s.NavType == type)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
                }
                else
                {
                    return await _coreDbContext.SnNavigationType.Where(s => true)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
                }


            }
        }

        public async Task<bool> UpdateAsync(SnNavigationType entity)
        {
            _coreDbContext.SnNavigationType.Update(entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }
    }
}
