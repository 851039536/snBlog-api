namespace Snblog.Service
{
    public class SnVideoTypeService : ISnVideoTypeService
    {
        private readonly snblogContext _service;
        public SnVideoTypeService(snblogContext service) 
        {
            _service = service;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnVideoType Entity)
        {
            await _service.SnVideoTypes.AddAsync(Entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnVideoType>> GetAll()
        {
            return await _service.SnVideoTypes.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _service.SnVideoTypes.CountAsync();
        }

        public async Task<bool> DeleteAsync(SnVideoType Entity)
        {
            _service.SnVideoTypes.Remove(Entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnVideoType>> GetAllAsync(int id)
        {
            //var data = from s in _coreDbContext.SnVideoType
            //           where s.VId == id
            //           select s;
            return await _service.SnVideoTypes.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<bool> UpdateAsync(SnVideoType Entity)
        {
            _service.SnVideoTypes.Update(Entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
