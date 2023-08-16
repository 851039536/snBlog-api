namespace Snblog.Enties.Models
{
    public partial class DiaryType
    {
        public DiaryType()
        {
            Diaries = new HashSet<Diary>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Diary> Diaries { get; set; }
    }
}
