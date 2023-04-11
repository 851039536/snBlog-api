using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;

namespace Snblog.Service.Service
{
    public class SnOneService : ISnOneService
    {
        private readonly CacheUtil _cacheutil;
        private readonly snblogContext _service;//DB
        readonly Res<SnOne> res = new();
        readonly Dto<SnOneDto> resDto = new();
        private readonly ILogger<SnOneService> _logger;

        private readonly IMapper _mapper;
        public SnOneService(snblogContext service, ICacheUtil cacheutil, ILogger<SnOneService> logger, IMapper mapper)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<SnOneDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("查询所有" + cache);
            resDto.eList = _cacheutil.CacheString("GetAllAsync_SnOne" + cache, resDto.eList, cache);
            if (resDto.eList == null)
            {
                resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.AsNoTracking().ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnOne" + cache, resDto.eList, cache);
            }
            return resDto.eList;
        }

        public async Task<SnOneDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("SnOne主键查询=>" + id, cache);
            resDto.entity = _cacheutil.CacheString("GetByIdAsync_SnOne" + id + cache, resDto.entity, cache);
            if (resDto.entity == null)
            {
                resDto.entity = _mapper.Map<SnOneDto>(await _service.SnOnes.FindAsync(id));
                _cacheutil.CacheString("GetByIdAsync_SnOne" + id + cache, resDto.entity, cache);
            }
            return resDto.entity;
        }

        public async Task<List<SnOneDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("SnOneDto分页查询=>" + identity + pageIndex + pageSize + ordering + isDesc + cache);
            resDto.eList = _cacheutil.CacheString("GetFyAsync_SnOneDto" + identity + pageIndex + pageSize + ordering + isDesc + cache, resDto.eList, cache);

            if (resDto.eList == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(
                            await _service.SnOnes.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes
                            .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes
                            .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(
                            await _service.SnOnes.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes
                            .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes
                            .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;

                    case 1:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.Type.Title == type)
                            .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;

                    case 2:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                           .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                           .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                           .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                           .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                           .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    resDto.eList = _mapper.Map<List<SnOneDto>>(await _service.SnOnes.Where(w => w.User.Name == type)
                           .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;
                }
                _cacheutil.CacheString("GetFyAsync_SnOneDto" + identity + pageIndex + pageSize + ordering + isDesc + cache, resDto.eList, cache);
            }
            return resDto.eList;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnOne" + id);
            var result = await _service.SnOnes.FindAsync(id);
            if (result == null) return false;
            _service.SnOnes.Remove(result);
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
            await _service.SnOnes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
            // return await CreateService<SnOne>().AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(SnOne entity)
        {
            _logger.LogInformation("更新数据_SnOne" + entity);
            _service.SnOnes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetCountAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation("查询总数SnOne=>" + cache);
            res.entityInt = _cacheutil.CacheNumber("CountAsync_SnOne" + cache, res.entityInt, cache);
            if (res.entityInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        res.entityInt = await _service.SnOnes.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        res.entityInt = await _service.SnOnes.Where(w => w.Type.Title == type).AsNoTracking().CountAsync();
                        break;
                    case 2:
                        res.entityInt = await _service.SnOnes.Where(w => w.User.Name == type).AsNoTracking().CountAsync();
                        break;
                }
                _cacheutil.CacheNumber("CountAsync_SnOne" + cache, res.entityInt, cache);
            }
            return res.entityInt;
        }

        public async Task<int> CountTypeAsync(int type, bool cache)
        {

            _logger.LogInformation("条件查询总数_SnOne" + type + cache);
            res.entityInt = _cacheutil.CacheNumber("CountTypeAsync_SnOne" + type + cache, res.entityInt, cache);
            if (res.entityInt == 0)
            {
                res.entityInt = await _service.SnOnes.CountAsync(s => s.TypeId == type);
                _cacheutil.CacheNumber("CountTypeAsync_SnOne" + type + cache, res.entityInt, cache);
            }
            return res.entityInt;

        }

        public async Task<int> GetSumAsync(string type, bool cache)
        {

            _logger.LogInformation("SnOne统计[字段/阅读/点赞]总数量=>" + type + cache);
            res.entityInt = _cacheutil.CacheNumber("GetSumAsync_SnOne" + type + cache, res.entityInt, cache);
            if (res.entityInt != 0)
            {
                return res.entityInt;
            }
            res.entityInt = await GetSum(type);
            _cacheutil.CacheNumber("GetSumAsync_SnOne" + type + cache, res.entityInt, cache);
            return res.entityInt;
        }

        private async Task<int> GetSum(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                    var read = await _service.SnOnes.Select(c => c.Read).ToListAsync();
                    foreach (var i in read)
                    {
                        var item = i;
                        num += item;
                    }

                    break;
                case "text":
                    var text = await _service.SnOnes.Select(c => c.Text).ToListAsync();
                    foreach (var t in text)
                    {
                        num += t.Length;
                    }

                    break;
                case "give":
                    var give = await _service.SnOnes.Select(c => c.Give).ToListAsync();
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

            var resulet = await _service.SnOnes.FindAsync(snOne.Id);
            if (resulet == null) return false;
            switch (type)
            {    //指定字段进行更新操作
                case "give":
                    //date.Property("OneGive").IsModified = true;
                    resulet.Give = snOne.Give;
                    break;
                case "read":
                    //date.Property("OneRead").IsModified = true;
                    resulet.Read = snOne.Read;
                    break;
            }
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<List<SnOneDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {
            _logger.LogInformation(message: $"SnOneDto模糊查询=>{type}{name}{cache}");
            resDto.eList = _cacheutil.CacheString("GetContainsAsync_SnOneDto" + type + name + cache, resDto.eList, cache);
            if (resDto.eList == null)
            {
                switch (identity)
                {
                    case 0:
                        resDto.eList = _mapper.Map<List<SnOneDto>>(
                      await _service.SnOnes
                      .Where(l => l.Title.Contains(name))
                     .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        resDto.eList = _mapper.Map<List<SnOneDto>>(
                       await _service.SnOnes
                       .Where(l => l.Title.Contains(name) && l.Type.Title == type)
                       .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.eList = _mapper.Map<List<SnOneDto>>(
                        await _service.SnOnes
                         .Where(l => l.Title.Contains(name) && l.User.Name == type)
                         .AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetContainsAsync_SnOneDto" + type + name + cache, resDto.eList, cache);
            }
            return resDto.eList;
        }
    }
}
