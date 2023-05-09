using Microsoft.EntityFrameworkCore;
using Snblog.Enties.Models;
using Snblog.IRepository;


namespace Snblog.Repository.Repository
{
    public partial class snblogContext : DbContext, IConcardContext
    {
        public snblogContext()
        {
        }

        public snblogContext(DbContextOptions<snblogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleTag> ArticleTags { get; set; }
        public virtual DbSet<ArticleType> ArticleTypes { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<DiaryType> DiaryTypes { get; set; }
        public virtual DbSet<Interface> Interfaces { get; set; }
        public virtual DbSet<InterfaceType> InterfaceTypes { get; set; }
        public virtual DbSet<SnComment> SnComments { get; set; }
        public virtual DbSet<SnLeave> SnLeaves { get; set; }
        public virtual DbSet<SnNavigation> SnNavigations { get; set; }
        public virtual DbSet<SnNavigationType> SnNavigationTypes { get; set; }
        public virtual DbSet<SnPicture> SnPictures { get; set; }
        public virtual DbSet<SnPictureType> SnPictureTypes { get; set; }
        public virtual DbSet<SnSetblog> SnSetblogs { get; set; }
        public virtual DbSet<SnSetblogType> SnSetblogTypes { get; set; }
        public virtual DbSet<SnSoftware> SnSoftwares { get; set; }
        public virtual DbSet<SnSoftwareType> SnSoftwareTypes { get; set; }
        public virtual DbSet<SnTalk> SnTalks { get; set; }
        public virtual DbSet<SnTalkType> SnTalkTypes { get; set; }
        public virtual DbSet<SnUserFriend> SnUserFriends { get; set; }
        public virtual DbSet<SnUserTalk> SnUserTalks { get; set; }
        public virtual DbSet<SnVideoType> SnVideoTypes { get; set; }
        public virtual DbSet<Snippet> Snippets { get; set; }
        public virtual DbSet<SnippetLabel> SnippetLabels { get; set; }
        public virtual DbSet<SnippetTag> SnippetTags { get; set; }
        public virtual DbSet<SnippetType> SnippetTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Video> Videos { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured) {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=localhost;userid=root;pwd=woshishui;port=3306;database=snblog;sslmode=none",ServerVersion.Parse("8.0.33-mysql"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            modelBuilder.Entity<Article>(entity => {
                entity.ToTable("article");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.TagId,"article_labelsId");

                entity.HasIndex(e => e.TypeId,"article_sortId");

                entity.HasIndex(e => e.UserId,"user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Give)
                    .HasColumnName("give")
                    .HasComment("点赞");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("img")
                    .HasComment("图片");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("标题 ");

                entity.Property(e => e.Read)
                    .HasColumnName("read")
                    .HasComment("阅读次数");

                entity.Property(e => e.Sketch)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("sketch")
                    .HasComment("内容简述");

                entity.Property(e => e.TagId)
                    .HasColumnName("tag_id")
                    .HasComment("标签外键");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("text")
                    .HasComment("博客内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create")
                    .HasComment("发表时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified")
                    .HasComment("更新时间");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("分类外键");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户外键id");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_ibfk_1");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tyoeId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userId");
            });

            modelBuilder.Entity<ArticleTag>(entity => {
                entity.ToTable("article_tag");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("description")
                    .HasComment("标签描述");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .HasComment("标签名称");
            });

