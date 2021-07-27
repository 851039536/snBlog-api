using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;

namespace Snblog.Models
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

                entity.HasIndex(e => e.LabelId)
                    .HasName("article_labelsId");

                entity.HasIndex(e => e.SortId)
                    .HasName("article_sortId");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .HasColumnType("int(11)")
                    .HasComment("主键");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("tinyint(5)")
                    .HasComment("评论");

                entity.Property(e => e.Give)
                    .HasColumnName("give")
                    .HasColumnType("int(20)")
                    .HasComment("点赞");

                entity.Property(e => e.LabelId)
                    .HasColumnName("label_id")
                    .HasColumnType("int(11)")
                    .HasComment("标签外键");

                entity.Property(e => e.Read)
                    .HasColumnName("read")
                    .HasColumnType("int(20)")
                    .HasComment("阅读次数");

                entity.Property(e => e.SortId)
                    .HasColumnName("sort_id")
                    .HasColumnType("int(11)")
                    .HasComment("分类外键");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("text")
                    .HasComment("博客内容")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("发表时间")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)")
                    .HasComment("标题 ")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TitleText)
                    .HasColumnName("title_text")
                    .HasColumnType("text")
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
                    .HasColumnType("int(5)")
                    .HasComment("用户外键id");

                //entity.HasOne(d => d.Label)
                //    .WithMany(p => p.SnArticle)
                //    .HasForeignKey(d => d.LabelId)
                //    .HasConstraintName("article_labelsId");

                //entity.HasOne(d => d.Sort)
                //    .WithMany(p => p.SnArticle)
                //    .HasForeignKey(d => d.SortId)
                //    .HasConstraintName("article_sortId");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.SnArticle)
                //    .HasForeignKey(d => d.UserId)
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
                    .IsRequired()
                    .HasColumnName("label_name")
                    .HasColumnType("varchar(20)")
                    .HasComment("标签名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnLeave>(entity =>
            {
                entity.ToTable("sn_leave");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .HasComment("主键");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime")
                    .HasComment("发布时间");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("text")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户外键");
            });

            modelBuilder.Entity<SnNavigation>(entity =>
            {
                entity.HasKey(e => e.NavId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_navigation");

                entity.Property(e => e.NavId)
                    .HasColumnName("nav_id")
                    .HasColumnType("int(11)")
                    .HasComment("主键");

                entity.Property(e => e.NavImg)
                    .HasColumnName("nav_img")
                    .HasColumnType("varchar(255)")
                    .HasComment("图片路径")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NavText)
                    .HasColumnName("nav_text")
                    .HasColumnType("text")
                    .HasComment("标题描述")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NavTitle)
                    .IsRequired()
                    .HasColumnName("nav_title")
                    .HasColumnType("varchar(50)")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NavType)
                    .IsRequired()
                    .HasColumnName("nav_type")
                    .HasColumnType("varchar(20)")
                    .HasComment("分类")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NavUrl)
                    .IsRequired()
                    .HasColumnName("nav_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("链接路径")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnNavigationType>(entity =>
            {
                entity.ToTable("sn_navigation_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .HasComment("主键");

                entity.Property(e => e.NavType)
                    .HasColumnName("nav_type")
                    .HasColumnType("varchar(50)")
                    .HasComment("分类外键")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnOne>(entity =>
            {
                entity.HasKey(e => e.OneId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_one");

                entity.HasIndex(e => e.OneTypeId)
                    .HasName("sn_one_type");

                entity.Property(e => e.OneId)
                    .HasColumnName("one_id")
                    .HasColumnType("int(11)")
                    .HasComment("主键");

                entity.Property(e => e.OneAuthor)
                    .HasColumnName("one_author")
                    .HasColumnType("varchar(20)")
                    .HasComment("作者")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OneComment)
                    .HasColumnName("one_comment")
                    .HasColumnType("int(11) unsigned zerofill")
                    .HasComment("评论");

                entity.Property(e => e.OneData)
                    .HasColumnName("one_data")
                    .HasColumnType("datetime")
                    .HasComment("时间");

                entity.Property(e => e.OneGive)
                    .HasColumnName("one_give")
                    .HasColumnType("int(11)")
                    .HasComment("点赞");

                entity.Property(e => e.OneImg)
                    .HasColumnName("one_img")
                    .HasColumnType("varchar(255)")
                    .HasComment("图片")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OneRead)
                    .HasColumnName("one_read")
                    .HasColumnType("int(11)")
                    .HasComment("阅读数");

                entity.Property(e => e.OneText)
                    .HasColumnName("one_text")
                    .HasColumnType("text")
                    .HasComment("内容")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OneTitle)
                    .IsRequired()
                    .HasColumnName("one_title")
                    .HasColumnType("varchar(100)")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OneTypeId)
                    .HasColumnName("one_type_id")
                    .HasColumnType("int(11)")
                    .HasComment("分类");
            });

            modelBuilder.Entity<SnOneType>(entity =>
            {
                entity.ToTable("sn_one_type");

                entity.HasIndex(e => e.SoTypeId)
                    .HasName("so_type_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SoTypeId)
                    .HasColumnName("so_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SoTypeTitle)
                    .IsRequired()
                    .HasColumnName("so_type_title")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnPicture>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_picture");

                entity.Property(e => e.PictureId)
                    .HasColumnName("picture_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PictureTitle)
                    .HasColumnName("picture_title")
                    .HasColumnType("varchar(255)")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PictureTypeId)
                    .HasColumnName("picture_type_id")
                    .HasColumnType("int(11)")
                    .HasComment("分类");

                entity.Property(e => e.PictureUrl)
                    .HasColumnName("picture_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("图片地址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnPictureType>(entity =>
            {
                entity.ToTable("sn_picture_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PictureTypeId)
                    .HasColumnName("picture_type_id")
                    .HasColumnType("int(11)")
                    .HasComment("分类");

                entity.Property(e => e.PictureTypeName)
                    .HasColumnName("picture_type_name")
                    .HasColumnType("varchar(100)")
                    .HasComment("分类名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnSoftware>(entity =>
            {
                entity.HasKey(e => e.SoId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_software");

                entity.Property(e => e.SoId)
                    .HasColumnName("so_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SoComment)
                    .HasColumnName("so_comment")
                    .HasColumnType("varchar(255)")
                    .HasComment("评论")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SoData)
                    .HasColumnName("so_data")
                    .HasColumnType("varchar(20)")
                    .HasComment("时间")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SoImg)
                    .HasColumnName("so_img")
                    .HasColumnType("varchar(200)")
                    .HasComment("图片路径")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SoTitle)
                    .HasColumnName("so_title")
                    .HasColumnType("varchar(100)")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SoTypeid)
                    .HasColumnName("so_typeid")
                    .HasColumnType("int(11)")
                    .HasComment("分类");
            });

            modelBuilder.Entity<SnSoftwareType>(entity =>
            {
                entity.HasKey(e => e.SoId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_software_type");

                entity.Property(e => e.SoId)
                    .HasColumnName("so_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SoType)
                    .HasColumnName("so-type")
                    .HasColumnType("varchar(20)")
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
                    .IsRequired()
                    .HasColumnName("sort_name")
                    .HasColumnType("varchar(20)")
                    .HasComment("分类名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SnTalk>(entity =>
            {
                entity.ToTable("sn_talk");

                entity.HasIndex(e => e.TalkTypeId)
                    .HasName("sn_talk_typeId");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TalkBrief)
                    .HasColumnName("talk_brief")
                    .HasColumnType("varchar(255)")
                    .HasComment("简介")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TalkComment)
                    .HasColumnName("talk_comment")
                    .HasColumnType("int(11)")
                    .HasComment("评论");

                entity.Property(e => e.TalkGive)
                    .HasColumnName("talk_give")
                    .HasColumnType("int(11)")
                    .HasComment("点赞");

                entity.Property(e => e.TalkRead)
                    .HasColumnName("talk_read")
                    .HasColumnType("int(11)")
                    .HasComment("阅读量");

                entity.Property(e => e.TalkText)
                    .IsRequired()
                    .HasColumnName("talk_text")
                    .HasColumnType("text")
                    .HasComment("内容")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TalkTime)
                    .HasColumnName("talk_time")
                    .HasColumnType("datetime")
                    .HasComment("发表时间");

                entity.Property(e => e.TalkTitle)
                    .IsRequired()
                    .HasColumnName("talk_title")
                    .HasColumnType("varchar(255)")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TalkTypeId)
                    .HasColumnName("talk_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户");
            });

            modelBuilder.Entity<SnTalkType>(entity =>
            {
                entity.ToTable("sn_talk_type");

                entity.HasIndex(e => e.TalkId)
                    .HasName("talk_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TalkId)
                    .HasColumnName("talk_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("varchar(100)")
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
                    .HasColumnType("int(11)")
                    .HasComment("主键");

                entity.Property(e => e.UserBrief)
                    .IsRequired()
                    .HasColumnName("user_brief")
                    .HasColumnType("varchar(100)")
                    .HasComment("简介")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("user_email")
                    .HasColumnType("varchar(30)")
                    .HasComment("邮箱")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserIp)
                    .IsRequired()
                    .HasColumnName("user_ip")
                    .HasColumnType("varchar(20)")
                    .HasComment("ip地址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasColumnType("varchar(20)")
                    .HasComment("用户名称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserNickname)
                    .IsRequired()
                    .HasColumnName("user_nickname")
                    .HasColumnType("varchar(20)")
                    .HasComment("称呼")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserPhoto)
                    .IsRequired()
                    .HasColumnName("user_photo")
                    .HasColumnType("varchar(255)")
                    .HasComment("头像")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserPwd)
                    .IsRequired()
                    .HasColumnName("user_pwd")
                    .HasColumnType("varchar(20)")
                    .HasComment("密码")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserTime)
                    .IsRequired()
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

            modelBuilder.Entity<SnUserTalk>(entity =>
            {
                entity.ToTable("sn_user_talk");

                entity.HasIndex(e => e.UserId)
                    .HasName("sn_user_talk_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasColumnType("int(11)")
                    .HasComment("评论id");

                entity.Property(e => e.TalkGive)
                    .HasColumnName("talk_give")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TalkRead)
                    .HasColumnName("talk_read")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TalkText)
                    .HasColumnName("talk_text")
                    .HasColumnType("varchar(255)")
                    .HasComment("说说内容")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TalkTime)
                    .HasColumnName("talk_time")
                    .HasColumnType("datetime")
                    .HasComment("发表时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.SnUserTalk)
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("sn_user_talk_userId");
            });

            modelBuilder.Entity<SnVideo>(entity =>
            {
                entity.HasKey(e => e.VId)
                    .HasName("PRIMARY");

                entity.ToTable("sn_video");

                entity.HasIndex(e => e.VTypeid)
                    .HasName("video");

                entity.Property(e => e.VId)
                    .HasColumnName("v_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.VData)
                    .IsRequired()
                    .HasColumnName("v_data")
                    .HasColumnType("varchar(50)")
                    .HasComment("时间")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VImg)
                    .IsRequired()
                    .HasColumnName("v_img")
                    .HasColumnType("varchar(255)")
                    .HasComment("图片")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VTitle)
                    .IsRequired()
                    .HasColumnName("v_title")
                    .HasColumnType("varchar(50)")
                    .HasComment("标题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VTypeid)
                    .HasColumnName("v_typeid")
                    .HasColumnType("int(11)")
                    .HasComment("分类");

                entity.Property(e => e.VUrl)
                    .IsRequired()
                    .HasColumnName("v_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("链接路径")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                //entity.HasOne(d => d.VType)
                //    .WithMany(p => p.SnVideo)
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
                    .HasColumnName("v_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.VType)
                    .HasColumnName("v_type")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
