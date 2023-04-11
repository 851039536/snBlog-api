using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class InterfaceService : IInterfaceService
    {
        private readonly ILogger<InterfaceService> _logger;
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly IMapper _mapper;
        private readonly Dto<InterfaceDto> resDto = new();
        const string NAME = "Interface_";
        const string BYID = "BYID_";
        const string Condition = "Condition";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public InterfaceService(snblogContext service, ICacheUtil cacheutil, IMapper mapper, ILogger<InterfaceService> logger = null)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">用户&分类: 0 | 用户: 1 | 分类: 2</param>
        /// <param name="userName">用户名称</param>
        /// <param name="type">类别</param>
        /// <param name="cache">缓存</param>
        public async Task<List<InterfaceDto>> GetConditionAsync(int identity, string userName, string type, bool cache)
        {
            _logger.LogInformation($"{NAME}{Condition}{identity}_{userName}_{type}_{cache}");
            resDto.eList = _cacheutil.CacheString($"{NAME}{Condition}{identity}_{userName}_{type}_{cache}", resDto.eList, cache);
            if (resDto.eList == null)
            {
                switch (identity)
                {
                    case 0:
                        resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(s => s.Type.Name == type && s.User.Name == userName).AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(s => s.User.Name == userName).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString($"{NAME}{Condition}{identity}_{userName}_{type}_{cache}", resDto.eList, cache);
            }
            return resDto.eList;
        }

        //public async Task<List<InterfaceDto>> GetAllAsync(bool cache)
        //{
        //    resDto.entityList = _cacheutil.CacheString("GetAllAsync_SnInterface", resDto.entityList, cache);
        //    if (resDto.entityList == null)
        //    {
        //        resDto.entityList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Include(i => i.Type).Include(i => i.User).AsNoTracking().ToListAsync());
        //        _cacheutil.CacheString("GetAllAsync_SnInterface", resDto.entityList, cache);
        //    }
        //    return resDto.entityList;
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有: 0 | 分类: 1 | 用户名: 2 |  用户-分类: 3</param>
        /// <param name="type">类别参数, identity为0时可为空(null) 多条件以','分割</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序条件[按id排序]</param>
        /// <returns>list-entity</returns>
        public async Task<List<InterfaceDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation($"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}");
            resDto.eList = _cacheutil.CacheString($"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}", resDto.eList, cache);
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
                                    resDto.eList = _mapper.Map<List<InterfaceDto>>(
                            await _service.Interfaces.Where(s => true).Include(i => i.Type).Include(i => i.User)
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
                                    resDto.eList = _mapper.Map<List<InterfaceDto>>(
                            await _service.Interfaces.Where(s => true).Include(i => i.Type).Include(i => i.User)
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
                                    resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(w => w.Type.Name == type).Include(i => i.Type).Include(i => i.User)
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
                                    resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(w => w.Type.Name == type).Include(i => i.Type).Include(i => i.User)
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
                                    resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
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
                                    resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;

                    case 3:

                    string[] sName =  type.Split(',');
                    if (isDesc)//降序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                            resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(w => w.User.Name == sName[0] && w.Type.Name == sName[1]).Include(i => i.Type).Include(i => i.User)
                    .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                        }
                    } else //升序
                      {
                        switch (ordering) //排序
                        {
                            case "id":
                            resDto.eList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces.Where(w => w.User.Name == sName[0] && w.Type.Name == sName[1]).Include(i => i.Type).Include(i => i.User)
                     .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                     .Take(pageSize).AsNoTracking().ToListAsync());
                            break;
                        }
                    }
                    break;
                }
                _cacheutil.CacheString($"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}", resDto.eList, cache);
            }
            return resDto.eList;
        }

        public async Task<bool> AddAsync(Interface entity)
        {
            _logger.LogInformation($"{NAME}{ADD}");
            await  _service.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Interface entity)
        {
            _logger.LogInformation($"{NAME}{UP}_{entity}");
            _service.Interfaces.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation($"{NAME}{DEL}_{id}");
            var reslult = await _service.Interfaces.FindAsync(id);
            if (reslult == null) return false;
            _service.Interfaces.Remove(reslult);//删除单个
            _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }


        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<InterfaceDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation($"{NAME}{BYID}{id}_{cache}");
            resDto.entity = _cacheutil.CacheString($"{NAME}{BYID}{id}_{cache}", resDto.entity, cache);
            if (resDto.entity == null)
                resDto.entity = _mapper.Map<InterfaceDto>(await _service.Interfaces.FindAsync(id));
                _cacheutil.CacheString($"{NAME}{BYID}{id}_{cache}", resDto.entity, cache);
            return resDto.entity;
        }
    }
}
