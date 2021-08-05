using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;

namespace Snblog.Models
{
    public partial class SnblogContext : DbContext, IConcardContext
    {
        public SnblogContext()
        {
        }

        public SnblogContext(DbContextOptions<SnblogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SnArticle> SnArticle { get; set; }
        public virtual DbSet<SnComments> SnComments { get; set; }
        public virtual DbSet<SnInterface> SnInterface { get; set; }
        public virtual DbSet<SnInterfaceType> SnInterfaceType { get; set; }
        public virtual DbSet<SnLabels> SnLabels { get; set; }
        public virtual DbSet<SnLeave> SnLeave { get; set; }
        public virtual DbSet<SnNavigation> SnNavigation { get; set; }
        public virtual DbSet<SnNavigationType> SnNavigationType { get; set; }
        public virtual DbSet<SnOne> SnOne { get; set; }
        public virtual DbSet<SnOneType> SnOneType { get; set; }
        public virtual DbSet<SnPicture> SnPicture { get; set; }
        public virtual DbSet<SnPictureType> SnPictureType { get; set; }
        public virtual DbSet<SnSoftware> SnSoftware { get; set; }
        public virtual DbSet<SnSoftwareType> SnSoftwareType { get; set; }
        public virtual DbSet<SnSort> SnSort { get; set; }
        public virtual DbSet<SnTalk> SnTalk { get; set; }
        public virtual DbSet<SnTalkType> SnTalkType { get; set; }
        public virtual DbSet<SnUser> SnUser { get; set; }
        public virtual DbSet<SnUserFriends> SnUserFriends { get; set; }
        public virtual DbSet<SnUserTalk> SnUserTalk { get; set; }
        public virtual DbSet<SnVideo> SnVideo { get; set; }
        public virtual DbSet<SnVideoType> SnVideoType { get; set; }

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

                entity.HasIndex(e => e.LabelId, "article_labelsId");

                entity.HasIndex(e => e.SortId, "article_sortId");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.ArticleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("article_id")
                    .HasComment("主键");

                entity.Property(e => e.Comment)
                    .HasColumnType("smallint(8)")
                    .HasColumnName("comment")
                    .HasComment("评论");

                entity.Property(e => e.Give)
                    .HasColumnType("smallint(8)")
                    .HasColumnName("give")
                    .HasComment("点赞");

                entity.Property(e => e.LabelId)
                    .HasColumnType("int(11)")
                    .HasColumnName("label_id")
                    .HasComment("标签外键");

                entity.Property(e => e.Read)
                    .HasColumnType("smallint(8)")
                    .HasColumnName("read")
                    .HasComment("阅读次数");

                entity.Property(e => e.SortId)
                    .HasColumnType("int(11)")
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
                    .HasComment("修改时间");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("标题 ");

                entity.Property(e => e.TitleText)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("title_text")
                    .HasComment("内容简述");

                entity.Property(e => e.TypeTitle)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("type_title")
                    .HasComment("分类标题");

                entity.Property(e => e.UrlImg)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("url_img")
                    .HasComment("图片");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(5)")
                    .HasColumnName("user_id")
                    .HasComment("用户外键id");

                //entity.HasOne(d => d.Label)
                //    .WithMany(p => p.SnArticles)
                //    .HasForeignKey(d => d.LabelId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("article_labelsId");

                //entity.HasOne(d => d.Sort)
                //    .WithMany(p => p.SnArticles)
                //    .HasForeignKey(d => d.SortId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("article_sortId");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.SnArticles)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("article_userId");
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
                    .IsRequired()
                    .HasColumnName("comment_date")
                    .HasColumnType("varchar(20)")
                    .HasComment("评论日期")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommentText)
                    .IsRequired()
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

                //entity.HasOne(d => d.Type)
                //    .WithMany(p => p.SnInterfaces)
                //    .HasForeignKey(d => d.TypeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("sn_interface_ibfk_1");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.SnInterfaces)
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("sn_interface_ibfk_2");
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

            modelBuilder.Entity<SnLabels>(entity =>
            {
                entity.HasKey(e => e.LabelId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_labels");

                entity.Property(e => e.LabelId)
                    .HasColumnType("int(11)")
                    .HasColumnName("label_id");

                entity.Property(e => e.LabelAlias)
                    .HasMaxLength(20)
                    .HasColumnName("label_alias")
                    .HasComment("标签别名");

                entity.Property(e => e.LabelDescription)
                    .HasColumnType("text")
                    .HasColumnName("label_description")
                    .HasComment("标签描述");

                entity.Property(e => e.LabelName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("label_name")
                    .HasComment("标签名称");
            });

            modelBuilder.Entity<SnLeave>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("sn_leave");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("le_id")
                    .HasComment("主键");

                entity.Property(e => e.Time)
                    .HasColumnType("date")
                    .HasColumnName("le_time")
                    .HasComment("发布时间");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("le_title")
                    .HasComment("标题");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
                    .HasComment("用户外键");
            });

