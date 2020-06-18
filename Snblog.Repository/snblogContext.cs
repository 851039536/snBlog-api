

using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.Models;

namespace Snblog.Repository
{
    public partial class snblogContext : DbContext, IconcardContext
    {
        public snblogContext()
        {
        }

        public snblogContext(DbContextOptions<snblogContext> options)
            : base(options)
        {
        }



        public virtual DbSet<SnArticle> SnArticle { get; set; }
        public virtual DbSet<SnComments> SnComments { get; set; }
        public virtual DbSet<SnLabels> SnLabels { get; set; }
        public virtual DbSet<SnSort> SnSort { get; set; }
        public virtual DbSet<SnUser> SnUser { get; set; }
        public virtual DbSet<SnUserFriends> SnUserFriends { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseMySql("server=localhost;userid=root;pwd=woshishui;port=3306;database=snblog;sslmode=none", x => x.ServerVersion("8.0.16-mysql"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnArticle>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_article");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("varchar(20)")
                    .HasComment("评论")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Give)
                    .HasColumnName("give")
                    .HasColumnType("int(20)")
                    .HasComment("点赞");

                entity.Property(e => e.LabelId)
                    .HasColumnName("label_id")
                    .HasColumnType("int(11)")
                    .HasComment("标签");

                entity.Property(e => e.Read)
                    .HasColumnName("read")
                    .HasColumnType("int(20)")
                    .HasComment("阅读次数");

                entity.Property(e => e.SortId)
                    .HasColumnName("sort_id")
                    .HasColumnType("int(11)")
                    .HasComment("分类");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("text")
                    .HasComment("博客内容")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasComment("发表时间")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(30)")
                    .HasComment("标题 ")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TitleText)
                    .HasColumnName("title_text")
                    .HasColumnType("varchar(100)")
                    .HasComment("内容简述")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TypeTitle)
                    .HasColumnName("type_title")
                    .HasColumnType("varchar(20)")
                    .HasComment("分类标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UrlImg)
                    .HasColumnName("url_img")
                    .HasColumnType("varchar(50)")
                    .HasComment("图片")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("发表人id");
            });

            modelBuilder.Entity<SnComments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_comments");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasColumnType("int(11)")
                    .HasComment("评论主键");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .HasColumnType("int(11)")
                    .HasComment("评论博文id");

                entity.Property(e => e.CommentCount)
                    .HasColumnName("comment_count")
                    .HasColumnType("int(11)")
                    .HasComment("点赞数");

                entity.Property(e => e.CommentDate)
                    .HasColumnName("comment_date")
                    .HasColumnType("varchar(20)")
                    .HasComment("评论日期")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommentText)
                    .HasColumnName("comment_text")
                    .HasColumnType("varchar(255)")
                    .HasComment("内容")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ParentCommentId)
                    .HasColumnName("parent_comment_id")
                    .HasColumnType("int(11)")
                    .HasComment("父评论id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户id");
            });

            modelBuilder.Entity<SnLabels>(entity =>
            {
                entity.HasKey(e => e.LabelId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_labels");

                entity.Property(e => e.LabelId)
                    .HasColumnName("label_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LabelAlias)
                    .HasColumnName("label_alias")
                    .HasColumnType("varchar(20)")
                    .HasComment("标签别名")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LabelDescription)
                    .HasColumnName("label_description")
                    .HasColumnType("text")
                    .HasComment("标签描述")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LabelName)
                    .HasColumnName("label_name")
                    .HasColumnType("varchar(20)")
                    .HasComment("标签名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnSort>(entity =>
            {
                entity.HasKey(e => e.SortId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_sort");

                entity.Property(e => e.SortId)
                    .HasColumnName("sort_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ParentSortId)
                    .HasColumnName("parent_sort_id")
                    .HasColumnType("int(11)")
                    .HasComment("父分类id");

                entity.Property(e => e.SortAlias)
                    .HasColumnName("sort_alias")
                    .HasColumnType("varchar(50)")
                    .HasComment("分类别名")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SortDescription)
                    .HasColumnName("sort_description")
                    .HasColumnType("text")
                    .HasComment("分类描述")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SortName)
                    .HasColumnName("sort_name")
                    .HasColumnType("varchar(20)")
                    .HasComment("分类名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_user");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserBrief)
                    .HasColumnName("user_brief")
                    .HasColumnType("varchar(100)")
                    .HasComment("简介")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserEmail)
                    .HasColumnName("user_email")
                    .HasColumnType("varchar(30)")
                    .HasComment("邮箱")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserIp)
                    .HasColumnName("user_ip")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasColumnType("varchar(20)")
                    .HasComment("用户名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserNickname)
                    .HasColumnName("user_nickname")
                    .HasColumnType("varchar(20)")
                    .HasComment("称呼")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserPhoto)
                    .HasColumnName("user_photo")
                    .HasColumnType("varchar(255)")
                    .HasComment("头像")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserPwd)
                    .HasColumnName("user_pwd")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserTime)
                    .HasColumnName("user_time")
                    .HasColumnType("varchar(20)")
                    .HasComment("注册时间")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnUserFriends>(entity =>
            {
                entity.ToTable("sn_user_friends");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserFriendsId)
                    .HasColumnName("user_friends_id")
                    .HasColumnType("int(11)")
                    .HasComment("好友id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户id");

                entity.Property(e => e.UserNote)
                    .HasColumnName("user_note")
                    .HasColumnType("varchar(20)")
                    .HasComment("好友备注")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserStatus)
                    .HasColumnName("user_status")
                    .HasColumnType("varchar(20)")
                    .HasComment("好友状态")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
