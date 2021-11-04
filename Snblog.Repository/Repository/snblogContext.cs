using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Snblog.Enties.Models;
using Snblog.IRepository;

#nullable disable

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

        public virtual DbSet<SnArticle> SnArticles { get; set; }
        public virtual DbSet<SnComment> SnComments { get; set; }
        public virtual DbSet<SnInterface> SnInterfaces { get; set; }
        public virtual DbSet<SnInterfaceType> SnInterfaceTypes { get; set; }
        public virtual DbSet<SnLabel> SnLabels { get; set; }
        public virtual DbSet<SnLeave> SnLeaves { get; set; }
        public virtual DbSet<SnNavigation> SnNavigations { get; set; }
        public virtual DbSet<SnNavigationType> SnNavigationTypes { get; set; }
        public virtual DbSet<SnOne> SnOnes { get; set; }
        public virtual DbSet<SnOneType> SnOneTypes { get; set; }
        public virtual DbSet<SnPicture> SnPictures { get; set; }
        public virtual DbSet<SnPictureType> SnPictureTypes { get; set; }
        public virtual DbSet<SnSetblog> SnSetblogs { get; set; }
        public virtual DbSet<SnSetblogType> SnSetblogTypes { get; set; }
        public virtual DbSet<SnSoftware> SnSoftwares { get; set; }
        public virtual DbSet<SnSoftwareType> SnSoftwareTypes { get; set; }
        public virtual DbSet<SnSort> SnSorts { get; set; }
        public virtual DbSet<SnTalk> SnTalks { get; set; }
        public virtual DbSet<SnTalkType> SnTalkTypes { get; set; }
        public virtual DbSet<SnUser> SnUsers { get; set; }
        public virtual DbSet<SnUserFriend> SnUserFriends { get; set; }
        public virtual DbSet<SnUserTalk> SnUserTalks { get; set; }
        public virtual DbSet<SnVideo> SnVideos { get; set; }
        public virtual DbSet<SnVideoType> SnVideoTypes { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<SnArticle>(entity =>
            {
                entity.ToTable("sn_article");

                entity.HasIndex(e => e.LabelId, "article_labelsId");

                entity.HasIndex(e => e.SortId, "article_sortId");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.CommentId)
                    .HasColumnType("smallint(8)")
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Give)
                    .HasColumnType("smallint(8)")
                    .HasColumnName("give")
                    .HasComment("点赞");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("img")
                    .HasComment("图片");

                entity.Property(e => e.LabelId)
                    .HasColumnType("int(5)")
                    .HasColumnName("label_id")
                    .HasComment("标签外键");

                entity.Property(e => e.Read)
                    .HasColumnType("smallint(8)")
                    .HasColumnName("read")
                    .HasComment("阅读次数");

                entity.Property(e => e.Sketch)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("sketch")
                    .HasComment("内容简述");

                entity.Property(e => e.SortId)
                    .HasColumnType("int(5)")
                    .HasColumnName("sort_id")
                    .HasComment("分类外键");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("text")
                    .HasComment("博客内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("发表时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified")
                    .HasComment("更新时间");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("标题 ");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(5)")
                    .HasColumnName("user_id")
                    .HasComment("用户外键id");

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.SnArticles)
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("abelsId");

                entity.HasOne(d => d.Sort)
                    .WithMany(p => p.SnArticles)
                    .HasForeignKey(d => d.SortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sortId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnArticles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userId");
            });

            modelBuilder.Entity<SnComment>(entity =>
            {
                entity.ToTable("sn_comments");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("评论主键");

                entity.Property(e => e.Give)
                    .HasColumnType("int(11)")
                    .HasColumnName("give")
                    .HasComment("点赞数");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("text")
                    .HasComment("内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("评论日期");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
                    .HasComment("用户id");
            });

            modelBuilder.Entity<SnInterface>(entity =>
            {
                entity.ToTable("sn_interface");

                entity.HasIndex(e => e.TypeId, "type_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Identity)
                    .HasColumnName("identity")
                    .HasComment("显示隐藏");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("path")
                    .HasComment("路径");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(5)")
                    .HasColumnName("type_id")
                    .HasComment("类别");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(5)")
                    .HasColumnName("user_id")
                    .HasComment("用户");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnInterfaces)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnInterfaces)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sn_interface_ibfk_2");
            });

            modelBuilder.Entity<SnInterfaceType>(entity =>
            {
                entity.ToTable("sn_interface_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnLabel>(entity =>
            {
                entity.ToTable("sn_labels");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("description")
                    .HasComment("标签描述");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .HasComment("标签名称");
            });

            modelBuilder.Entity<SnLeave>(entity =>
            {
                entity.ToTable("sn_leave");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("text")
                    .HasComment("留言内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("发布时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
                    .HasComment("用户外键");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnLeaves)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_id");
            });

            modelBuilder.Entity<SnNavigation>(entity =>
            {
                entity.ToTable("sn_navigation");

                entity.HasIndex(e => e.TypeId, "nav_type_id");

                entity.HasIndex(e => e.UserId, "nav_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("describe")
                    .HasComment("标题描述");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("img")
                    .HasComment("图片路径");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("title")
                    .HasComment("导航标题");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("url")
                    .HasComment("链接路径");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
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

            modelBuilder.Entity<SnNavigationType>(entity =>
            {
                entity.ToTable("sn_navigation_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .HasComment("标题");
            });

            modelBuilder.Entity<SnOne>(entity =>
            {
                entity.ToTable("sn_one");

                entity.HasIndex(e => e.UserId, "one_user_id");

                entity.HasIndex(e => e.TypeId, "sn_one_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.CommentId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Give)
                    .HasColumnType("int(11)")
                    .HasColumnName("give")
                    .HasComment("点赞");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("img")
                    .HasComment("图片");

                entity.Property(e => e.Read)
                    .HasColumnType("int(11)")
                    .HasColumnName("read")
                    .HasComment("阅读数");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("text")
                    .HasComment("内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
                    .HasComment("作者");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnOnes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("one_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnOnes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("one_user_id");
            });

            modelBuilder.Entity<SnOneType>(entity =>
            {
                entity.ToTable("sn_one_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<SnPicture>(entity =>
            {
                entity.ToTable("sn_picture");

                entity.HasIndex(e => e.UserId, "pivture_user_id");

                entity.HasIndex(e => e.TypeId, "prcture_type_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

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
                    .HasColumnType("int(11)")
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

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

            modelBuilder.Entity<SnPictureType>(entity =>
            {
                entity.ToTable("sn_picture_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasComment("分类名称");
            });

            modelBuilder.Entity<SnSetblog>(entity =>
            {
                entity.ToTable("sn_setblog");

                entity.HasIndex(e => e.TypeId, "setblog_type_id");

                entity.HasIndex(e => e.UserId, "setblog_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

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
                    .HasColumnType("int(5)")
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(5)")
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

            modelBuilder.Entity<SnSetblogType>(entity =>
            {
                entity.ToTable("sn_setblog_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnSoftware>(entity =>
            {
                entity.ToTable("sn_software");

                entity.HasIndex(e => e.TypeId, "software_type_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CommentId)
                    .HasColumnType("int(255)")
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Img)
                    .HasMaxLength(200)
                    .HasColumnName("img")
                    .HasComment("图片路径");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnSoftwares)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("software_type_id");
            });

            modelBuilder.Entity<SnSoftwareType>(entity =>
            {
                entity.ToTable("sn_software_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnSort>(entity =>
            {
                entity.ToTable("sn_sort");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description")
                    .HasComment("分类描述");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .HasComment("分类名称");
            });

            modelBuilder.Entity<SnTalk>(entity =>
            {
                entity.ToTable("sn_talk");

                entity.HasIndex(e => e.TypeId, "sn_talk_typeId");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CommentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("comment_id")
                    .HasComment("评论");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("describe")
                    .HasComment("简介");

                entity.Property(e => e.Give)
                    .HasColumnType("int(11)")
                    .HasColumnName("give")
                    .HasComment("点赞");

                entity.Property(e => e.Read)
                    .HasColumnType("int(11)")
                    .HasColumnName("read")
                    .HasComment("阅读量");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("text")
                    .HasComment("内容");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("发表时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("type_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
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

            modelBuilder.Entity<SnTalkType>(entity =>
            {
                entity.ToTable("sn_talk_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SnUser>(entity =>
            {
                entity.ToTable("sn_user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Brief)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("brief")
                    .HasComment("简介");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("email")
                    .HasComment("邮箱");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ip")
                    .HasComment("ip地址");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .HasComment("用户名称");

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
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("注册时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<SnUserFriend>(entity =>
            {
                entity.ToTable("sn_user_friends");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.UserFriendsId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_friends_id")
                    .HasComment("好友id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
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

            modelBuilder.Entity<SnUserTalk>(entity =>
            {
                entity.ToTable("sn_user_talk");

                entity.HasIndex(e => e.UserId, "sn_user_talk_userId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CommentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("comment_id")
                    .HasComment("评论id");

                entity.Property(e => e.TalkGive)
                    .HasColumnType("int(11)")
                    .HasColumnName("talk_give");

                entity.Property(e => e.TalkRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("talk_read");

                entity.Property(e => e.TalkText)
                    .HasMaxLength(255)
                    .HasColumnName("talk_text")
                    .HasComment("说说内容");

                entity.Property(e => e.TalkTime)
                    .HasColumnType("date")
                    .HasColumnName("talk_time")
                    .HasComment("发表时间");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnUserTalks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("sn_user_talk_userId");
            });

            modelBuilder.Entity<SnVideo>(entity =>
            {
                entity.ToTable("sn_video");

                entity.HasIndex(e => e.TypeId, "video_type_id");

                entity.HasIndex(e => e.UserId, "video_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("img")
                    .HasComment("图片");

                entity.Property(e => e.TimeCreate)
                    .HasColumnType("date")
                    .HasColumnName("time_create")
                    .HasComment("时间");

                entity.Property(e => e.TimeModified)
                    .HasColumnType("date")
                    .HasColumnName("time_modified");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("type_id")
                    .HasComment("分类");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("url")
                    .HasComment("链接路径");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SnVideos)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("video_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SnVideos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("video_user_id");
            });

            modelBuilder.Entity<SnVideoType>(entity =>
            {
                entity.ToTable("sn_video_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
