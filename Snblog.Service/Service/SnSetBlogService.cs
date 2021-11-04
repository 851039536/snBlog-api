using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
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
        private int result_Int;
        private List<SnSetblog> result_List = default;
        private SnSetblogDto resultDto = default;
        private SnSetblog result = default;
        private List<SnSetblogDto> result_ListDto = default;
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

        public async Task<List<SnSetblogDto>> GetfyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询_SnSetBlogDto" + type + pageIndex + pageSize + isDesc + cache);
            result_ListDto = _cacheutil.CacheString("GetfyAsync_SnSetBlogDto" + type + pageIndex + pageSize + isDesc + cache, result_ListDto, cache);
            if (result_ListDto == null)
            {
                result_ListDto = _mapper.Map<List<SnSetblogDto>>(await GetfyTest(type, pageIndex, pageSize, isDesc));
                _cacheutil.CacheString("GetfyAsync_SnSetBlogDto" + type + pageIndex + pageSize + isDesc + cache, result_ListDto, cache);
            }
            return result_ListDto;
        }

        private async Task<List<SnSetblog>> GetfyTest(int label, int pageIndex, int pageSize, bool isDesc)
        {
            if (label == 00)
            {
                if (isDesc)
                {
                    result_List = await _service.SnSetblogs.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync();
                }
                else
                {
                    result_List = await _service.SnSetblogs.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    result_List = await _service.SnSetblogs.Where(s => s.TypeId == label).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync();
                }
                else
                {
                    result_List = await _service.SnSetblogs.Where(s => s.TypeId == label).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync();
                }
            }
            return result_List;
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
            _logger.LogInformation("主键查询_SnSetBlogDto" + id + cache);
            resultDto = _cacheutil.CacheString("GetByIdAsync_SnSetBlogDto" + id + cache, resultDto, cache);
            if (resultDto == null)
            {
                resultDto = _mapper.Map<SnSetblogDto>(await _service.SnSetblogs.FindAsync(id));
                _cacheutil.CacheString("GetByIdAsync_SnSetBlogDto" + id + cache, resultDto, cache);
            }
            return resultDto;
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

        public async Task<int> CountAsync(bool cache)
        {
            _logger.LogInformation("查询总数_SnSetBlogDto" + cache);
            result_Int = _cacheutil.CacheNumber("CountAsync_SnSetBlogDto" + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnSetblogs.AsNoTracking().CountAsync();
                _cacheutil.CacheNumber("CountAsync_SnSetBlogDto" + cache, result_Int, cache);
            }
            return result_Int;
        }
    }
}
