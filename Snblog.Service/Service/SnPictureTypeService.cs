namespace Snblog.Service.Service
{
    public class SnPictureTypeService : ISnPictureTypeService
    {
        private readonly SnblogContext _service;//DB
        private readonly CacheUtil _cache;
        private int _resultInt;
        private List<SnPictureType> _retList;
        public SnPictureTypeService(SnblogContext service, ICacheUtil cache)
        {
            _service = service;
            _cache = (CacheUtil)cache;
        }


        public async Task<bool> AddAsync(SnPictureType entity)
        {
            await _service.SnPictureTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetAllAsync()
        {
            const string key = "SnPictureType_GetAllAsync";
            _retList = _cache.CacheString(key, _retList,true);
            if (_retList != null) return _retList;
            
            _retList = await _service.SnPictureTypes.ToListAsync();
            _cache.CacheString(key, _retList,true);
            return _retList;
        }

        public async Task<int> CountAsync()
        {
            _resultInt = _cache.CacheNumber1("SnPictureType_CountAsync", _resultInt);
            if (_resultInt != 0)
            {
                return _resultInt;
            }
            _resultInt = await _service.SnPictureTypes.CountAsync();
            _cache.CacheNumber1("SnPictureType_CountAsync", _resultInt);
            return _resultInt;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // _service.SnPictureType.Remove(Entity);
            //return await _service.SaveChangesAsync()>0;
            var todoItem = await _service.SnPictureTypes.FindAsync(id);
            _service.SnPictureTypes.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnPictureType> GetByIdAsync(int id)
        {
            SnPictureType pictureType = default;
            pictureType = _cache.CacheString1("SnPictureType_GetByIdAsync" + id, pictureType);
            if (pictureType != null)
            {
                return pictureType;
            }
            pictureType = await _service.SnPictureTypes.FindAsync(id);
            _cache.CacheString1("SnPictureType_GetByIdAsync" + id, pictureType);
            return pictureType;
        }

        public async Task<bool> UpdateAsync(SnPictureType entity)
        {
            _service.SnPictureTypes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            _retList = _cache.CacheString1("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, _retList);
            if (_retList != null)
            {
                return _retList;
            }
            _retList = await GetFyAll(pageIndex, pageSize, isDesc);
            _cache.CacheString1("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, _retList);
            return _retList;
        }

        private async Task<List<SnPictureType>> GetFyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnPictureTypes.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.SnPictureTypes.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public Task<List<SnPictureType>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            throw new NotImplementedException();
        }
    }
}
