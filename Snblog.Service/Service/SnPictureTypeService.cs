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
    public class SnPictureTypeService : ISnPictureTypeService
    {
        private readonly snblogContext _coreDbContext;//DB
        public SnPictureTypeService(snblogContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }


       public async Task<bool> AddAsync(SnPictureType Entity)
        {
              await _coreDbContext.SnPictureType.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync()>0;
        }

        public async Task<List<SnPictureType>> GetAllAsync()
        {
          return await _coreDbContext.SnPictureType.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnPictureType.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // _coreDbContext.SnPictureType.Remove(Entity);
            //return await _coreDbContext.SaveChangesAsync()>0;
              var todoItem = await _coreDbContext.SnPictureType.FindAsync(id);
              _coreDbContext.SnPictureType.Remove(todoItem);
            return  await _coreDbContext.SaveChangesAsync()>0;
        }

        public async Task<List<SnPictureType>> GetAllAsync(int id)
        {
           return await _coreDbContext.SnPictureType.Where(s =>s.Id == id).ToListAsync();
        }

        public async Task<bool> UpdateAsync(SnPictureType Entity)
        {
            _coreDbContext.SnPictureType.Update(Entity);
            return await _coreDbContext.SaveChangesAsync()>0;
        }

        public async  Task<List<SnPictureType>> GetFyAllAsync( int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                  return await _coreDbContext.SnPictureType.Where(s => true)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            }
            else
            {
                   return await _coreDbContext.SnPictureType.Where(s => true)
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            }
           
        }

        public Task<List<SnPictureType>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            throw new NotImplementedException();
        }
    }
}
