
namespace Snblog.Service.Service
{
    public class DiaryTypeService : IDiaryTypeService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cache;
        private int _resultInt;
        private List<DiaryType> _resultList = null;

        const string NAME = "diaryType_";
        private string _cacheKey;
        public DiaryTypeService(snblogContext service, ICacheUtil cache)
        {
            _service = service;
            _cache = (CacheUtil)cache;
        }

        public async Task<bool> AddAsync(DiaryType entity)
        {
            Log.Information($"{NAME}{ConstantString.ADD}{entity}");
            
            await _service.DiaryTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSumAsync(bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.SUM}{cache}";
            Log.Information(_cacheKey);
            
            
            if (cache)
            {
                _resultInt = _cache.GetValue(_cacheKey, _resultInt);
                if (_resultInt != 0)return _resultInt;
                    
            }
            _resultInt = await _service.DiaryTypes.CountAsync();
            _cache.SetValue(_cacheKey, _resultInt);
            return _resultInt;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _cacheKey = $"{NAME}{ConstantString.DEL}{id}";
            Log.Information(_cacheKey);
            
            var ret = await _service.DiaryTypes.FindAsync(id);
            
            if (ret == null) return false;

            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<DiaryType> GetByIdAsync(int id, bool cache)
        {
            Log.Information("主键查询_SnOneType" + cache);
            DiaryType result = default;
            result = _cache.CacheString("GetByIdAsync_SnOneType" + id + cache, result, cache);
            if (result != null)
            {
                return result;
            }
            result = await _service.DiaryTypes.FindAsync(id);
            _cache.CacheString("GetByIdAsync_SnOneType" + id + cache, result, cache);
            return result;
        }

        public async Task<DiaryType> GetTypeAsync(int type, bool cache)
        {
            Log.Information("类别查询_SnOneType" + type + cache);
            DiaryType result = default;
            result = _cache.CacheString("GetTypeAsync_SnOneType" + type + cache, result, cache);
            if (result != null)
            {
                return result;
            }
            result = await _service.DiaryTypes.FirstAsync(s => s.Id == type);
            _cache.CacheString("GetTypeAsync_SnOneType" + type + cache, result, cache);
            return result;
        }

        public async Task<bool> UpdateAsync(DiaryType entity)
        {
            Log.Information("更新数据_SnOneType" + entity);
            _service.DiaryTypes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
