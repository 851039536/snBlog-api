using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;
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
        private readonly Res<SnVideo> res = new();
        private readonly ResDto<SnVideoDto> resDto = new();
        private readonly IMapper _mapper;
        public SnVideoService(ILogger<SnVideoService> logger, snblogContext service, ICacheUtil cacheutil, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _mapper = mapper;
        }
        public async Task<SnVideoDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("主键查询_SnVideoDto=>" + id + cache);
            resDto.entity = _cacheutil.CacheString("GetByIdAsync_SnVideo" + id + cache, resDto.entity, cache);
            if (resDto.entity == null)
            {
                resDto.entity = _mapper.Map<SnVideoDto>(await _service.SnVideos.FindAsync(id));
                _cacheutil.CacheString("GetByIdAsync_SnVideo" + id + cache, resDto.entity, cache);
            }
            return resDto.entity;
        }
        public async Task<List<SnVideoDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("查询所有SnVideo=>" + cache);
            resDto.entityList = _cacheutil.CacheString("GetAllAsync_SnVideo", resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.AsNoTracking().ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnVideo", resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<List<SnVideo>> GetFyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询 _SnVideo:" + type + pageIndex + pageSize + isDesc + cache);
            res.entityList = _cacheutil.CacheString("GetFyAsync" + type + pageIndex + pageSize + isDesc + cache, res.entityList, cache);
            if (res.entityList == null)
            {
                res.entityList = await GetFyAsyncs(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetFyAsync" + type + pageIndex + pageSize + isDesc + cache, res.entityList, cache);
            }
            return res.entityList;
        }
        private async Task<List<SnVideo>> GetFyAsyncs(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == 9999)
            {
                if (isDesc)
                {
                    res.entityList = await _service.SnVideos.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
                else
                {
                    res.entityList = await _service.SnVideos.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    res.entityList = await _service.SnVideos.Where(s => s.TypeId == type).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                }
                else
                {
                    res.entityList = await _service.SnVideos.Where(s => s.TypeId == type).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
            }
            return res.entityList;
        }

        public async Task<int> GetCountAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation("查询总数_SnVideo=>" + identity + cache + type + cache);
            res.entityInt = _cacheutil.CacheNumber("Count_SnVideo", res.entityInt, cache);
            if (res.entityInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        res.entityInt = await _service.SnVideos.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        res.entityInt = await _service.SnVideos.Where(w => w.Type.Name == type).AsNoTracking().CountAsync();
                        break;
                    case 2:
                        res.entityInt = await _service.SnVideos.Where(w => w.User.Name == type).AsNoTracking().CountAsync();
                        break;
                }
                _cacheutil.CacheNumber("Count_SnVideo", res.entityInt, cache);
            }
            return res.entityInt;
        }

        public async Task<int> GetTypeCount(int type, bool cache)
        {
            _logger.LogInformation("条件查总数 :" + type);
            //读取缓存值
            res.entityInt = _cacheutil.CacheNumber("GetTypeCount_SnVideo" + type + cache, res.entityInt, cache);
            if (res.entityInt == 0)
            {
                res.entityInt = await _service.SnVideos.CountAsync(c => c.TypeId == type);
                _cacheutil.CacheNumber("GetTypeCount_SnVideo" + type + cache, res.entityInt, cache);
            }
            return res.entityInt;
        }

        public async Task<List<SnVideo>> GetTypeAllAsync(int type, bool cache)
        {
            _logger.LogInformation("分类查询:_SnVideo" + type + cache);
            res.entityList = _cacheutil.CacheString("GetTypeAllAsync_SnVideo" + type + cache, res.entityList, cache);
            if (res.entityList == null)
            {
                res.entityList = await _service.SnVideos.Where(s => s.TypeId == type).ToListAsync();
                _cacheutil.CacheString("GetTypeAllAsync_SnVideo" + type + cache, res.entityList, cache);
            }
            return res.entityList;
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
            if (todoItem == null)
            {
                return false;
            }

            _service.SnVideos.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSumAsync(bool cache)
        {
            _logger.LogInformation("统计标题数量_SnVideo：" + cache);
            res.entityInt = _cacheutil.CacheNumber("GetSumAsync_SnVideo" + cache, res.entityInt, cache);
            if (res.entityInt == 0)
            {
                res.entityInt = await GetSum();
                _cacheutil.CacheNumber("GetSumAsync_SnVideo" + cache, res.entityInt, cache);
            }
            return res.entityInt;
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

        public async Task<List<SnVideoDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {
            _logger.LogInformation(message: $"SnVideoDto=>{identity}{type}{name}{cache}");
            resDto.entityList = _cacheutil.CacheString("GetContainsAsync_SnVideoDto" + identity + type + name + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                switch (identity)
                {
                    case 0:
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(
                    await _service.SnVideos
                      .Where(l => l.Title.Contains(name))
                     .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(
                      await _service.SnVideos
                       .Where(l => l.Title.Contains(name) && l.Type.Name == type)
                       .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(
                       await _service.SnVideos
                         .Where(l => l.Title.Contains(name) && l.User.Name == type)
                         .AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + type + name + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<List<SnVideoDto>> GetTypeAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation(message: $"SnVideoDto条件查询=>{identity}{type}{cache}");
            resDto.entityList = _cacheutil.CacheString("GetTypeAsync_SnVideoDto" + identity + type + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                switch (identity)
                {
                    case 1:
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(s => s.User.Name == type).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetTypeAsync_SnVideoDto" + identity + type + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<List<SnVideoDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("SnVideoDto分页查询=>" + identity + pageIndex + pageSize + ordering + isDesc + cache);
            resDto.entityList = _cacheutil.CacheString("GetFyAsync_SnVideoDto" + identity + pageIndex + pageSize + ordering + isDesc + cache, resDto.entityList, cache);

            if (resDto.entityList == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        await GetFyAll(pageIndex, pageSize, ordering, isDesc);
                        break;

                    case 1:
                        await GetFyType(type, pageIndex, pageSize, ordering, isDesc);
                        break;

                    case 2:
                        await GetUser(type, pageIndex, pageSize, ordering, isDesc);
                        break;
                }
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        private async Task GetUser(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.User.Name == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.User.Name == type)
               .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.User.Name == type)
 .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
 .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.User.Name == type)
   .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
   .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }

        private async Task GetFyType(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.Type.Name == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.Type.Name == type)
              .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.Type.Name == type)
                   .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                   .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(await _service.SnVideos.Where(w => w.Type.Name == type)
                 .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }

        private async Task GetFyAll(int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(
                await _service.SnVideos
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(
                await _service.SnVideos
                 .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(
               await _service.SnVideos
               .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<SnVideoDto>>(
                 await _service.SnVideos
                 .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }
    }
}

