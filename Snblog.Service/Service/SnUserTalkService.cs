namespace Snblog.Service
{
    public class SnUserTalkService : ISnUserTalkService
    {
        private readonly snblogContext _service;

        public SnUserTalkService(snblogContext service)
        {
            _service = service;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DelAsync(int id)
        {
            // 通过id查找文章
            var result = await _service.SnUserTalks.FindAsync(id);

            // 如果文章不存在，返回false
            if (result == null) return false;

            _service.SnUserTalks.Remove(result); //删除单个
            _service.Remove(result); //直接在context上Remove()方法传入model，它会判断类型

            // 保存更改
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnUserTalk>> GetAll()
        {
            var data = await _service.SnUserTalks.ToListAsync();
            return  data;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        public async Task<bool> AsyInsUserTalk(SnUserTalk entity)
        {
            _service.SnUserTalks.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }


        public async Task<bool> AysUpUserTalk(SnUserTalk entity)
        {
            _service.SnUserTalks.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        
    }
}