using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class SnTalkService : ISnTalkService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        // private int result_Int;
        //  private List<SnTalk> result_List = default;

        public SnTalkService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<bool> AddAsync(SnTalk entity)
        {
            await _service.SnTalks.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            return await _service.SnTalks.CountAsync();
        }

        public async Task<int> CountAsync(int type)
        {
            return await _service.SnTalks.Where(s => s.TypeId == type).CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _service.SnTalks.FindAsync(id);
            if (todoItem == null)
            {
                return false;
            }

            _service.SnTalks.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnTalk>> GetAllAsync()
        {
            return await _service.SnTalks.ToListAsync();
        }

        public async Task<List<SnTalk>> GetAllAsync(int id)
        {
            return await _service.SnTalks.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<List<SnTalk>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnTalks.Where(s => true)
              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.SnTalks.Where(s => true)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
            }
        }

        public async Task<List<SnTalk>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnTalks.Where(s => s.TypeId == type)
              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.SnTalks.Where(s => s.TypeId == type)
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
             .Take(pageSize).ToListAsync();
            }
        }
        public async Task<bool> UpdateAsync(SnTalk entity)
        {
            _service.SnTalks.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
