using Snblog.IService.IReService;

namespace Snblog.Service.ReService
{
    public class ReSnNavigationService : BaseService, IReSnNavigationService
    {
        private readonly CacheUtil _cache;
        private int _resultInt;
        private List<SnNavigation> _resultList = null;
        private SnNavigation _resultModel = null;
        public ReSnNavigationService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, ICacheUtil cache) : base(repositoryFactory, mydbcontext)
        {
            _cache = (CacheUtil)cache;
        }

        public async Task<List<SnNavigation>> GetAllAsync()
        {
            _resultList = _cache.CacheString1("ReGetAllAsync", _resultList);
            if (_resultList == null)
            {
                _resultList = await CreateService<SnNavigation>().GetAllAsync();
                _cache.CacheString1("ReGetAllAsync", _resultList);
            }
            return _resultList;
        }

        /// <summary>
        /// BYID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SnNavigation> GetByIdAsync(int id)
        {
            _resultModel = _cache.CacheString1("ReGetByIdAsync" + id, _resultModel);
            if (_resultModel == null)
            {
                _resultModel = await CreateService<SnNavigation>().GetByIdAsync(id);
                _cache.CacheString1("ReGetByIdAsync" + id, _resultModel);
            }
            return _resultModel;
        }

        /// <summary>
        /// SUM
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            _resultInt = _cache.CacheNumber1("ReGetCountAsync", _resultInt);
            if (_resultInt == 0)
            {
                _resultInt = await CreateService<SnNavigation>().CountAsync();
                _cache.CacheNumber1("ReGetCountAsync", _resultInt);
            }
            return _resultInt;
        }

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> CountTypeAsync(string type)
        {
            _resultInt = _cache.CacheNumber1("ReCountTypeAsync", _resultInt);
            if (_resultInt == 0)
            {
                _resultInt = await CreateService<SnNavigation>().CountAsync(c => c.Type.Title == type);
                _cache.CacheNumber1("ReCountTypeAsync", _resultInt);
            }
            return _resultInt;
        }

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        public async Task<List<SnNavigation>> GetDistinct(string type)
        {
            _resultList = _cache.CacheString1("ReGetDistinct" + type, _resultList);
            if (_resultList == null)
            {
                _resultList = await CreateService<SnNavigation>().Distinct(s => s.Type.Title == type).ToListAsync();
                _cache.CacheString1("ReGetDistinct" + type, _resultList);
            }
            return _resultList;

        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="order">排序</param>
        /// <returns>List</returns>
        public async Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order)
        {
             _resultList = _cache.CacheString1("ReGetTypeOrderAsync" + type + order, _resultList);
            if (_resultList == null)
            {
                _resultList = await CreateService<SnNavigation>().Where(c => c.Type.Title == type, s => s.Id, order).ToListAsync();
                _cache.CacheString1("ReGetTypeOrderAsync" + type + order, _resultList);
            }
            return _resultList;
           
        }

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        public async Task<List<SnNavigation>> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc)
        {
           _resultList = _cache.CacheString1("ReGetFyAllAsync" + type + pageIndex + pageSize + isDesc, _resultList);
            if (_resultList == null)
            {
                 _resultList= await FyAll(type, pageIndex, pageSize, isDesc);
                _cache.CacheString1("ReGetFyAllAsync" + type + pageIndex + pageSize + isDesc ,_resultList);
            }
            return _resultList;
        }

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        public async Task<SnNavigation> AddAsync(SnNavigation entity)
        {
            return await CreateService<SnNavigation>().AddAsync(entity) ;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SnNavigation entity)
        {
            return  await CreateService<SnNavigation>().UpdateAsync(entity)>0;
        }

        /// <summary>
        /// 按id删除
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            return  await CreateService<SnNavigation>().DelAsync(id)>0;
        }

        private async Task<List<SnNavigation>> FyAll(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == "all")
            {
                var data = await CreateService<SnNavigation>().WherePageAsync(s => s.Type.Title != null, c => c.Id, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
            else
            {
                var data = await CreateService<SnNavigation>().WherePageAsync(s => s.Type.Title == type, c => c.Id, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
        }
    }
}
