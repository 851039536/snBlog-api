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
    public class SnArticleService : ISnArticleService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly ILogger<SnArticleService> _logger;
        private int result_Int;
        private List<SnArticle> result_List = default;
        private SnArticleDto resultDto = default;
        private SnArticle result = default;
        private List<SnArticleDto> result_ListDto = default;
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
            _logger.LogInformation(message: $"SnArticle主键查询 id:{id} 缓存:{cache}");
            resultDto = _cacheutil.CacheString("GetByIdAsync_SnArticleDto" + id + cache, resultDto, cache);
            if (resultDto == null)
            {
                result = await _service.SnArticles.FindAsync(id);
                resultDto = _mapper.Map<SnArticleDto>(result);
                _cacheutil.CacheString("GetByIdAsync_SnArticleDto" + id + cache, resultDto, cache);
            }
            return resultDto;
        }

        public async Task<List<SnArticleDto>> GetTypeAsync(int identity, int type, bool cache)
        {
            _logger.LogInformation(message: $"SnArticle条件查询=>{type} 缓存:{cache}");
            result_List = _cacheutil.CacheString("GetTypeAsync_SnArticle" + type + cache, result_List, cache);
            if (result_List == null)
            {
                if (identity == 1)
                {
                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => s.SortId == type).AsNoTracking().ToListAsync());
                }
                else
                {
                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => s.LabelId == type).AsNoTracking().ToListAsync());
                }

                _cacheutil.CacheString("GetTypeAsync_SnArticle" + type + cache, result_List, cache);
            }
            return result_ListDto;
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

        public int GetTypeCountAsync(int type, bool cache)
        {
            _logger.LogInformation("SnArticle查询标签总数:" + type + cache);
            result_Int = _cacheutil.CacheNumber("GetTypeCountAsync_SnArticle" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = _service.SnArticles.Count(c => c.LabelId == type);
                _cacheutil.CacheNumber("GetTypeCountAsync_SnArticle" + type + cache, result_Int, cache);
            }
            return result_Int;
        }


        public async Task<int> GetCountAsync(int identity, int type, bool cache)
        {
            _logger.LogInformation("SnArticle查询总数,缓存:" + identity + cache);
            result_Int = _cacheutil.CacheNumber("Count_SnArticle" + identity + cache, result_Int, cache);
            if (result_Int == 0)
            {
                switch (identity)
                {
                    case 0:
                        result_Int = await _service.SnArticles.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        result_Int = await _service.SnArticles.AsNoTracking().CountAsync(c => c.SortId == type);
                        break;
                    case 2:
                        result_Int = await _service.SnArticles.AsNoTracking().CountAsync(c => c.LabelId == type);
                        break;
                    case 3:
                        result_Int = await _service.SnArticles.AsNoTracking().CountAsync(c => c.UserId == type);
                        break;
                }
                _cacheutil.CacheNumber("Count_SnArticle" + identity + cache, result_Int, cache);
            }
            return result_Int;
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
            result_ListDto = _mapper.Map<List<SnArticleDto>>(data);
            return result_ListDto;

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
            result_Int = _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await GetSum(type);
                _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, result_Int, cache);
            }
            return result_Int;
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

        public async Task<List<SnArticleDto>> GetFyAsync(int identity, int type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("SnArticle分页查询" + identity + pageIndex + pageSize + ordering + isDesc + cache);
            result_ListDto = _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, result_ListDto, cache); //设置缓存

            if (result_ListDto == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(
                            await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                                    result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Id == type)
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
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, result_ListDto, cache);
            }
            return result_ListDto;
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


        public async Task<List<SnArticleDto>> GetTypeContainsAsync(int type, string name, bool cache)
        {
            _logger.LogInformation(message: $"SnArticleDto模糊查询{type}{name}{cache}");
            result_ListDto = _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + type + name + cache, result_ListDto, cache);
            if (result_ListDto == null)
            {
                switch (type)
                {
                    case 0:
                        result_ListDto = _mapper.Map<List<SnArticleDto>>(
                       result_List = await _service.SnArticles
                       .Where(l => l.Title.Contains(name))
                      .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        result_ListDto = _mapper.Map<List<SnArticleDto>>(
                         result_List = await _service.SnArticles
                        .Where(l => l.Title.Contains(name) && l.SortId == type)
                        .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        result_ListDto = _mapper.Map<List<SnArticleDto>>(
                           result_List = await _service.SnArticles
                          .Where(l => l.Title.Contains(name) && l.LabelId == type)
                          .AsNoTracking().ToListAsync());
                        break;
                    default:
                        result_ListDto = _mapper.Map<List<SnArticleDto>>(
                     result_List = await _service.SnArticles
                     .Where(l => l.Title.Contains(name))
                    .AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + type + name + cache, result_ListDto, cache);
            }
            return result_ListDto;
        }
    }
}
