namespace Snblog.Service.Service
{
    public class UserTalkService : IUserTalkService
    {
        const string Name = "userTalk_";
        private readonly EntityData<UserTalk> _ret = new();
        private readonly EntityDataDto<UserTalkDto> _retDto = new();

        //服务
        private readonly SnblogContext _service;
        private readonly CacheUtil _cache;
        private readonly IMapper _mapper;

        public UserTalkService(SnblogContext service, ICacheUtil cache, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _cache = (CacheUtil)cache;
        }

        public async Task<List<UserTalkDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {
            var upNames = name.ToUpper();
            Common.CacheInfo($"{Name}{Common.Contains}{identity}_{type}_{name}_{cache}");

            if (cache)
            {
                _retDto.EntityList = _cache.GetValue<List<UserTalkDto>>(Common.CacheKey);
                if (_retDto.EntityList != null)
                {
                    return _retDto.EntityList;
                }
            }

            return identity switch
            {
                0 => await GetContainsAsync(l => l.Text.ToUpper().Contains(upNames)),
                1 => await GetContainsAsync(l => l.Text.ToUpper().Contains(upNames) && l.User.Name == type),
                _ => await GetContainsAsync(l => l.Text.ToUpper().Contains(upNames)),
            };
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|用户:1</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序规则 data:时间|id:主键</param>
        /// <returns>list-entity</returns>
        public async Task<List<UserTalkDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize,
            string ordering, bool isDesc, bool cache)
        {
            Common.CacheInfo(
                $"{Name}{Common.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}");

            if (cache)
            {
                _retDto.EntityList = _cache.GetValue<List<UserTalkDto>>(Common.CacheKey);
                if (_retDto.EntityList != null)
                {
                    return _retDto.EntityList;
                }
            }

            switch (identity)
            {
                case 0:
                    return await GetPaging(pageIndex, pageSize, ordering, isDesc);
                case 1:
                    return await GetPaging(pageIndex, pageSize, ordering, isDesc, w => w.User.Name == type);
                default:
                    return await GetPaging(pageIndex, pageSize, ordering, isDesc);
            }
        }

        private async Task<List<UserTalkDto>> GetPaging(int pageIndex, int pageSize, string ordering, bool isDesc,
            Expression<Func<UserTalk, bool>> predicate = null)
        {
            IQueryable<UserTalk> userTalks = _service.UserTalks.AsQueryable();

            // 查询条件,如果为空则无条件查询
            if (predicate != null)
            {
                userTalks = userTalks.Where(predicate);
            }

            switch (ordering)
            {
                case "id":
                    userTalks = isDesc ? userTalks.OrderByDescending(c => c.Id) : userTalks.OrderBy(c => c.Id);
                    break;
                case "data":
                    userTalks = isDesc
                        ? userTalks.OrderByDescending(c => c.TimeCreate)
                        : userTalks.OrderBy(c => c.TimeCreate);
                    break;
            }

            _retDto.EntityList = await userTalks.Skip((pageIndex - 1) * pageSize).Take(pageSize).SelectUserTalk()
                .ToListAsync();
            _cache.SetValue(Common.CacheKey, _retDto.EntityList);

            // _retDto.EntityList = _mapper.Map<List<UserTalkDto>>(data);
            return _retDto.EntityList;
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="predicate">筛选文章的条件</param>
        private async Task<List<UserTalkDto>> GetContainsAsync(Expression<Func<UserTalk, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _retDto.EntityList;
            }
            _retDto.EntityList = await _service.UserTalks.Where(predicate).SelectUserTalk().ToListAsync();
            _cache.SetValue(Common.CacheKey, _retDto.EntityList); //设置缓存
            return _retDto.EntityList;
        }


        public async Task<bool> DelAsync(int id)
        {
            Common.CacheInfo($"{Name}{Common.Del}{id}");
            var ret = await _service.UserTalks.FindAsync(id);
            if (ret == null) return false;
            _service.UserTalks.Remove(ret);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddAsync(UserTalk entity)
        {
            
            Common.CacheInfo($"{Name}{Common.Add}{entity}");
            entity.TimeCreate = DateTime.Now; 
            _service.UserTalks.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(UserTalk entity)
        {
            Common.CacheInfo($"{Name}{Common.Up}{entity}");

            _service.UserTalks.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}