            modelBuilder.Entity<ArticleType>(entity => {
                entity.ToTable("article_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("mediumtext")
                    .HasColumnName("description")
                    .HasComment("分类描述");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .HasComment("分类名称");
            });

            modelBuilder.Entity<Diary>(entity => {
                entity.ToTable("diary");
                entity.HasComment("日记表")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.UserId,"one_user_id");

                entity.HasIndex(e => e.TypeId,"sn_one_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Give)
                    .HasColumnName("give")
                    .HasComment("点赞");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("img")
                    .HasComment("图片");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("标题");

                entity.Property(e => e.Read)
                    .HasColumnName("read")
                    .HasComment("阅读数");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("text")
                    .HasComment("内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create")
                    .HasComment("时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("作者");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Diaries)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("one_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Diaries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("one_user_id");
            });

            modelBuilder.Entity<DiaryType>(entity => {
                entity.ToTable("diary_type");

                entity.HasComment("日记分类")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Interface>(entity => {
                entity.ToTable("interface");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.TypeId,"type_id");

                entity.HasIndex(e => e.UserId,"user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Identity)
                    .HasColumnName("identity")
                    .HasComment("显示隐藏");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasComment("标题");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("path")
                    .HasComment("路径");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("类别");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Interfaces)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interfaces)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interface_ibfk_2");
            });

            modelBuilder.Entity<InterfaceType>(entity => {
                entity.ToTable("interface_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnComment>(entity => {
                entity.ToTable("sn_comments");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("评论主键");

                entity.Property(e => e.Give)
                    .HasColumnName("give")
                    .HasComment("点赞数");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("text")
                    .HasComment("内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create")
                    .HasComment("评论日期");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户id");
            });

            modelBuilder.Entity<SnLeave>(entity => {
                entity.ToTable("sn_leave");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.UserId,"user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("text")
                    .HasComment("留言内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create")
                    .HasComment("发布时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户外键");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnLeaves)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_id");
            });

            modelBuilder.Entity<SnNavigation>(entity => {
                entity.ToTable("sn_navigation");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.TypeId,"nav_type_id");

                entity.HasIndex(e => e.UserId,"nav_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("describe")
                    .HasComment("标题描述");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("img")
                    .HasComment("图片路径");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("title")
                    .HasComment("导航标题");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("url")
                    .HasComment("链接路径");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnNavigations)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nav_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnNavigations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nav_user_id");
            });

            modelBuilder.Entity<SnNavigationType>(entity => {
                entity.ToTable("sn_navigation_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .HasComment("标题");
            });

            modelBuilder.Entity<SnPicture>(entity => {
                entity.ToTable("sn_picture");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.UserId,"pivture_user_id");

                entity.HasIndex(e => e.TypeId,"prcture_type_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImgUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("img_url")
                    .HasComment("图片地址");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("图床名");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnPictures)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("prcture_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnPictures)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pivture_user_id");
            });

            modelBuilder.Entity<SnPictureType>(entity => {
                entity.ToTable("sn_picture_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasComment("分类名称");
            });

            modelBuilder.Entity<SnSetblog>(entity => {
                entity.ToTable("sn_setblog");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.TypeId,"setblog_type_id");

                entity.HasIndex(e => e.UserId,"setblog_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Isopen)
                    .HasColumnName("isopen")
                    .HasComment("是否启用");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasComment("设置的内容名称");

                entity.Property(e => e.RouterUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("router_url")
                    .HasComment("路由链接");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("关联用户表");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnSetblogs)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("setblog_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnSetblogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("setblog_user_id");
            });

            modelBuilder.Entity<SnSetblogType>(entity => {
                entity.ToTable("sn_setblog_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnSoftware>(entity => {
                entity.ToTable("sn_software");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.TypeId,"software_type_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Img)
                    .HasMaxLength(200)
                    .HasColumnName("img")
                    .HasComment("图片路径");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create")
                    .HasComment("时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnSoftwares)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("software_type_id");
            });

            modelBuilder.Entity<SnSoftwareType>(entity => {
                entity.ToTable("sn_software_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnTalk>(entity => {
                entity.ToTable("sn_talk");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.TypeId,"sn_talk_typeId");

                entity.HasIndex(e => e.UserId,"user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("describe")
                    .HasComment("简介");

                entity.Property(e => e.Give)
                    .HasColumnName("give")
                    .HasComment("点赞");

                entity.Property(e => e.Read)
                    .HasColumnName("read")
                    .HasComment("阅读量");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("text")
                    .HasComment("内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create")
                    .HasComment("发表时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnTalks)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("talk_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnTalks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lalk_user_id");
            });

            modelBuilder.Entity<SnTalkType>(entity => {
                entity.ToTable("sn_talk_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnUserFriend>(entity => {
                entity.ToTable("sn_user_friends");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserFriendsId)
                    .HasColumnName("user_friends_id")
                    .HasComment("好友id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户id");

                entity.Property(e => e.UserNote)
                    .HasMaxLength(20)
                    .HasColumnName("user_note")
                    .HasComment("好友备注");

                entity.Property(e => e.UserStatus)
                    .HasMaxLength(20)
                    .HasColumnName("user_status")
                    .HasComment("好友状态");
            });

            modelBuilder.Entity<SnUserTalk>(entity => {
                entity.ToTable("sn_user_talk");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.UserId,"sn_user_talk_userId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasComment("评论id");

                entity.Property(e => e.TalkGive).HasColumnName("talk_give");

                entity.Property(e => e.TalkRead).HasColumnName("talk_read");

                entity.Property(e => e.TalkText)
                    .HasMaxLength(255)
                    .HasColumnName("talk_text")
                    .HasComment("说说内容");

                entity.Property(e => e.TalkTime)
                    .HasColumnType("date")
                    .HasColumnName("talk_time")
                    .HasComment("发表时间");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnUserTalks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("sn_user_talk_userId");
            });

            modelBuilder.Entity<SnVideoType>(entity => {
                entity.ToTable("sn_video_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Snippet>(entity => {
                entity.ToTable("snippet");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.LabelId,"label");

                entity.HasIndex(e => e.TagId,"tagid");

                entity.HasIndex(e => e.TypeId,"typeid");

                entity.HasIndex(e => e.UserId,"uid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LabelId).HasColumnName("label_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.Text)
                    .HasColumnType("mediumtext")
                    .HasColumnName("text");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.Snippets)
                    .HasForeignKey(d => d.LabelId)
                    .HasConstraintName("label");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Snippets)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("tagid");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Snippets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("typeid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Snippets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("uid");
            });

            modelBuilder.Entity<SnippetLabel>(entity => {
                entity.ToTable("snippet_label");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.Name,"name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnippetTag>(entity => {
                entity.ToTable("snippet_tag");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnippetType>(entity => {
                entity.ToTable("snippet_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("user");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Brief)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("brief")
                    .HasComment("简介");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("email")
                    .HasComment("邮箱");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("ip")
                    .HasComment("ip地址");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .HasComment("账号");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nickname")
                    .HasComment("称呼");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("photo")
                    .HasComment("头像");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("pwd")
                    .HasComment("密码");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("timestamp")
                    .HasColumnName("time_create")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("注册时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("time_modified")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<Video>(entity => {
                entity.ToTable("video");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_bin");

                entity.HasIndex(e => e.TypeId,"video_type_id");

                entity.HasIndex(e => e.UserId,"video_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("img")
                    .HasComment("图片");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasComment("标题");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("time_create")
                    .HasComment("时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("datetime")
                    .HasColumnName("time_modified");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("url")
                    .HasComment("链接路径");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("video_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("video_user_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
