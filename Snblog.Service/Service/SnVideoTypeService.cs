namespace Snblog.Service
{
    public class SnVideoTypeService : BaseService, ISnVideoTypeService
    {
        private readonly snblogContext _coreDbContext;//DB
        public SnVideoTypeService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, snblogContext coreDbContext) : base(repositoryFactory, mydbcontext)
        {
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnVideoType Entity)
        {
            await _coreDbContext.SnVideoTypes.AddAsync(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnVideoType>> GetAll()
        {
            return await CreateService<SnVideoType>().GetAll().ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnVideoTypes.CountAsync();
        }

        public async Task<bool> DeleteAsync(SnVideoType Entity)
        {
            _coreDbContext.SnVideoTypes.Remove(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnVideoType>> GetAllAsync(int id)
        {
            //var data = from s in _coreDbContext.SnVideoType
            //           where s.VId == id
            //           select s;
            return await _coreDbContext.SnVideoTypes.Where(s => s.Id == id).ToListAsync();
        }

        public async Task<bool> UpdateAsync(SnVideoType Entity)
        {
            _coreDbContext.SnVideoTypes.Update(Entity);
            return await _coreDbContext.SaveChangesAsync() > 0;
        }
    }
}
