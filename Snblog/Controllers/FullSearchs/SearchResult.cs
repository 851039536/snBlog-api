namespace Snblog.Controllers.FullSearchs
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Source { get; set; } // 添加来源字段
        public string Content { get; set; }
    
    }
}