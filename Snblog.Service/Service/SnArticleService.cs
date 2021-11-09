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
    public class SnArticleService : ISnArticleService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly ILogger<SnArticleService> _logger;

        Tool<SnArticle> data = new Tool<SnArticle>();
        Tool<SnArticleDto> datas = new Tool<SnArticleDto>();
        private readonly IMapper _mapper;
        public SnArticleService(ICacheUtil cacheUtil, snblogContext coreDbContext, ILogger<SnArticleService> logger, IMapper mapper)
        {
            _service = coreDbContext;
            _cacheutil = (CacheUtil)cacheUtil;
            _logger = logger ?? throw new ArgumentNullException(nameof(Logger));
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation(message: $"SnArticle删除数据 id:{{id}}");
            var reslult = await _service.SnArticles.FindAsync(id);
            if (reslult == null) return false;
            _service.SnArticles.Remove(reslult);//删除单个
            _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }


        public async Task<SnArticleDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation(message: $"SnArticleDto主键查询 =>id:{id} 缓存:{cache}");
            datas.resultDto = _cacheutil.CacheString("SnArticleDto_GetByIdAsync=>" + id + cache, datas.resultDto, cache);
            if (datas.resultDto == null)
            {
                data.result = await _service.SnArticles.FindAsync(id);
                datas.resultDto = _mapper.Map<SnArticleDto>(data.result);
                _cacheutil.CacheString("SnArticleDto_GetByIdAsync=>" + id + cache, datas.resultDto, cache);
            }
            return datas.resultDto;
        }

        public async Task<List<SnArticleDto>> GetTypeAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation(message: $"SnArticle条件查询=>{type} 缓存:{cache}");
            data.resultList = _cacheutil.CacheString("GetTypeAsync_SnArticle" + type + cache, data.resultList, cache);
            if (data.resultList == null)
            {
                if (identity == 1)
                {
                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => s.Sort.Name == type).AsNoTracking().ToListAsync());
                }
                else
                {
                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => s.Label.Name == type).AsNoTracking().ToListAsync());
                }

                _cacheutil.CacheString("GetTypeAsync_SnArticle" + type + cache, data.resultList, cache);
            }
            return datas.resultListDto;
        }


        public async Task<bool> AddAsync(SnArticle entity)
        {
            _logger.LogInformation("SnArticle添加:" + entity);
            await _service.SnArticles.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(SnArticle entity)
        {
            _logger.LogInformation("SnArticle更新:" + entity);
            _service.SnArticles.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetCountAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation("SnArticle查询总数=>" + identity + cache);
            data.resulInt = _cacheutil.CacheNumber("Count_SnArticle" + identity + cache, data.resulInt, cache);
            if (data.resulInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        data.resulInt = await _service.SnArticles.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        data.resulInt = await _service.SnArticles.AsNoTracking().CountAsync(c => c.Sort.Name == type);
                        break;
                    case 2:
                        data.resulInt = await _service.SnArticles.AsNoTracking().CountAsync(c => c.Label.Name == type);
                        break;
                    case 3:
                        data.resulInt = await _service.SnArticles.AsNoTracking().CountAsync(c => c.User.Name == type);
                        break;
                }
                _cacheutil.CacheNumber("Count_SnArticle" + identity + cache, data.resulInt, cache);
            }
            return data.resulInt;
        }


        public async Task<List<SnArticleDto>> GetAllAsync(bool cache)
        {
            var data = await _service.SnArticles.Select(e => new SnArticleDto
            {
                Id = e.Id,
                Title = e.Title,
                User = e.User,
                Sort = e.Sort,
                Label = e.Label
            }).ToListAsync();
            datas.resultListDto = _mapper.Map<List<SnArticleDto>>(data);
            return datas.resultListDto;

            //_logger.LogInformation("SnArticleDto查询所有" + cache);
            //result_ListDto = _cacheutil.CacheString("GetAllAsync_SnArticleDto" + cache, result_ListDto, cache);
            //if (result_ListDto == null)
            //{
            //    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Include(c=> c.User.UserId).ToListAsync());
            //    _cacheutil.CacheString("GetAllAsync_SnArticleDto" + cache, result_ListDto, cache);
            //}
            //return result_ListDto;
        }

        public async Task<int> GetSumAsync(string type, bool cache)
        {
            _logger.LogInformation("SnArticle统计[字段/阅读/点赞]数量" + type + cache);
            data.resulInt = _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, data.resulInt, cache);
            if (data.resulInt == 0)
            {
                data.resulInt = await GetSum(type);
                _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, data.resulInt, cache);
            }
            return data.resulInt;
        }

        private async Task<int> GetSum(string type)
        {
            _logger.LogInformation("读取总字数：" + type);
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                    var read = await _service.SnArticles.Select(c => c.Read).ToListAsync();
                    foreach (var i in read)
                    {
                        num += i;
                    }
                    break;
                case "text":
                    var text = await _service.SnArticles.Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await _service.SnArticles.Select(c => c.Give).ToListAsync();
                    foreach (var i in give)
                    {
                        num += i;
                    }
                    break;
            }

            return num;
        }

        public async Task<List<SnArticleDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("SnArticle分页查询=>" + identity + pageIndex + pageSize + ordering + isDesc + cache);
            datas.resultListDto = _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, datas.resultListDto, cache);

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
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(
                            await _service.SnArticles
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());

                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
                            .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
                            .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
                            .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
                            .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
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
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
                            .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
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
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).Select(e => new SnArticleDto
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Sketch = e.Sketch,
                                Give = e.Give,
                                Read = e.Read,
                                Img = e.Img,
                                TimeCreate = e.TimeCreate,
                                TimeModified = e.TimeModified,
                                User = e.User,
                                Sort = e.Sort,
                                Label = e.Label
                            }).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                           .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).Select(e => new SnArticleDto
                           {
                               Id = e.Id,
                               Title = e.Title,
                               Sketch = e.Sketch,
                               Give = e.Give,
                               Read = e.Read,
                               Img = e.Img,
                               TimeCreate = e.TimeCreate,
                               TimeModified = e.TimeModified,
                               User = e.User,
                               Sort = e.Sort,
                               Label = e.Label
                           }).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                           .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).Select(e => new SnArticleDto
                           {
                               Id = e.Id,
                               Title = e.Title,
                               Sketch = e.Sketch,
                               Give = e.Give,
                               Read = e.Read,
                               Img = e.Img,
                               TimeCreate = e.TimeCreate,
                               TimeModified = e.TimeModified,
                               User = e.User,
                               Sort = e.Sort,
                               Label = e.Label
                           }).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                           .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).Select(e => new SnArticleDto
                           {
                               Id = e.Id,
                               Title = e.Title,
                               Sketch = e.Sketch,
                               Give = e.Give,
                               Read = e.Read,
                               Img = e.Img,
                               TimeCreate = e.TimeCreate,
                               TimeModified = e.TimeModified,
                               User = e.User,
                               Sort = e.Sort,
                               Label = e.Label
                           }).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                           .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).Select(e => new SnArticleDto
                           {
                               Id = e.Id,
                               Title = e.Title,
                               Sketch = e.Sketch,
                               Give = e.Give,
                               Read = e.Read,
                               Img = e.Img,
                               TimeCreate = e.TimeCreate,
                               TimeModified = e.TimeModified,
                               User = e.User,
                               Sort = e.Sort,
                               Label = e.Label
                           }).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                           .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).Select(e => new SnArticleDto
                           {
                               Id = e.Id,
                               Title = e.Title,
                               Sketch = e.Sketch,
                               Give = e.Give,
                               Read = e.Read,
                               Img = e.Img,
                               TimeCreate = e.TimeCreate,
                               TimeModified = e.TimeModified,
                               User = e.User,
                               Sort = e.Sort,
                               Label = e.Label
                           }).AsNoTracking().ToListAsync());
                                    break;
                                case "read":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                           .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).Select(e => new SnArticleDto
                           {
                               Id = e.Id,
                               Title = e.Title,
                               Sketch = e.Sketch,
                               Give = e.Give,
                               Read = e.Read,
                               Img = e.Img,
                               TimeCreate = e.TimeCreate,
                               TimeModified = e.TimeModified,
                               User = e.User,
                               Sort = e.Sort,
                               Label = e.Label
                           }).AsNoTracking().ToListAsync());
                                    break;
                                case "give":
                                    datas.resultListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
                           .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).Select(e => new SnArticleDto
                           {
                               Id = e.Id,
                               Title = e.Title,
                               Sketch = e.Sketch,
                               Give = e.Give,
                               Read = e.Read,
                               Img = e.Img,
                               TimeCreate = e.TimeCreate,
                               TimeModified = e.TimeModified,
                               User = e.User,
                               Sort = e.Sort,
                               Label = e.Label
                           }).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;
                }
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }



        public async Task<bool> UpdatePortionAsync(SnArticle snArticle, string type)
        {
            _logger.LogInformation("SnArticle更新部分参数");
            var resulet = await _service.SnArticles.FindAsync(snArticle.Id);
            if (resulet == null) return false;
            switch (type)
            {    //指定字段进行更新操作
                case "Read":
                    //修改属性，被追踪的league状态属性就会变为Modify
                    resulet.Read = snArticle.Read;
                    break;
                case "Give":
                    resulet.Give = snArticle.Give;
                    break;
                case "Comment":
                    resulet.CommentId = snArticle.CommentId;
                    break;
            }
            //执行数据库操作
            return await _service.SaveChangesAsync() > 0;
        }


        public async Task<List<SnArticleDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {
            _logger.LogInformation(message: $"SnArticleDto模糊查询=>{type}{name}{cache}");
            datas.resultListDto = _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + type + name + cache, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                switch (identity)
                {
                    case 0:
                        datas.resultListDto = _mapper.Map<List<SnArticleDto>>(
                    await _service.SnArticles
                      .Where(l => l.Title.Contains(name))
                     .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        datas.resultListDto = _mapper.Map<List<SnArticleDto>>(
                      await _service.SnArticles
                       .Where(l => l.Title.Contains(name) && l.Sort.Name == type)
                       .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        datas.resultListDto = _mapper.Map<List<SnArticleDto>>(
                       await _service.SnArticles
                         .Where(l => l.Title.Contains(name) && l.Label.Name == type)
                         .AsNoTracking().ToListAsync());
                        break;
                    default:
                        _mapper.Map<List<SnArticleDto>>(
                    await _service.SnArticles
                      .Where(l => l.Title.Contains(name))
                     .AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + type + name + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }
    }
}
