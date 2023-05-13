using Microsoft.Extensions.Logging;

namespace Snblog.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly CacheUtil _cacheutil;
        private readonly snblogContext _service;
        private readonly ILogger<UserService> _logger;
        private int rInt;
        private UserDto rDto = default;
        private List<UserDto> rListDto = default;
        // 创建一个字段来存储mapper对象
        private readonly IMapper _mapper;

        const string NAME = "user_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string DEL = "DEL_";
        const string ADD = "ADD";
        const string UPDATE = "UPDATE_";
        public UserService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, snblogContext service, IMapper mapper, ILogger<UserService> logger, ICacheUtil cacheutil) : base(repositoryFactory, mydbcontext)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<int> DelAsync(int id)
        {
            Log.Information($"{NAME}{DEL}{id}");
            return await CreateService<User>().DelAsync(id);
        }

        public async Task<UserDto> GetByIdAsync(int id, bool cache)
        {
            Log.Information($"{NAME}{BYID}{id}_{cache}");
            rDto = _cacheutil.CacheString($"{NAME}{BYID}{id}_{cache}", rDto, cache);
            if (rDto == null)
            {
                rDto = _mapper.Map<UserDto>(await _service.Users.FindAsync(id));
                _cacheutil.CacheString($"{NAME}{BYID}{id}_{cache}", rDto, cache);
            }
            return rDto;
        }

        public async Task<int> AddAsync(User entity)
        {
            Log.Information($"{NAME}{ADD}{entity}");
            await _service.Users.AddAsync(entity);
            return await _service.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(UserDto entity)
        {
            Log.Information($"{NAME}{UPDATE}{entity}");
            var model = _mapper.Map<User>(entity);
            return await CreateService<User>().UpdateAsync(model);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        public async Task<List<UserDto>> GetPagingAsync(int pageIndex, int pageSize)
        {
            var data = await _service.Users.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return _mapper.Map<List<UserDto>>(data);
        }

        public async Task<int> GetSumAsync(bool cache)
        {
            Log.Information($"{NAME}{SUM}{cache}");
            rInt = _cacheutil.CacheString($"{NAME}{SUM}{cache}", rInt, cache);
            if (rInt == 0)
            {
                rInt = await _service.Users.CountAsync();
                _cacheutil.CacheString($"{NAME}{SUM}{cache}", rInt, cache);
            }
            return rInt;
        }

        public async Task<List<UserDto>> GetContainsAsync(string name, bool cache)
        {
            Log.Information( $"{NAME}{CONTAINS}{name}{cache}");
            rListDto = _cacheutil.CacheString($"{NAME}{CONTAINS}{name}{cache}", rListDto, cache);
            if (rListDto == null)
            {
                var res = await _service.Users.Where(u => u.Name.Contains(name) || u.Nickname.Contains(name)).AsNoTracking().ToListAsync();
                rListDto = _mapper.Map<List<UserDto>>(res);
                _cacheutil.CacheString($"{NAME}{CONTAINS}{name}{cache}", rListDto, cache);
            }
            return rListDto;
        }
    }
}
