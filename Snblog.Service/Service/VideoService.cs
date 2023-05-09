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
    public class VideoService : IVideoService
        {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly ILogger<VideoService> _logger;
        private readonly Res<Video> res = new();
        private readonly Dto<VideoDto> rDto = new();
        private readonly IMapper _mapper;

        const string VIDEO = "video_";
        const string 主键查询 = "主键查询_";
        const string 查询总数 = "查询总数_";
        const string 模糊查询 = "模糊查询_";
        const string 分页查询 = "分页查询_";
        const string 删除 = "删除_";
        const string 新增_ = "ADD";
        const string 更新 = "更新_";
        public VideoService(ILogger<VideoService> logger,snblogContext service,ICacheUtil cacheutil,IMapper mapper)
            {
            _logger = logger;
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _mapper = mapper;
            }
        public async Task<VideoDto> GetByIdAsync(int id,bool cache)
            {
            _logger.LogInformation($"{VIDEO}{主键查询}{id}{cache}");
            rDto.entity = _cacheutil.CacheString($"{VIDEO}{主键查询}{id}{cache}",rDto.entity,cache);
            if (rDto.entity == null) {
                rDto.entity = _mapper.Map<VideoDto>(await _service.Videos.FindAsync(id));
                _cacheutil.CacheString($"{VIDEO}{主键查询}{id}{cache}",rDto.entity,cache);
                }
            return rDto.entity;
            }
        public async Task<List<VideoDto>> GetAllAsync(bool cache)
            {
            _logger.LogInformation("查询所有SnVideo=>" + cache);
            rDto.entityList = _cacheutil.CacheString("GetAllAsync_SnVideo",rDto.entityList,cache);
            if (rDto.entityList == null) {
                rDto.entityList = _mapper.Map<List<VideoDto>>(await _service.Videos.AsNoTracking().ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnVideo",rDto.entityList,cache);
                }
            return rDto.entityList;
            }

        public async Task<List<Video>> GetFyAsync(int type,int pageIndex,int pageSize,bool isDesc,bool cache)
            {
            _logger.LogInformation("分页查询 _SnVideo:" + type + pageIndex + pageSize + isDesc + cache);
            res.entityList = _cacheutil.CacheString("GetPagingAsync" + type + pageIndex + pageSize + isDesc + cache,res.entityList,cache);
            if (res.entityList == null) {
                res.entityList = await GetFyAsyncs(type,pageIndex,pageSize,isDesc);
                _cacheutil.CacheString("GetPagingAsync" + type + pageIndex + pageSize + isDesc + cache,res.entityList,cache);
                }
            return res.entityList;
            }
        private async Task<List<Video>> GetFyAsyncs(int type,int pageIndex,int pageSize,bool isDesc)
            {
            if (type == 9999) {
                if (isDesc) {
                    res.entityList = await _service.Videos.OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                           .Take(pageSize).ToListAsync();
                    } else {
                    res.entityList = await _service.Videos.OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                             .Take(pageSize).ToListAsync();
                    }
                } else {
                if (isDesc) {
                    res.entityList = await _service.Videos.Where(s => s.TypeId == type).OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                            .Take(pageSize).ToListAsync();
                    } else {
                    res.entityList = await _service.Videos.Where(s => s.TypeId == type).OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                           .Take(pageSize).ToListAsync();
                    }
                }
            return res.entityList;
            }

        public async Task<int> GetSumAsync(int identity,string type,bool cache)
            {
            _logger.LogInformation("查询总数_SnVideo=>" + identity + cache + type + cache);
            res.entityInt = _cacheutil.CacheNumber("Count_SnVideo",res.entityInt,cache);
            if (res.entityInt == 0) {
                switch (identity) {
                    case 0:
                        res.entityInt = await _service.Videos.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        res.entityInt = await _service.Videos.Where(w => w.Type.Name == type).AsNoTracking().CountAsync();
                        break;
                    case 2:
                        res.entityInt = await _service.Videos.Where(w => w.User.Name == type).AsNoTracking().CountAsync();
                        break;
                    }
                _cacheutil.CacheNumber("Count_SnVideo",res.entityInt,cache);
                }
            return res.entityInt;
            }

        public async Task<int> GetTypeCount(int type,bool cache)
            {
            _logger.LogInformation("条件查总数 :" + type);
            //读取缓存值
            res.entityInt = _cacheutil.CacheNumber("GetTypeCount_SnVideo" + type + cache,res.entityInt,cache);
            if (res.entityInt == 0) {
                res.entityInt = await _service.Videos.CountAsync(c => c.TypeId == type);
                _cacheutil.CacheNumber("GetTypeCount_SnVideo" + type + cache,res.entityInt,cache);
                }
            return res.entityInt;
            }

        public async Task<List<Video>> GetTypeAllAsync(int type,bool cache)
            {
            _logger.LogInformation("分类查询:_SnVideo" + type + cache);
            res.entityList = _cacheutil.CacheString("GetTypeAllAsync_SnVideo" + type + cache,res.entityList,cache);
            if (res.entityList == null) {
                res.entityList = await _service.Videos.Where(s => s.TypeId == type).ToListAsync();
                _cacheutil.CacheString("GetTypeAllAsync_SnVideo" + type + cache,res.entityList,cache);
                }
            return res.entityList;
            }

        public async Task<bool> AddAsync(Video entity)
            {
            _logger.LogInformation("添加数据_SnVideo :" + entity);
            await _service.Videos.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
            }

        public async Task<bool> UpdateAsync(Video entity)
            {
            _logger.LogInformation("删除数据_SnVideo :" + entity);
            _service.Videos.Update(entity);
            return await _service.SaveChangesAsync() > 0;
            }

        public async Task<bool> DeleteAsync(int id)
            {
            _logger.LogInformation("删除数据_SnVideo:" + id);
            var todoItem = await _service.Videos.FindAsync(id);
            if (todoItem == null) {
                return false;
                }

            _service.Videos.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
            }

        public async Task<int> GetSumAsync(bool cache)
            {
            _logger.LogInformation("统计标题数量_SnVideo：" + cache);
            res.entityInt = _cacheutil.CacheNumber("GetSumAsync_SnVideo" + cache,res.entityInt,cache);
            if (res.entityInt == 0) {
                res.entityInt = await GetSum();
                _cacheutil.CacheNumber("GetSumAsync_SnVideo" + cache,res.entityInt,cache);
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
            var text = await _service.Videos.Select(c => c.Name).ToListAsync();
            for (int i = 0 ; i < text.Count ; i++) {
                num += text[i].Length;
                }
            return num;
            }

        public async Task<List<VideoDto>> GetContainsAsync(int identity,string type,string name,bool cache)
            {
            _logger.LogInformation(message: $"{VIDEO}{模糊查询}{identity}{type}{name}{cache}");
            rDto.entityList = _cacheutil.CacheString($"{VIDEO}{模糊查询}{identity}{type}{name}{cache}",rDto.entityList,cache);
            if (rDto.entityList == null) {
                switch (identity) {
                    case 0:
                        rDto.entityList = _mapper.Map<List<VideoDto>>(
                    await _service.Videos
                      .Where(l => l.Name.Contains(name))
                     .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        rDto.entityList = _mapper.Map<List<VideoDto>>(
                      await _service.Videos
                       .Where(l => l.Name.Contains(name) && l.Type.Name == type)
                       .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        rDto.entityList = _mapper.Map<List<VideoDto>>(
                       await _service.Videos
                         .Where(l => l.Name.Contains(name) && l.User.Name == type)
                         .AsNoTracking().ToListAsync());
                        break;
                    }
                _cacheutil.CacheString($"{VIDEO}{模糊查询}{type}{name}{cache}",rDto.entityList,cache);
                }
            return rDto.entityList;
            }

        public async Task<List<VideoDto>> GetTypeAsync(int identity,string type,bool cache)
            {
            _logger.LogInformation(message: $"SnVideoDto条件查询=>{identity}{type}{cache}");
            rDto.entityList = _cacheutil.CacheString("GetTypeAsync_SnVideoDto" + identity + type + cache,rDto.entityList,cache);
            if (rDto.entityList == null) {
                switch (identity) {
                    case 1:
                        rDto.entityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        rDto.entityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(s => s.User.Name == type).AsNoTracking().ToListAsync());
                        break;
                    }
                _cacheutil.CacheString("GetTypeAsync_SnVideoDto" + identity + type + cache,rDto.entityList,cache);
                }
            return rDto.entityList;
            }

        public async Task<List<VideoDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,bool isDesc,bool cache)
            {
            _logger.LogInformation($"{VIDEO}{分页查询}{identity}{pageIndex}{pageSize}{isDesc}{cache}");
            rDto.entityList = _cacheutil.CacheString($"{VIDEO}{分页查询}{identity}{pageIndex}{pageSize}{isDesc}{cache}",rDto.entityList,cache);
            if (rDto.entityList == null) {
                switch (identity) //查询条件
                {
                    case 0:
                        await QPagingAll(pageIndex,pageSize,isDesc);
                        break;
                    case 1:
                        await GetFyType(type,pageIndex,pageSize,isDesc);
                        break;
                    case 2:
                        await GetUser(type,pageIndex,pageSize,isDesc);
                        break;
                    }
                _cacheutil.CacheString($"{VIDEO}{分页查询}{identity}{pageIndex}{pageSize}{isDesc}{cache}",rDto.entityList,cache);
                }
            return rDto.entityList;
            }

        private async Task GetUser(string type,int pageIndex,int pageSize,bool isDesc)
            {
            if (isDesc)//降序
            {
                rDto.entityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.User.Name == type)
        .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
        .Take(pageSize).Select(e => new VideoDto {
            Id = e.Id,
            Name = e.Name,
            Img = e.Img,
            User = e.User,
            Url = e.Url,
            TimeCreate = e.TimeCreate,
            TimeModified = e.TimeModified,
            Type = e.Type
            }).AsNoTracking().ToListAsync());

                } else //升序
                  {

                rDto.entityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.User.Name == type)
.OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
.Take(pageSize).Select(e => new VideoDto {
    Id = e.Id,
    Name = e.Name,
    Img = e.Img,
    User = e.User,
    Url = e.Url,
    TimeCreate = e.TimeCreate,
    TimeModified = e.TimeModified,
    Type = e.Type
    }).AsNoTracking().ToListAsync());

                }
            }

        private async Task GetFyType(string type,int pageIndex,int pageSize,bool isDesc)
            {
            if (isDesc)//降序
            {
                rDto.entityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.Type.Name == type)
        .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
        .Take(pageSize).Select(e => new VideoDto {
            Id = e.Id,
            Name = e.Name,
            Img = e.Img,
            User = e.User,
            Url = e.Url,
            TimeCreate = e.TimeCreate,
            TimeModified = e.TimeModified,
            Type = e.Type
            }).AsNoTracking().ToListAsync());

                } else //升序
                  {

                rDto.entityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.Type.Name == type)
           .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
           .Take(pageSize).Select(e => new VideoDto {
               Id = e.Id,
               Name = e.Name,
               Img = e.Img,
               User = e.User,
               Url = e.Url,
               TimeCreate = e.TimeCreate,
               TimeModified = e.TimeModified,
               Type = e.Type
               }).AsNoTracking().ToListAsync());

                }
            }

        private async Task QPagingAll(int pageIndex,int pageSize,bool isDesc)
            {
            if (isDesc)//降序
            {
                rDto.entityList = _mapper.Map<List<VideoDto>>(
        await _service.Videos
        .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
        .Take(pageSize).Select(e => new VideoDto {
            Id = e.Id,
            Name = e.Name,
            Img = e.Img,
            User = e.User,
            Url = e.Url,
            TimeCreate = e.TimeCreate,
            TimeModified = e.TimeModified,
            Type = e.Type
            }).AsNoTracking().ToListAsync());

            } else //升序
                  {
                rDto.entityList = _mapper.Map<List<VideoDto>>(
       await _service.Videos
       .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
       .Take(pageSize).Select(e => new VideoDto {
           Id = e.Id,
           Name = e.Name,
           Img = e.Img,
           User = e.User,
           Url = e.Url,
           TimeCreate = e.TimeCreate,
           TimeModified = e.TimeModified,
           Type = e.Type
           }).AsNoTracking().ToListAsync());

                }
            }
        }
    }

