using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly Res<SnArticle> res = new();
        private readonly ResDto<SnArticleDto> resDto = new();
        private readonly IMapper _mapper;
        public SnArticleService(ICacheUtil cacheUtil, snblogContext coreDbContext, ILogger<SnArticleService> logger, IMapper mapper)
        {
            _service = coreDbContext;
            _cacheutil = (CacheUtil)cacheUtil;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation(message: $"SnArticle删除数据 id:{{id}}");
            SnArticle reslult = await _service.SnArticles.FindAsync(id);
            if (reslult == null)
            {
                return false;
            }

            _service.SnArticles.Remove(reslult);//删除单个
            _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }


        public async Task<List<SnArticleDto>> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation(message: $"SnArticleDto主键查询 =>id:{id} 缓存:{cache}");
            resDto.entityList = _cacheutil.CacheString("SnArticleDto_GetByIdAsync=>" + id + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Id == id).Include(i => i.User).Include(i => i.Label).Include(i => i.Sort).AsNoTracking().ToListAsync());
                _cacheutil.CacheString("SnArticleDto_GetByIdAsync=>" + id + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<List<SnArticleDto>> GetTypeAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation(message: $"SnArticle条件查询=>{type} 缓存:{cache}");
            resDto.entityList = _cacheutil.CacheString("GetTypeAsync_SnArticle" + type + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                if (identity == 1)
                {
                    resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => s.Sort.Name == type).AsNoTracking().ToListAsync());
                }
                else
                {
                    resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(s => s.Label.Name == type).AsNoTracking().ToListAsync());
                }

                _cacheutil.CacheString("GetTypeAsync_SnArticle" + type + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }


        public async Task<bool> AddAsync(SnArticle entity)
        {
            _logger.LogInformation("SnArticle添加:" + entity);
            entity.TimeCreate = DateTime.Now;
            entity.TimeModified = DateTime.Now;
            await _service.SnArticles.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(SnArticle entity)
        {
            _logger.LogInformation("SnArticle更新:" + entity);
            entity.TimeModified = DateTime.Now; //更新时间
            var res = await _service.SnArticles.Where(w => w.Id == entity.Id).Select(
                s => new
                {
                    s.TimeCreate,
                }
                ).AsNoTracking().ToListAsync();
            entity.TimeCreate = res[0].TimeCreate;  //赋值表示更新时间不变
            _service.SnArticles.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetCountAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation("SnArticle查询总数=>" + identity + cache);
            res.entityInt = _cacheutil.CacheNumber("Count_SnArticle" + identity + cache, res.entityInt, cache);
            if (res.entityInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        res.entityInt = await _service.SnArticles.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        res.entityInt = await _service.SnArticles.AsNoTracking().CountAsync(c => c.Sort.Name == type);
                        break;
                    case 2:
                        res.entityInt = await _service.SnArticles.AsNoTracking().CountAsync(c => c.Label.Name == type);
                        break;
                    case 3:
                        res.entityInt = await _service.SnArticles.AsNoTracking().CountAsync(c => c.User.Name == type);
                        break;
                }
                _cacheutil.CacheNumber("Count_SnArticle" + identity + cache, res.entityInt, cache);
            }
            return res.entityInt;
        }


        public async Task<List<SnArticleDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("SnArticleDto查询所有=>" + cache);
            resDto.entityList = _cacheutil.CacheString("GetAllAsync_SnArticleDto" + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Include(i => i.User).Include(i => i.Label).Include(i => i.Sort).AsNoTracking().ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnArticleDto" + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<int> GetSumAsync(string type, bool cache)
        {
            _logger.LogInformation("SnArticle统计[字段/阅读/点赞]数量" + type + cache);
            res.entityInt = _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, res.entityInt, cache);
            if (res.entityInt == 0)
            {
                res.entityInt = await GetSum(type);
                _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, res.entityInt, cache);
            }
            return res.entityInt;
        }

        private async Task<int> GetSum(string type)
        {
            _logger.LogInformation("读取总字数：" + type);
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                    List<short> read = await _service.SnArticles.Select(c => c.Read).ToListAsync();
                    foreach (short i in read)
                    {
                        num += i;
                    }
                    break;
                case "text":
                    List<string> text = await _service.SnArticles.Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    List<short> give = await _service.SnArticles.Select(c => c.Give).ToListAsync();
                    foreach (short i in give)
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
            resDto.entityList = _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, resDto.entityList, cache);

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
                        await GetFyTag(type, pageIndex, pageSize, ordering, isDesc);
                        break;
                }
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        private async Task GetFyTag(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Label.Name == type)
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
        }

        private async Task GetFyType(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles.Where(w => w.Sort.Name == type)
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
        }

        private async Task GetFyAll(int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
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
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(await _service.SnArticles
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
        }

        public async Task<bool> UpdatePortionAsync(SnArticle snArticle, string type)
        {
            _logger.LogInformation("SnArticle更新部分参数");
            SnArticle resulet = await _service.SnArticles.FindAsync(snArticle.Id);
            if (resulet == null)
            {
                return false;
            }

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
            _logger.LogInformation(message: $"SnArticleDto=>{identity}{type}{name}{cache}");
            resDto.entityList = _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + type + name + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                switch (identity)
                {
                    case 0:
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(
                    await _service.SnArticles
                      .Where(l => l.Title.Contains(name))
                     .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(
                      await _service.SnArticles
                       .Where(l => l.Title.Contains(name) && l.Sort.Name == type)
                       .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.entityList = _mapper.Map<List<SnArticleDto>>(
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
                _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + type + name + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }
    }
}
