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
    public class SnPictureService : ISnPictureService
    {
         private readonly snblogContext _coreDbContext;//DB

        public SnPictureService(snblogContext coreDbContext)
        {
              _coreDbContext = coreDbContext;
        }

        public async Task<bool> AddAsync(SnPicture Entity)
        {
              await _coreDbContext.SnPicture.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync()>0;
        }

        public async Task<List<SnPicture>> GetAllAsync()
        {
          return await _coreDbContext.SnPicture.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnPicture.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // _coreDbContext.SnPicture.Remove(Entity);
            //return await _coreDbContext.SaveChangesAsync()>0;
            //执行查询
            var todoItem = await _coreDbContext.SnPicture.FindAsync(id);
              _coreDbContext.SnPicture.Remove(todoItem);
            return  await _coreDbContext.SaveChangesAsync()>0;

        }

        public async Task<List<SnPicture>> GetAllAsync(int id)
        {
           return await _coreDbContext.SnPicture.Where(s =>s.PictureId == id).ToListAsync();
        }

        public async Task<bool> UpdateAsync(SnPicture Entity)
        {
            _coreDbContext.SnPicture.Update(Entity);
            return await _coreDbContext.SaveChangesAsync()>0;
        }

        public async  Task<List<SnPicture>> GetFyAllAsync( int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                  return await _coreDbContext.SnPicture.Where(s => true)
                .OrderByDescending(c => c.PictureId).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            }
            else
            {
                   return await _coreDbContext.SnPicture.Where(s => true)
                .OrderBy(c => c.PictureId).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            }
           
        }

        public async Task<List<SnPicture>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                  return await _coreDbContext.SnPicture.Where(s => s.PictureTypeId == type)
                .OrderByDescending(c => c.PictureId).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            }
            else
            {
                   return await _coreDbContext.SnPicture.Where(s => s.PictureTypeId == type)
                .OrderBy(c => c.PictureId).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            }
        }

        public async Task<int> CountAsync(int type)
        {
           return await _coreDbContext.SnPicture.Where(s=>s.PictureTypeId == type).CountAsync();
        }
    }
}
