using Snblog.Models;

namespace Snblog.Enties.Models;

public partial class SnblogContext : DbContext
{
    public SnblogContext() { }

    public SnblogContext(DbContextOptions<SnblogContext> options)
        : base(options) { }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleTag> ArticleTags { get; set; }

    public virtual DbSet<ArticleType> ArticleTypes { get; set; }

    public virtual DbSet<Diary> Diaries { get; set; }

    public virtual DbSet<DiaryType> DiaryTypes { get; set; }

    public virtual DbSet<Interface> Interfaces { get; set; }

    public virtual DbSet<InterfaceType> InterfaceTypes { get; set; }

    public virtual DbSet<Navigation> Navigations { get; set; }

    public virtual DbSet<NavigationType> NavigationTypes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<PhotoGallery> PhotoGalleries { get; set; }

    public virtual DbSet<PhotoGalleryType> PhotoGalleryTypes { get; set; }

    public virtual DbSet<PhotoType> PhotoTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SnComment> SnComments { get; set; }

    public virtual DbSet<SnPicture> SnPictures { get; set; }

    public virtual DbSet<SnPictureType> SnPictureTypes { get; set; }

    public virtual DbSet<SnSetblog> SnSetblogs { get; set; }

    public virtual DbSet<SnSetblogType> SnSetblogTypes { get; set; }

    public virtual DbSet<SnSoftware> SnSoftwares { get; set; }

    public virtual DbSet<SnSoftwareType> SnSoftwareTypes { get; set; }

    public virtual DbSet<SnTalk> SnTalks { get; set; }

    public virtual DbSet<SnTalkType> SnTalkTypes { get; set; }

    public virtual DbSet<SnUserFriend> SnUserFriends { get; set; }

    public virtual DbSet<SnVideoType> SnVideoTypes { get; set; }

    public virtual DbSet<Snippet> Snippets { get; set; }

    public virtual DbSet<SnippetTag> SnippetTags { get; set; }

    public virtual DbSet<SnippetType> SnippetTypes { get; set; }

    public virtual DbSet<SnippetTypeSub> SnippetTypeSubs { get; set; }

    public virtual DbSet<SnippetVersion> SnippetVersions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTalk> UserTalks { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        =>
    //        optionsBuilder.UseMySql(
    //            "server=127.0.0.1;port=3307;database=snblog;uid=root;pwd=123456;character set=utf8",
    //            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.24-mysql")
    //        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8mb4_0900_ai_ci").HasCharSet("utf8mb4");

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("article").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TagId, "article_labelsId");

