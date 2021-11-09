using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class SnSetBlogService : ISnSetBlogService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly ILogger<SnSetBlogService> _logger;

        Tool<SnSetblog> data = new Tool<SnSetblog>();
        Tool<SnSetblogDto> datas = new Tool<SnSetblogDto>();
        private readonly IMapper _mapper;
        public SnSetBlogService(ICacheUtil cacheUtil, snblogContext coreDbContext, ILogger<SnSetBlogService> logger, IMapper mapper)
        {
            _service = coreDbContext;
            _cacheutil = (CacheUtil)cacheUtil;
            _logger = logger ?? throw new ArgumentNullException(nameof(Logger));
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnSetBlogs" + id);
            var reslult = await _service.SnSetblogs.FindAsync(id);
            if (reslult == null) return false;
            _service.SnSetblogs.Remove(reslult);//删除单个
            _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnSetblogDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("SnSetBlogDto分页查询=>" + type + pageIndex + pageSize + isDesc + cache);
           datas.resultListDto = _cacheutil.CacheString("GetfyAsync_SnSetBlogDto" + type + pageIndex + pageSize + isDesc + cache, datas.resultListDto, cache);
        
                if (datas.resultListDto == null)
                {
                    switch (identity) //查询条件
                    {
                        case 0:
                            if (isDesc)//降序
                            {
                                switch (ordering) //排序
                                {
                                    case "id":
                                        datas.resultListDto = _mapper.Map<List<SnSetblogDto>>(
                                await _service.SnSetblogs
                                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize).AsNoTracking().ToListAsync());
                                        break;
                                }
                            }
                            else //升序
                            {
                                switch (ordering) //排序
                                {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnSetblogDto>>(
                            await _service.SnSetblogs
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
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
                                        datas.resultListDto = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.Type.Name == type)
                                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize).AsNoTracking().ToListAsync());
                                        break;
                                }
                            }
                            else //升序
                            {
                                switch (ordering) //排序
                                {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.Type.Name == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
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
                                        datas.resultListDto = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.User.Name == type)
                                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize).AsNoTracking().ToListAsync());
                                        break;
                                }
                            }
                            else //升序
                            {
                                switch (ordering) //排序
                                {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.User.Name == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                            }
                            break;
                }
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }

        private async Task<List<SnSetblog>> GetfyTest(int label, int pageIndex, int pageSize, bool isDesc)
        {
            if (label == 00)
            {
                if (isDesc)
                {
                   data.resultList = await _service.SnSetblogs.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync();
                }
                else
                {
                   data.resultList = await _service.SnSetblogs.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                   data.resultList = await _service.SnSetblogs.Where(s => s.TypeId == label).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync();
                }
                else
                {
                   data.resultList = await _service.SnSetblogs.Where(s => s.TypeId == label).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync();
                }
            }
            return data.resultList;
        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnSetblogDto entity)
        {
            _logger.LogInformation("添加数据_SnSetBlog" + entity);
            await _service.SnSetblogs.AddAsync(_mapper.Map<SnSetblog>(entity));
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateAsync(SnSetblogDto entity)
        {
            _logger.LogInformation("更新数据_SnArticle" + entity);
            _service.SnSetblogs.Update(_mapper.Map<SnSetblog>(entity));
            return await _service.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdatePortionAsync(SnSetblogDto entity, string type)
        {
            //var res= _mapper.Map<SnSetBlog>(entity);
            var resulet = await _service.SnSetblogs.FindAsync(entity.Id);
            if (resulet == null) return false;
            switch (type)
            {    //指定字段进行更新操作
                case "type":
                    //修改属性，被追踪的Read状态属性就会变为Modify
                    resulet.Isopen = entity.Isopen;
                    break;
            }
            ////执行数据库操作
            return await _service.SaveChangesAsync() > 0;
        }

        public Task<List<SnSetblog>> GetAllAsync(bool cache)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnArticleDto>> GetContainsAsync(string name, bool cache)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetSumAsync(string type, bool cache)
        {
            throw new NotImplementedException();
        }

        public async Task<SnSetblogDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("SnSetBlogDto主键查询=>" + id + cache);
           datas.resultDto = _cacheutil.CacheString("GetByIdAsync_SnSetBlogDto" + id + cache, datas.resultDto, cache);
            if (datas.resultDto == null)
            {
                datas.resultDto = _mapper.Map<SnSetblogDto>(await _service.SnSetblogs.FindAsync(id));
                _cacheutil.CacheString("GetByIdAsync_SnSetBlogDto" + id + cache, datas.resultDto, cache);
            }
            return datas.resultDto;
        }

        public Task<List<SnArticle>> GetTypeIdAsync(int sortId, bool cache)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnArticle>> GetfySortTestAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnArticle>> GetFyAsync(int type, int pageIndex, int pageSize, string name, bool isDesc, bool cache)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnArticle>> GetTagAsync(int tag, bool isDesc, bool cache)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetConutSortAsync(int type, bool cache)
        {
            throw new NotImplementedException();
        }

        public int GetTypeCountAsync(int type, bool cache)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation("SnSetBlogDto查询总数=>" + cache);
            datas.resulInt = _cacheutil.CacheNumber("CountAsync_SnSetBlogDto" + cache, datas.resulInt, cache);
            if (datas.resulInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        datas.resulInt = await _service.SnSetblogs.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        datas.resulInt = await _service.SnSetblogs.Where(w=>w.Type.Name== type).AsNoTracking().CountAsync();
                        break;
                    case 2:
                        datas.resulInt = await _service.SnSetblogs.Where(w => w.User.Name == type).AsNoTracking().CountAsync();
                        break;
                }
                _cacheutil.CacheNumber("CountAsync_SnSetBlogDto" + cache, datas.resulInt, cache);
            }
            return datas.resulInt;
        }
    }
}
