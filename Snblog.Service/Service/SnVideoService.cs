using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnVideoService : ISnVideoService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly ILogger<SnVideoService> _logger;
        private int result_Int;
        private List<SnVideo> result_List = default;
        public SnVideoService(ILogger<SnVideoService> logger, snblogContext service, ICacheUtil cacheutil)
        {
            _logger = logger;
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }



        public async Task<SnVideo> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("主键查询_SnVideo:" + id + cache);
            SnVideo result = null;
            result = _cacheutil.CacheString("GetByIdAsync_SnVideo" + id + cache, result, cache);
            if (result == null)
            {
                result = await _service.SnVideos.FindAsync(id);
                _cacheutil.CacheString("GetByIdAsync_SnVideo" + id + cache, result, cache);
            }
            return result;
        }














        public async Task<List<SnVideo>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("查询所有-SnVideo");
            result_List = _cacheutil.CacheString1("GetAllAsync_SnVideo", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnVideos.ToListAsync();
                _cacheutil.CacheString("GetAllAsync_SnVideo", result_List, cache);
            }
            return result_List;
        }

        public async Task<List<SnVideo>> GetFyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询 _SnVideo:" + type + pageIndex + pageSize + isDesc + cache);
            result_List = _cacheutil.CacheString("GetFyAsync" + type + pageIndex + pageSize + isDesc + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await GetFyAsyncs(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetFyAsync" + type + pageIndex + pageSize + isDesc + cache, result_List, cache);
            }
            return result_List;
        }

        private async Task<List<SnVideo>> GetFyAsyncs(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == 9999)
            {
                if (isDesc)
                {
                    result_List = await _service.SnVideos.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnVideos.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    result_List = await _service.SnVideos.Where(s => s.TypeId == type).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnVideos.Where(s => s.TypeId == type).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
            }
            return result_List;
        }

        public async Task<int> GetCountAsync(bool cache)
        {
            _logger.LogInformation("查询总数_SnVideo:" + cache);
            result_Int = _cacheutil.CacheNumber("Count_SnVideo", result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnVideos.CountAsync();
                _cacheutil.CacheNumber("Count_SnVideo", result_Int, cache);
            }
            return result_Int;
        }

        public async Task<int> GetTypeCount(int type, bool cache)
        {
            _logger.LogInformation("条件查总数 :" + type);
            //读取缓存值
            result_Int = _cacheutil.CacheNumber("GetTypeCount_SnVideo" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnVideos.CountAsync(c => c.TypeId == type);
                _cacheutil.CacheNumber("GetTypeCount_SnVideo" + type + cache, result_Int, cache);
            }
            return result_Int;
        }

        public async Task<List<SnVideo>> GetTypeAllAsync(int type, bool cache)
        {
            _logger.LogInformation("分类查询:_SnVideo" + type + cache);
            result_List = _cacheutil.CacheString("GetTypeAllAsync_SnVideo" + type + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await _service.SnVideos.Where(s => s.TypeId == type).ToListAsync();
                _cacheutil.CacheString("GetTypeAllAsync_SnVideo" + type + cache, result_List, cache);
            }
            return result_List;
        }

        public async Task<bool> AddAsync(SnVideo entity)
        {
            _logger.LogInformation("添加数据_SnVideo :" + entity);
            await _service.SnVideos.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SnVideo entity)
        {
            _logger.LogInformation("删除数据_SnVideo :" + entity);
            _service.SnVideos.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnVideo:" + id);
            var todoItem = await _service.SnVideos.FindAsync(id);
            if (todoItem == null) return false;
            _service.SnVideos.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSumAsync(bool cache)
        {
            _logger.LogInformation("统计标题数量_SnVideo：" + cache);
            result_Int = _cacheutil.CacheNumber("GetSumAsync_SnVideo"+cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await GetSum();
                _cacheutil.CacheNumber("GetSumAsync_SnVideo"+cache, result_Int, cache);
            }
            return result_Int;
        }

        /// <summary>
        /// 统计字段数
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetSum()
        {
            int num = 0;
            var text = await _service.SnVideos.Select(c => c.Title).ToListAsync();
            for (int i = 0; i < text.Count; i++)
            {
                num += text[i].Length;
            }
            return num;
        }


    }
}

