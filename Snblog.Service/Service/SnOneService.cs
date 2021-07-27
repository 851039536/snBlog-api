using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.IService.IService;
using Snblog.Models;

namespace Snblog.Service.Service
{
    public class SnOneService : ISnOneService
    {
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnOne> result_List = null;
        private readonly snblogContext _service;//DB

        private readonly ILogger<SnOneService> _logger;
        public SnOneService(snblogContext service, ICacheUtil cacheutil, ILogger<SnOneService> logger)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
        }

        public async Task<List<SnOne>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("查询所有" + cache);
            result_List = _cacheutil.CacheString("GetAllAsync_SnOne" + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await _service.SnOne.ToListAsync();
                _cacheutil.CacheString("GetAllAsync_SnOne" + cache, result_List, cache);
            }
            return result_List;
        }


        public async Task<SnOne> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("主键查询_SnOne" + id, cache);
            SnOne result = default;
            result = _cacheutil.CacheString("GetByIdAsync_SnOne" + id + cache, result, cache);
            if (result == null)
            {
                result = await _service.SnOne.FindAsync(id);
                _cacheutil.CacheString("GetByIdAsync_SnOne" + id + cache, result, cache);
            }
            return result;
        }

        public async Task<List<SnOne>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询_SnOne" + pageIndex + pageSize + isDesc + cache);
            result_List = _cacheutil.CacheString("GetFyAllAsync_SnOne" + pageIndex + pageSize + isDesc + cache, result_List, cache);
            if (result_List == null)
            {
                if (isDesc)
                {
                    result_List = await _service.SnOne.OrderByDescending(c => c.OneId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnOne.OrderBy(c => c.OneId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }

                _cacheutil.CacheString("GetFyAllAsync_SnOne" + pageIndex + pageSize + isDesc + cache, result_List, cache);
            }
            return result_List;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnOne" + id);
            var result = await _service.SnOne.FindAsync(id);
            if (result == null) return false;
            _service.SnOne.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnOne entity)
        {
            _logger.LogInformation("添加数据_SnOne" + entity);
            await _service.SnOne.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
            // return await CreateService<SnOne>().AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(SnOne entity)
        {
            _logger.LogInformation("更新数据_SnOne" + entity);
            _service.SnOne.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync(bool cache)
        {
            _logger.LogInformation("查询总数_SnOne" + cache);
            result_Int = _cacheutil.CacheNumber("CountAsync_SnOne" + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnOne.CountAsync();
                _cacheutil.CacheNumber("CountAsync_SnOne" + cache, result_Int, cache);
            }
            return result_Int;
        }

        public async Task<int> CountTypeAsync(int type, bool cache)
        {

            _logger.LogInformation("条件查询总数_SnOne" + type + cache);
            result_Int = _cacheutil.CacheNumber("CountTypeAsync_SnOne" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnOne.CountAsync(s => s.OneTypeId == type);
                _cacheutil.CacheNumber("CountTypeAsync_SnOne" + type + cache, result_Int, cache);
            }
            return result_Int;

        }

        public async Task<List<SnOne>> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc, bool cache)
        {
            _logger.LogInformation("条件分页查询总数_SnOne" + type + pageIndex + pageSize + name + isDesc + cache);
            result_List = _cacheutil.CacheString("GetFyTypeAsync_SnOne" + type + pageIndex + pageSize + name + isDesc + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await GetListFyAsync(type, pageIndex, pageSize, name, isDesc);
                _cacheutil.CacheString("GetFyTypeAsync_SnOne" + type + pageIndex + pageSize + name + isDesc + cache, result_List, cache);
            }
            return result_List;
        }

        private async Task<List<SnOne>> GetListFyAsync(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            if (isDesc) //降序
            {
                if (type.Equals(999)) //表示查所有
                {
                    switch (name)
                    {
                        case "read":
                            return await _service.SnOne.OrderByDescending(c => c.OneRead).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        case "data":
                            return await _service.SnOne.OrderByDescending(c => c.OneData).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        case "give":
                            return await _service.SnOne.OrderByDescending(c => c.OneGive).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        case "comment":
                            return await _service.SnOne.OrderByDescending(c => c.OneComment).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        default:
                            return await _service.SnOne.OrderByDescending(c => c.OneId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _service.SnOne.Where(s => s.OneTypeId == type).OrderByDescending(c => c.OneId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
            else //升序
            {
                if (type.Equals(999)) //表示查所有
                {
                    switch (name)
                    {
                        case "read":
                            return await _service.SnOne.OrderBy(c => c.OneRead).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        case "data":
                            return await _service.SnOne.OrderBy(c => c.OneData).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        case "give":
                            return await _service.SnOne.OrderBy(c => c.OneGive).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        case "comment":
                            return await _service.SnOne.OrderBy(c => c.OneComment).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                        default:
                            return await _service.SnOne.OrderBy(c => c.OneId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _service.SnOne.Where(s => s.OneTypeId == type).OrderBy(c => c.OneId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
        }

        public async Task<int> GetSumAsync(string type, bool cache)
        {

            _logger.LogInformation("统计[字段/阅读/点赞]总数量_SnOne" + type + cache);
            result_Int = _cacheutil.CacheNumber("GetSumAsync_SnOne" + type + cache, result_Int, cache);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await GetSum(type);
            _cacheutil.CacheNumber("GetSumAsync_SnOne" + type + cache, result_Int, cache);
            return result_Int;
        }

        private async Task<int> GetSum(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                    var read = await _service.SnOne.Select(c => c.OneRead).ToListAsync();
                    foreach (var i in read)
                    {
                        var item = i;
                        num += item;
                    }

                    break;
                case "text":
                    var text = await _service.SnOne.Select(c => c.OneText).ToListAsync();
                    foreach (var t in text)
                    {
                        num += t.Length;
                    }

                    break;
                case "give":
                    var give = await _service.SnOne.Select(c => c.OneGive).ToListAsync();
                    foreach (var i in give)
                    {
                            var item = i;
                            num += item;
                    }

                    break;
            }

            return num;
        }

        public async Task<bool> UpdatePortionAsync(SnOne snOne, string type)
        {
            _logger.LogInformation("新部分列_snOne" + snOne + type);
            var date = _service.SnOne.Update(snOne);

            //默认不更新
            date.Property("OneId").IsModified = false;
            date.Property("OneTitle").IsModified = false;
            date.Property("OneText").IsModified = false;
            date.Property("OneImg").IsModified = false;
            date.Property("OneTypeId").IsModified = false;
            date.Property("OneAuthor").IsModified = false;
            date.Property("OneData").IsModified = false;
            date.Property("OneRead").IsModified = false;
            date.Property("OneGive").IsModified = false;
            date.Property("OneComment").IsModified = false;

            switch (type)
            {    //指定字段进行更新操作
                case "give":
                    date.Property("OneGive").IsModified = true;
                    break;
                case "read":
                    date.Property("OneRead").IsModified = true;
                    break;
            }
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
