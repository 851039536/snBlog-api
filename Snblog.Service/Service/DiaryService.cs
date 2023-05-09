using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class DiaryService : IDiaryService
    {

        string cacheKey;
        const string NAME = "diary_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        private readonly CacheUtil _cacheutil;
        private readonly snblogContext _service;//DB
        readonly Res<Diary> res = new();
        readonly Dto<DiaryDto> resDto = new();
        private readonly ILogger<DiaryService> _logger;

        private readonly IMapper _mapper;
        public DiaryService(snblogContext service,ICacheUtil cacheutil,ILogger<DiaryService> logger,IMapper mapper)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<DiaryDto> GetByIdAsync(int id,bool cache)
        {
            cacheKey = $"{NAME}{BYID}{id}";
            _logger.LogInformation(cacheKey,cache);
            resDto.entity = _cacheutil.CacheString(cacheKey,resDto.entity,cache);

            if (resDto.entity != null) return resDto.entity;

            resDto.entity = _mapper.Map<DiaryDto>(
   await _service.Diaries.Include(i => i.User)
              .Include(i => i.Type)
              .AsNoTracking()
              .SingleOrDefaultAsync(b => b.Id == id));

             _cacheutil.CacheString(cacheKey,resDto.entity,cache);

            return resDto.entity;
        }

        public async Task<List<DiaryDto>> GetFyAsync(int identity,string type,int pageIndex,int pageSize,string ordering,bool isDesc,bool cache)
        {
            _logger.LogInformation("SnOneDto分页查询=>" + identity + pageIndex + pageSize + ordering + isDesc + cache);
            resDto.entityList = _cacheutil.CacheString("GetFyAsync_SnOneDto" + identity + pageIndex + pageSize + ordering + isDesc + cache,resDto.entityList,cache);

            if (resDto.entityList == null) {
                switch (identity) //查询条件
                {
                    case 0:
                    if (isDesc)//降序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(
                    await _service.Diaries.OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "data":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries
                    .OrderByDescending(c => c.TimeCreate).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "read":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries
                    .OrderByDescending(c => c.Read).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "give":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries
                    .OrderByDescending(c => c.Give).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                        }
                    } else //升序
                      {
                        switch (ordering) //排序
                        {
                            case "id":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(
                    await _service.Diaries.OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "data":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries
                    .OrderBy(c => c.TimeCreate).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "read":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries
                    .OrderBy(c => c.Read).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "give":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries
                    .OrderBy(c => c.Give).Skip(( pageIndex - 1 ) * pageSize)
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
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "data":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderByDescending(c => c.TimeCreate).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "read":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderByDescending(c => c.Read).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "give":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderByDescending(c => c.Give).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                        }
                    } else //升序
                      {
                        switch (ordering) //排序
                        {
                            case "id":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "data":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderBy(c => c.TimeCreate).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "read":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderBy(c => c.Read).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "give":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.Type.Name == type)
                    .OrderBy(c => c.Give).Skip(( pageIndex - 1 ) * pageSize)
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
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                    .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "data":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                   .OrderByDescending(c => c.TimeCreate).Skip(( pageIndex - 1 ) * pageSize)
                   .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "read":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                   .OrderByDescending(c => c.Read).Skip(( pageIndex - 1 ) * pageSize)
                   .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "give":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                   .OrderByDescending(c => c.Give).Skip(( pageIndex - 1 ) * pageSize)
                   .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                        }
                    } else //升序
                      {
                        switch (ordering) //排序
                        {
                            case "id":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                    .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "data":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                   .OrderBy(c => c.TimeCreate).Skip(( pageIndex - 1 ) * pageSize)
                   .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "read":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                   .OrderBy(c => c.Read).Skip(( pageIndex - 1 ) * pageSize)
                   .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                            case "give":
                            resDto.entityList = _mapper.Map<List<DiaryDto>>(await _service.Diaries.Where(w => w.User.Name == type)
                   .OrderBy(c => c.Give).Skip(( pageIndex - 1 ) * pageSize)
                   .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                        }
                    }
                    break;
                }
                _cacheutil.CacheString("GetFyAsync_SnOneDto" + identity + pageIndex + pageSize + ordering + isDesc + cache,resDto.entityList,cache);
            }
            return resDto.entityList;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnOne" + id);
            var result = await _service.Diaries.FindAsync(id);
            if (result == null) return false;
            _service.Diaries.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Diary entity)
        {
            _logger.LogInformation("添加数据_SnOne" + entity);
            await _service.Diaries.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
            // return await CreateService<Diary>().AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Diary entity)
        {
            _logger.LogInformation("更新数据_SnOne" + entity);
            _service.Diaries.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户2  </param>
        /// <param name="type">条件(identity为0则null) </param>
        /// <param name="cache"></param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(int identity,string type,bool cache)
        {
            cacheKey = $"{NAME}{SUM}{identity}{type}{cache}";
            _logger.LogInformation(cacheKey);

            res.entityInt = _cacheutil.CacheNumber(cacheKey,res.entityInt,cache);
            if (res.entityInt != 0) return res.entityInt;

            return identity switch {
                0 => await GetDiaryCountAsync(),
                1 => await GetDiaryCountAsync(w => w.Type.Name == type),
                2 => await GetDiaryCountAsync(w => w.User.Name == type),
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// 获取文章的数量
        /// </summary>
        /// <param name="predicate">筛选文章的条件</param>
        /// <returns>返回文章的数量</returns>
        private async Task<int> GetDiaryCountAsync(Expression<Func<Diary,bool>> predicate = null)
        {
            IQueryable<Diary> query = _service.Diaries.AsNoTracking();

            if (predicate != null) { //如果有筛选条件
                query = query.Where(predicate);
            }
            int count = await query.CountAsync();
            _cacheutil.CacheNumber(cacheKey,count,true); //设置缓存
            return count;
        }

        public async Task<int> CountTypeAsync(int type,bool cache)
        {

            _logger.LogInformation("条件查询总数_SnOne" + type + cache);
            res.entityInt = _cacheutil.CacheNumber("CountTypeAsync_SnOne" + type + cache,res.entityInt,cache);
            if (res.entityInt == 0) {
                res.entityInt = await _service.Diaries.CountAsync(s => s.TypeId == type);
                _cacheutil.CacheNumber("CountTypeAsync_SnOne" + type + cache,res.entityInt,cache);
            }
            return res.entityInt;

        }

        public async Task<int> GetSumAsync(string type,bool cache)
        {

            _logger.LogInformation("Diary统计[字段/阅读/点赞]总数量=>" + type + cache);
            res.entityInt = _cacheutil.CacheNumber("GetSumAsync_SnOne" + type + cache,res.entityInt,cache);
            if (res.entityInt != 0) {
                return res.entityInt;
            }
            res.entityInt = await GetSum(type);
            _cacheutil.CacheNumber("GetSumAsync_SnOne" + type + cache,res.entityInt,cache);
            return res.entityInt;
        }

        private async Task<int> GetSum(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                var read = await _service.Diaries.Select(c => c.Read).ToListAsync();
                foreach (var i in read) {
                    var item = i;
                    num += item;
                }

                break;
                case "text":
                var text = await _service.Diaries.Select(c => c.Text).ToListAsync();
                foreach (var t in text) {
                    num += t.Length;
                }

                break;
                case "give":
                var give = await _service.Diaries.Select(c => c.Give).ToListAsync();
                foreach (var i in give) {
                    var item = i;
                    num += item;
                }

                break;
            }

            return num;
        }

        public async Task<bool> UpdatePortionAsync(Diary snOne,string type)
        {
            _logger.LogInformation("新部分列_snOne" + snOne + type);

            var resulet = await _service.Diaries.FindAsync(snOne.Id);
            if (resulet == null) return false;
            switch (type) {    //指定字段进行更新操作
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

        public async Task<List<DiaryDto>> GetContainsAsync(int identity,string type,string name,bool cache)
        {
            var upNames = name.ToUpper();
            cacheKey = $"{NAME}{CONTAINS}{identity}{type}{name}{cache}";
            _logger.LogInformation(message: cacheKey);
            resDto.entityList = _cacheutil.CacheString(cacheKey,resDto.entityList,cache);

            if (resDto.entityList != null) return resDto.entityList;

            return identity switch {
                0 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
                1 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(name) && l.Type.Name == type),
                2 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(name) && l.User.Name == type),
                _ => null,
            };
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        private async Task<List<DiaryDto>> GetDiaryContainsAsync(Expression<Func<Diary,bool>> predicate = null)
        {
            IQueryable<Diary> query = _service.Diaries.AsNoTracking();
            if (predicate != null) {
                resDto.entityList = await query.Where(predicate).Select(e => new DiaryDto {
                    Id = e.Id,
                    Name = e.Name,
                    Text = e.Text,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                }).ToListAsync();
                _cacheutil.CacheNumber(cacheKey,resDto.entityList,true); //设置缓存
            }
            return resDto.entityList;
        }
    }
}