            modelBuilder.Entity<SnNavigation>(entity =>
            {
                entity.HasKey(e => e.NavId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_navigation");

                entity.HasIndex(e => e.NavType, "nav_type");

                entity.Property(e => e.NavId)
                    .HasColumnType("int(11)")
                    .HasColumnName("nav_id")
                    .HasComment("主键");

                entity.Property(e => e.NavImg)
                    .HasMaxLength(255)
                    .HasColumnName("nav_img")
                    .HasComment("图片路径");

                entity.Property(e => e.NavText)
                    .HasColumnType("text")
                    .HasColumnName("nav_text")
                    .HasComment("标题描述");

                entity.Property(e => e.NavTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nav_title")
                    .HasComment("标题");

                entity.Property(e => e.NavType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nav_type")
                    .HasComment("分类");

                entity.Property(e => e.NavUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("nav_url")
                    .HasComment("链接路径");
            });


            modelBuilder.Entity<SnNavigationType>(entity =>
            {
                entity.ToTable("sn_navigation_type");

                entity.HasIndex(e => new { e.Id, e.NavType }, "id");

                entity.HasIndex(e => e.NavType, "nav_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.NavType)
                    .HasMaxLength(50)
                    .HasColumnName("nav_type")
                    .HasComment("分类外键");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .HasComment("标题");
            });

            modelBuilder.Entity<SnOne>(entity =>
            {
                entity.HasKey(e => e.OneId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_one");

                entity.HasIndex(e => e.OneTypeId, "sn_one_type");

                entity.Property(e => e.OneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("one_id")
                    .HasComment("主键");

                entity.Property(e => e.OneAuthor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("one_author")
                    .HasComment("作者");

                entity.Property(e => e.OneComment)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("one_comment")
                    .HasComment("评论");

                entity.Property(e => e.OneData)
                    .HasColumnType("date")
                    .HasColumnName("one_data")
                    .HasComment("时间");

                entity.Property(e => e.OneGive)
                    .HasColumnType("int(11)")
                    .HasColumnName("one_give")
                    .HasComment("点赞");

                entity.Property(e => e.OneImg)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("one_img")
                    .HasComment("图片");

                entity.Property(e => e.OneRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("one_read")
                    .HasComment("阅读数");

                entity.Property(e => e.OneText)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("one_text")
                    .HasComment("内容");

                entity.Property(e => e.OneTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("one_title")
                    .HasComment("标题");

                entity.Property(e => e.OneTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("one_type_id")
                    .HasComment("分类");
            });

            modelBuilder.Entity<SnOneType>(entity =>
            {
                entity.ToTable("sn_one_type");

                entity.HasIndex(e => e.SoTypeId, "so_type_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.SoTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("so_type_id");

                entity.Property(e => e.SoTypeTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("so_type_title");
            });

            modelBuilder.Entity<SnPicture>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_picture");

                entity.Property(e => e.PictureId)
                    .HasColumnType("int(11)")
                    .HasColumnName("picture_id");

                entity.Property(e => e.PictureTitle)
                    .HasMaxLength(255)
                    .HasColumnName("picture_title")
                    .HasComment("标题");

                entity.Property(e => e.PictureTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("picture_type_id")
                    .HasComment("分类");

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(255)
                    .HasColumnName("picture_url")
                    .HasComment("图片地址");
            });

            modelBuilder.Entity<SnPictureType>(entity =>
            {
                entity.ToTable("sn_picture_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.PictureTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("picture_type_id")
                    .HasComment("分类");

                entity.Property(e => e.PictureTypeName)
                    .HasMaxLength(100)
                    .HasColumnName("picture_type_name")
                    .HasComment("分类名称");
            });

            modelBuilder.Entity<SnSoftware>(entity =>
            {
                entity.HasKey(e => e.SoId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_software");

                entity.Property(e => e.SoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("so_id");

                entity.Property(e => e.SoComment)
                    .HasMaxLength(255)
                    .HasColumnName("so_comment")
                    .HasComment("评论");

                entity.Property(e => e.SoData)
                    .HasMaxLength(20)
                    .HasColumnName("so_data")
                    .HasComment("时间");

                entity.Property(e => e.SoImg)
                    .HasMaxLength(200)
                    .HasColumnName("so_img")
                    .HasComment("图片路径");

                entity.Property(e => e.SoTitle)
                    .HasMaxLength(100)
                    .HasColumnName("so_title")
                    .HasComment("标题");

                entity.Property(e => e.SoTypeid)
                    .HasColumnType("int(11)")
                    .HasColumnName("so_typeid")
                    .HasComment("分类");
            });

            modelBuilder.Entity<SnSoftwareType>(entity =>
            {
                entity.HasKey(e => e.SoId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_software_type");

                entity.Property(e => e.SoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("so_id");

                entity.Property(e => e.SoType)
                    .HasMaxLength(20)
                    .HasColumnName("so-type");
            });

            modelBuilder.Entity<SnSort>(entity =>
            {
                entity.HasKey(e => e.SortId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_sort");

                entity.Property(e => e.SortId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sort_id");

                entity.Property(e => e.ParentSortId)
                    .HasColumnType("int(11)")
                    .HasColumnName("parent_sort_id")
                    .HasComment("父分类id");

                entity.Property(e => e.SortAlias)
                    .HasMaxLength(50)
                    .HasColumnName("sort_alias")
                    .HasComment("分类别名");

                entity.Property(e => e.SortDescription)
                    .HasColumnType("text")
                    .HasColumnName("sort_description")
                    .HasComment("分类描述");

                entity.Property(e => e.SortName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("sort_name")
                    .HasComment("分类名称");
            });

            modelBuilder.Entity<SnTalk>(entity =>
            {
                entity.ToTable("sn_talk");

                entity.HasIndex(e => e.TalkTypeId, "sn_talk_typeId");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.TalkBrief)
                    .HasMaxLength(255)
                    .HasColumnName("talk_brief")
                    .HasComment("简介");

                entity.Property(e => e.TalkComment)
                    .HasColumnType("int(11)")
                    .HasColumnName("talk_comment")
                    .HasComment("评论");

                entity.Property(e => e.TalkGive)
                    .HasColumnType("int(11)")
                    .HasColumnName("talk_give")
                    .HasComment("点赞");

                entity.Property(e => e.TalkRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("talk_read")
                    .HasComment("阅读量");

                entity.Property(e => e.TalkText)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("talk_text")
                    .HasComment("内容");

                entity.Property(e => e.TalkTime)
                    .HasColumnType("date")
                    .HasColumnName("talk_time")
                    .HasComment("发表时间");

                entity.Property(e => e.TalkTitle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("talk_title")
                    .HasComment("标题");

                entity.Property(e => e.TalkTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("talk_type_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
                    .HasComment("用户");
            });

            modelBuilder.Entity<SnTalkType>(entity =>
            {
                entity.ToTable("sn_talk_type");

                entity.HasIndex(e => e.TalkId, "talk_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.TalkId)
                    .HasColumnType("int(11)")
                    .HasColumnName("talk_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<SnUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_user");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
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

                entity.Property(e => e.UserIp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_ip")
                    .HasComment("ip地址");
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

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.SnUserTalks)
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("sn_user_talk_userId");
            });

            modelBuilder.Entity<SnVideo>(entity =>
            {
                entity.HasKey(e => e.VId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_video");

                entity.HasIndex(e => e.VTypeid, "video");

                entity.Property(e => e.VId)
                    .HasColumnType("int(11)")
                    .HasColumnName("v_id");

                entity.Property(e => e.VData)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("v_data")
                    .HasComment("时间");

                entity.Property(e => e.VImg)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("v_img")
                    .HasComment("图片");

                entity.Property(e => e.VTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("v_title")
                    .HasComment("标题");

                entity.Property(e => e.VTypeid)
                    .HasColumnType("int(11)")
                    .HasColumnName("v_typeid")
                    .HasComment("分类");

                entity.Property(e => e.VUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("v_url")
                    .HasComment("链接路径");

                //entity.HasOne(d => d.VType)
                //    .WithMany(p => p.SnVideos)
                //    .HasForeignKey(d => d.VTypeid)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("video");
            });

            modelBuilder.Entity<SnVideoType>(entity =>
            {
                entity.HasKey(e => e.VId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_video_type");

                entity.Property(e => e.VId)
                    .HasColumnType("int(11)")
                    .HasColumnName("v_id");

                entity.Property(e => e.VType)
                    .HasMaxLength(20)
                    .HasColumnName("v_type");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