            entity.HasIndex(e => e.TypeId, "article_sortId");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasComment("主键").HasColumnName("id");
            entity.Property(e => e.CommentId).HasComment("评论").HasColumnName("comment_id");
            entity.Property(e => e.Give).HasComment("点赞").HasColumnName("give");
            entity.Property(e => e.Img).IsRequired().HasMaxLength(50).HasComment("图片").HasColumnName("img");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasComment("标题 ").HasColumnName("name");
            entity.Property(e => e.Read).HasComment("阅读次数").HasColumnName("read");
            entity.Property(e => e.Sketch).IsRequired().HasComment("内容简述").HasColumnType("mediumtext").HasColumnName("sketch");
            entity.Property(e => e.TagId).HasComment("标签外键").HasColumnName("tag_id");
            entity.Property(e => e.Text).IsRequired().HasComment("博客内容").HasColumnType("mediumtext").HasColumnName("text");
            entity.Property(e => e.TimeCreate).HasComment("发表时间").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasComment("更新时间").HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.TypeId).HasComment("分类外键").HasColumnName("type_id");
            entity.Property(e => e.UserId).HasComment("用户外键id").HasColumnName("user_id");

            entity
                .HasOne(d => d.Tag)
                .WithMany(p => p.Articles)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("article_ibfk_1");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.Articles)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tyoeId");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Articles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userId");
        });

        modelBuilder.Entity<ArticleTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("article_tag").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity
                .Property(e => e.Description)
                .IsRequired()
                .HasComment("标签描述")
                .HasColumnType("mediumtext")
                .HasColumnName("description");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(20).HasComment("标签名称").HasColumnName("name");
        });

        modelBuilder.Entity<ArticleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("article_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasComment("分类描述").HasColumnType("mediumtext").HasColumnName("description");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(20).HasComment("分类名称").HasColumnName("name");
        });

        modelBuilder.Entity<Diary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("diary", tb => tb.HasComment("日记表")).UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.UserId, "one_user_id");

            entity.HasIndex(e => e.TypeId, "sn_one_type");

            entity.Property(e => e.Id).HasComment("主键").HasColumnName("id");
            entity.Property(e => e.CommentId).HasComment("评论").HasColumnName("comment_id");
            entity.Property(e => e.Give).HasComment("点赞").HasColumnName("give");
            entity.Property(e => e.Img).IsRequired().HasMaxLength(255).HasComment("图片").HasColumnName("img");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasComment("标题").HasColumnName("name");
            entity.Property(e => e.Read).HasComment("阅读数").HasColumnName("read");
            entity.Property(e => e.Text).IsRequired().HasComment("内容").HasColumnType("mediumtext").HasColumnName("text");
            entity.Property(e => e.TimeCreate).HasComment("时间").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.TypeId).HasComment("分类").HasColumnName("type_id");
            entity.Property(e => e.UserId).HasComment("作者").HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.Diaries)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("one_type_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Diaries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("one_user_id");
        });

        modelBuilder.Entity<DiaryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("diary_type", tb => tb.HasComment("日记分类")).UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name");
        });

        modelBuilder.Entity<Interface>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("interface").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TypeId, "type_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identity).HasComment("显示隐藏").HasColumnName("identity");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("标题").HasColumnName("name");
            entity.Property(e => e.Path).IsRequired().HasMaxLength(80).HasComment("路径").HasColumnName("path");
            entity.Property(e => e.TypeId).HasComment("类别").HasColumnName("type_id");
            entity.Property(e => e.UserId).HasComment("用户").HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.Interfaces)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("type");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Interfaces)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("interface_ibfk_2");
        });

        modelBuilder.Entity<InterfaceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("interface_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name");
        });

        modelBuilder.Entity<Navigation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("navigation").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TypeId, "nav_type_id");

            entity.HasIndex(e => e.UserId, "nav_user_id");

            entity.Property(e => e.Id).HasComment("主键").HasColumnName("id");
            entity
                .Property(e => e.Describe)
                .IsRequired()
                .HasComment("标题描述")
                .HasColumnType("mediumtext")
                .HasColumnName("describe");
            entity.Property(e => e.Img).IsRequired().HasMaxLength(255).HasComment("图片路径").HasColumnName("img");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(60).HasComment("导航标题").HasColumnName("name");
            entity.Property(e => e.TimeCreate).HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.TypeId).HasComment("分类").HasColumnName("type_id");
            entity.Property(e => e.Url).IsRequired().HasMaxLength(255).HasComment("链接路径").HasColumnName("url");
            entity.Property(e => e.UserId).HasComment("用户").HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.Navigations)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nav_type_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Navigations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nav_user_id");
        });

        modelBuilder.Entity<NavigationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("navigation_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasComment("主键").HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("标题").HasColumnName("name");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permission").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasComment("主键").HasColumnName("id");
            entity.Property(e => e.Description).IsRequired().HasMaxLength(255).HasComment("权限描述").HasColumnName("description");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("权限名称").HasColumnName("name");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("photo", tb => tb.HasComment("图册")).HasCharSet("utf8").UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.PhotoGalleryId, "fk_ph_gallery");

            entity.HasIndex(e => e.TypeId, "fk_type");

            entity.HasIndex(e => e.UserId, "fk_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).IsRequired().HasMaxLength(225).HasComment("描述").HasColumnName("description");
            entity.Property(e => e.Give).HasComment("热度").HasColumnName("give");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(80).HasComment("名称").HasColumnName("name");
            entity.Property(e => e.Path).IsRequired().HasMaxLength(225).HasComment("路径").HasColumnName("path");
            entity.Property(e => e.PhotoGalleryId).HasColumnName("photo_gallery_id");
            entity.Property(e => e.Read).HasComment("阅读").HasColumnName("read");
            entity.Property(e => e.Tag).HasMaxLength(50).HasComment("标签").HasColumnName("tag");
            entity.Property(e => e.TimeCreate).HasComment("创建时间").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TypeId).HasComment("分类").HasColumnName("type_id");
            entity.Property(e => e.UserId).HasComment("用户").HasColumnName("user_id");

            entity
                .HasOne(d => d.PhotoGallery)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.PhotoGalleryId)
                .HasConstraintName("fk_ph_gallery");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_type");

            entity.HasOne(d => d.User).WithMany(p => p.Photos).HasForeignKey(d => d.UserId).HasConstraintName("fk_user");
        });

        modelBuilder.Entity<PhotoGallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("photo_gallery").HasCharSet("utf8").UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.TypeId, "FK1_TYPE");

            entity.HasIndex(e => e.UserId, "FK2_USER");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).IsRequired().HasMaxLength(225).HasComment("描述").HasColumnName("description");
            entity.Property(e => e.Give).HasColumnName("give");
            entity.Property(e => e.Img).IsRequired().HasMaxLength(225).HasColumnName("img");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("名称").HasColumnName("name");
            entity.Property(e => e.Tag).IsRequired().HasMaxLength(50).HasColumnName("tag");
            entity.Property(e => e.TimeCreate).HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.PhotoGalleries)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1_TYPE");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.PhotoGalleries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2_USER");
        });

        modelBuilder.Entity<PhotoGalleryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("photo_gallery_type").HasCharSet("utf8").UseCollation("utf8_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasDefaultValueSql("'0'").HasColumnName("name");
        });

        modelBuilder.Entity<PhotoType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("photo_type").HasCharSet("utf8").UseCollation("utf8_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasComment("主键").HasColumnName("id");
            entity.Property(e => e.Description).IsRequired().HasMaxLength(255).HasComment("角色描述").HasColumnName("description");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("角色名称").HasColumnName("name");

            entity
                .HasMany(d => d.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r =>
                        r.HasOne<Permission>()
                            .WithMany()
                            .HasForeignKey("PermissionId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("role_permission_ibfk_2"),
                    l =>
                        l.HasOne<Role>()
                            .WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("role_permission_ibfk_1"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("role_permission").UseCollation("utf8mb4_bin");
                        j.HasIndex(new[] { "PermissionId" }, "permission_id");
                        j.IndexerProperty<int>("RoleId").HasComment("角色ID").HasColumnName("role_id");
                        j.IndexerProperty<int>("PermissionId").HasComment("权限ID").HasColumnName("permission_id");
                    }
                );
        });

        modelBuilder.Entity<SnComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_comments").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasComment("评论主键").HasColumnName("id");
            entity.Property(e => e.Give).HasComment("点赞数").HasColumnName("give");
            entity.Property(e => e.Text).IsRequired().HasComment("内容").HasColumnType("mediumtext").HasColumnName("text");
            entity.Property(e => e.TimeCreate).HasComment("评论日期").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasComment("更新时间").HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.UserId).HasComment("用户id").HasColumnName("user_id");
        });

        modelBuilder.Entity<SnPicture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_picture").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.UserId, "pivture_user_id");

            entity.HasIndex(e => e.TypeId, "prcture_type_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImgUrl).IsRequired().HasMaxLength(255).HasComment("图片地址").HasColumnName("img_url");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255).HasComment("图床名").HasColumnName("name");
            entity.Property(e => e.TypeId).HasComment("分类").HasColumnName("type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.SnPictures)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prcture_type_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.SnPictures)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pivture_user_id");
        });

        modelBuilder.Entity<SnPictureType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_picture_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("分类名称").HasColumnName("name");
        });

        modelBuilder.Entity<SnSetblog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_setblog").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TypeId, "setblog_type_id");

            entity.HasIndex(e => e.UserId, "setblog_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isopen).HasComment("是否启用").HasColumnName("isopen");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("设置的内容名称").HasColumnName("name");
            entity.Property(e => e.RouterUrl).IsRequired().HasMaxLength(255).HasComment("路由链接").HasColumnName("router_url");
            entity.Property(e => e.TypeId).HasComment("分类").HasColumnName("type_id");
            entity.Property(e => e.UserId).HasComment("关联用户表").HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.SnSetblogs)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("setblog_type_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.SnSetblogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("setblog_user_id");
        });

        modelBuilder.Entity<SnSetblogType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_setblog_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
        });

        modelBuilder.Entity<SnSoftware>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_software").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TypeId, "software_type_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasComment("评论").HasColumnName("comment_id");
            entity.Property(e => e.Img).HasMaxLength(200).HasComment("图片路径").HasColumnName("img");
            entity.Property(e => e.TimeCreate).HasComment("时间").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.Title).HasMaxLength(100).HasComment("标题").HasColumnName("title");
            entity.Property(e => e.TypeId).HasComment("分类").HasColumnName("type_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.SnSoftwares)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("software_type_id");
        });

        modelBuilder.Entity<SnSoftwareType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_software_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(20).HasColumnName("name");
        });

        modelBuilder.Entity<SnTalk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_talk").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TypeId, "sn_talk_typeId");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasComment("评论").HasColumnName("comment_id");
            entity.Property(e => e.Describe).IsRequired().HasMaxLength(255).HasComment("简介").HasColumnName("describe");
            entity.Property(e => e.Give).HasComment("点赞").HasColumnName("give");
            entity.Property(e => e.Read).HasComment("阅读量").HasColumnName("read");
            entity.Property(e => e.Text).IsRequired().HasComment("内容").HasColumnType("mediumtext").HasColumnName("text");
            entity.Property(e => e.TimeCreate).HasComment("发表时间").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100).HasComment("标题").HasColumnName("title");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.UserId).HasComment("用户").HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.SnTalks)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("talk_type_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.SnTalks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lalk_user_id");
        });

        modelBuilder.Entity<SnTalkType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_talk_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name");
        });

        modelBuilder.Entity<SnUserFriend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_user_friends").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserFriendsId).HasComment("好友id").HasColumnName("user_friends_id");
            entity.Property(e => e.UserId).HasComment("用户id").HasColumnName("user_id");
            entity.Property(e => e.UserNote).HasMaxLength(20).HasComment("好友备注").HasColumnName("user_note");
            entity.Property(e => e.UserStatus).HasMaxLength(20).HasComment("好友状态").HasColumnName("user_status");
        });

        modelBuilder.Entity<SnVideoType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sn_video_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(20).HasColumnName("name");
        });

        modelBuilder.Entity<Snippet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("snippet").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TagId, "FK_snippet_snippet_tag");

            entity.HasIndex(e => e.TypeSubId, "FK_snippet_snippet_type_sub");

            entity.HasIndex(e => e.SnippetVersionId, "snippet_version_id");

            entity.HasIndex(e => e.TypeId, "typeid");

            entity.HasIndex(e => e.UserId, "uid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.SnippetVersionId).HasDefaultValueSql("'0'").HasColumnName("snippet_version_id");
            entity.Property(e => e.TagId).HasDefaultValueSql("'1'").HasColumnName("tag_id");
            entity.Property(e => e.Text).HasColumnType("mediumtext").HasColumnName("text");
            entity
                .Property(e => e.TimeCreate)
                .HasDefaultValueSql("'2023-08-04 09:43:08'")
                .HasColumnType("datetime")
                .HasColumnName("time_create");
            entity
                .Property(e => e.TimeUpdate)
                .HasDefaultValueSql("'2023-08-04 09:43:08'")
                .HasColumnType("datetime")
                .HasColumnName("time_update");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeSubId).HasDefaultValueSql("'7'").HasColumnName("type_sub_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.Tag)
                .WithMany(p => p.Snippets)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_snippet_snippet_tag");

            entity.HasOne(d => d.Type).WithMany(p => p.Snippets).HasForeignKey(d => d.TypeId).HasConstraintName("typeid");

            entity
                .HasOne(d => d.TypeSub)
                .WithMany(p => p.Snippets)
                .HasForeignKey(d => d.TypeSubId)
                .HasConstraintName("FK_snippet_snippet_type_sub");

            entity.HasOne(d => d.User).WithMany(p => p.Snippets).HasForeignKey(d => d.UserId).HasConstraintName("uid");
        });

        modelBuilder.Entity<SnippetTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("snippet_tag").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
        });

        modelBuilder.Entity<SnippetType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("snippet_type").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
        });

        modelBuilder.Entity<SnippetTypeSub>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("snippet_type_sub", tb => tb.HasComment("片段分类下的子类"))
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.TypeId, "FK_snippet_type_sub_snippet_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.SnippetTypeSubs)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_snippet_type_sub_snippet_type");
        });

        modelBuilder.Entity<SnippetVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("snippet_version", tb => tb.HasComment("片段的历史版本表")).HasCharSet("utf8").UseCollation("utf8_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasDefaultValueSql("'1'").HasComment("版本变更次数").HasColumnName("count");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("标题").HasColumnName("name");
            entity.Property(e => e.SnippetId).HasComment("关联片段id").HasColumnName("snippet_id");
            entity.Property(e => e.Text).IsRequired().HasComment("内容").HasColumnType("text").HasColumnName("text");
            entity
                .Property(e => e.TimeCreate)
                .HasDefaultValueSql("now()")
                .HasComment("创建时间")
                .HasColumnType("datetime")
                .HasColumnName("time_create");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user").UseCollation("utf8mb4_bin");

            entity.Property(e => e.Id).HasComment("主键").HasColumnName("id");
            entity.Property(e => e.Brief).IsRequired().HasMaxLength(255).HasComment("简介").HasColumnName("brief");
            entity.Property(e => e.Email).IsRequired().HasMaxLength(30).HasComment("邮箱").HasColumnName("email");
            entity.Property(e => e.Ip).IsRequired().HasMaxLength(100).HasComment("ip地址").HasColumnName("ip");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(20).HasComment("账号").HasColumnName("name");
            entity.Property(e => e.Nickname).IsRequired().HasMaxLength(20).HasComment("称呼").HasColumnName("nickname");
            entity.Property(e => e.Photo).IsRequired().HasMaxLength(100).HasComment("头像").HasColumnName("photo");
            entity.Property(e => e.Pwd).IsRequired().HasMaxLength(20).HasComment("密码").HasColumnName("pwd");
            entity
                .Property(e => e.TimeCreate)
                .HasDefaultValueSql("now()")
                .HasComment("注册时间")
                .HasColumnType("datetime")
                .HasColumnName("time_create");
            entity
                .Property(e => e.TimeModified)
                .HasDefaultValueSql("now()")
                .HasComment("更新时间")
                .HasColumnType("datetime")
                .HasColumnName("time_modified");

            entity
                .HasMany(d => d.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r =>
                        r.HasOne<Role>()
                            .WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("user_role_ibfk_2"),
                    l =>
                        l.HasOne<User>()
                            .WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("user_role_ibfk_1"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("user_role").UseCollation("utf8mb4_bin");
                        j.HasIndex(new[] { "RoleId" }, "role_id");
                        j.IndexerProperty<int>("UserId").HasComment("用户ID").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasComment("角色ID").HasColumnName("role_id");
                    }
                );
        });

        modelBuilder.Entity<UserTalk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_talk").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.UserId, "sn_user_talk_userId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasComment("评论id").HasColumnName("comment_id");
            entity.Property(e => e.Give).HasColumnName("give");
            entity.Property(e => e.Read).HasColumnName("read");
            entity.Property(e => e.Text).HasComment("说说内容").HasColumnType("text").HasColumnName("text");
            entity.Property(e => e.TimeCreate).HasComment("发表时间").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.UserTalks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_talk_userId");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("video").UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.TypeId, "video_type_id");

            entity.HasIndex(e => e.UserId, "video_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Img).IsRequired().HasMaxLength(255).HasComment("图片").HasColumnName("img");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasComment("标题").HasColumnName("name");
            entity.Property(e => e.TimeCreate).HasComment("时间").HasColumnType("datetime").HasColumnName("time_create");
            entity.Property(e => e.TimeModified).HasColumnType("datetime").HasColumnName("time_modified");
            entity.Property(e => e.TypeId).HasComment("分类").HasColumnName("type_id");
            entity.Property(e => e.Url).IsRequired().HasMaxLength(255).HasComment("链接路径").HasColumnName("url");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.Type)
                .WithMany(p => p.Videos)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("video_type_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Videos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("video_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
