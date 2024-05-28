
namespace Snblog.Enties.Models;

public partial class ArticleType
{
    public ArticleType()
    {
        Articles = new HashSet<Article>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Article> Articles { get; set; }
}