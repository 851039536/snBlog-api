using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnOneService : ISnOneService
    {
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnOne> result_List = null;
        private readonly snblogContext _service;//DB
        public SnOneService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<List<SnOne>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString("SnOne_GetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnOne.ToListAsync();
                _cacheutil.CacheString("SnOne_GetAllAsync", result_List);
            }
            return result_List;
        }


        public async Task<SnOne> GetByIdAsync(int id)
        {
            SnOne result = default;
            result = _cacheutil.CacheString("SnOne_GetByIdAsync" + id, result);
            if (result == null)
            {
                result = await _service.SnOne.FindAsync(id);
                _cacheutil.CacheString("SnOne_GetByIdAsync" + id, result);
            }
            return result;
        }

        public async Task<List<SnOne>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString("SnOne_GetFyAllAsync"+pageIndex+pageSize+isDesc, result_List);
            if (result_List == null)
            {
                result_List = await _service.SnOne.OrderByDescending(c => c.OneRead).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                _cacheutil.CacheString("SnOne_GetFyAllAsync"+pageIndex+pageSize+isDesc, result_List);
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
            await _service.SnOne.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
            // return await CreateService<SnOne>().AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(SnOne entity)
        {
            _service.SnOne.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber("SnOne_CountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnOne.CountAsync();
                _cacheutil.CacheNumber("SnOne_CountAsync", result_Int);
            }
            return result_Int;
        }

        public async Task<int> CountTypeAsync(int type)
        {
            result_Int = _cacheutil.CacheNumber("SnOne_CountTypeAsync"+type, result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnOne.CountAsync(s => s.OneTypeId == type);
               _cacheutil.CacheNumber("SnOne_CountTypeAsync"+type, result_Int);
            }
            return result_Int;

        }

        public async Task<List<SnOne>> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            result_List = _cacheutil.CacheString("SnOne_GetFyTypeAsync"+type+pageIndex+pageSize+name+isDesc, result_List);
            if (result_List == null)
            {
                result_List = await GetListFyAsync(type, pageIndex, pageSize, name, isDesc);
             _cacheutil.CacheString("SnOne_GetFyTypeAsync"+type+pageIndex+pageSize+name+isDesc, result_List);
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

        public async Task<int> GetSumAsync(string type)
        {
            result_Int = _cacheutil.CacheNumber("SnOne_GetSumAsync"+type, result_Int);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await GetSum(type);
           _cacheutil.CacheNumber("SnOne_GetSumAsync"+type, result_Int);
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
                        if (i != null)
                        {
                            var item = (int)i;
                            num += item;
                        }
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
                        if (i != null)
                        {
                            var item = (int)i;
                            num += item;
                        }
                    }

                    break;
            }

            return num;
        }
    }
}
