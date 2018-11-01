using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntitiyTempp
{
    public partial class HBContext : DbContext
    {
        public virtual DbSet<Anket> Anket { get; set; }
        public virtual DbSet<AnketSecenek> AnketSecenek { get; set; }
        public virtual DbSet<AspnetApplications> AspnetApplications { get; set; }
        public virtual DbSet<AspnetMembership> AspnetMembership { get; set; }
        public virtual DbSet<AspnetPaths> AspnetPaths { get; set; }
        public virtual DbSet<AspnetPersonalizationAllUsers> AspnetPersonalizationAllUsers { get; set; }
        public virtual DbSet<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; }
        public virtual DbSet<AspnetProfile> AspnetProfile { get; set; }
        public virtual DbSet<AspnetRoles> AspnetRoles { get; set; }
        public virtual DbSet<AspnetSchemaVersions> AspnetSchemaVersions { get; set; }
        public virtual DbSet<AspnetUsers> AspnetUsers { get; set; }
        public virtual DbSet<AspnetUsersInRoles> AspnetUsersInRoles { get; set; }
        public virtual DbSet<AspnetWebEventEvents> AspnetWebEventEvents { get; set; }
        public virtual DbSet<Etiket> Etiket { get; set; }
        public virtual DbSet<Gorus> Gorus { get; set; }
        public virtual DbSet<Haber> Haber { get; set; }
        public virtual DbSet<HaberEtiket> HaberEtiket { get; set; }
        public virtual DbSet<HaberTip> HaberTip { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Resim> Resim { get; set; }
        public virtual DbSet<Yorum> Yorum { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=BAKIOZTURK\SQLSERVER;Initial Catalog=HaberPortalDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anket>(entity =>
            {
                entity.Property(e => e.Baslik)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

                entity.Property(e => e.KatilimciSayisi).HasDefaultValueSql("((0))");

                entity.Property(e => e.SonOyTarihi)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(day,(30),getdate()))");

                entity.Property(e => e.YayimTarihi)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.Anket)
                    .HasForeignKey(d => d.KategoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Anket_Kategori");
            });

            modelBuilder.Entity<AnketSecenek>(entity =>
            {
                entity.Property(e => e.AnketId).HasColumnName("AnketID");

                entity.Property(e => e.Metin)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.OySayisi).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Anket)
                    .WithMany(p => p.AnketSecenek)
                    .HasForeignKey(d => d.AnketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnketSecenek_Anket");
            });

            modelBuilder.Entity<AspnetApplications>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("aspnet_Applications");

                entity.HasIndex(e => e.ApplicationName)
                    .HasName("UQ__aspnet_A__3091033160803F8F")
                    .IsUnique();

                entity.HasIndex(e => e.LoweredApplicationName)
                    .HasName("aspnet_Applications_Index")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ApplicationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LoweredApplicationName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspnetMembership>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("aspnet_Membership");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredEmail })
                    .HasName("aspnet_Membership_index")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Comment).HasColumnType("ntext(3000)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FailedPasswordAnswerAttemptWindowStart).HasColumnType("datetime");

                entity.Property(e => e.FailedPasswordAttemptWindowStart).HasColumnType("datetime");

                entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");

                entity.Property(e => e.LoweredEmail).HasMaxLength(256);

                entity.Property(e => e.MobilePin)
                    .HasColumnName("MobilePIN")
                    .HasMaxLength(16);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordAnswer).HasMaxLength(128);

                entity.Property(e => e.PasswordFormat).HasDefaultValueSql("((0))");

                entity.Property(e => e.PasswordQuestion).HasMaxLength(256);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetMembership)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Me__Appli__7F2BE32F");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspnetMembership)
                    .HasForeignKey<AspnetMembership>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Me__UserI__00200768");
            });

            modelBuilder.Entity<AspnetPaths>(entity =>
            {
                entity.HasKey(e => e.PathId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("aspnet_Paths");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredPath })
                    .HasName("aspnet_Paths_index")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.PathId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LoweredPath)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetPaths)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pa__Appli__30C33EC3");
            });

            modelBuilder.Entity<AspnetPersonalizationAllUsers>(entity =>
            {
                entity.HasKey(e => e.PathId);

                entity.ToTable("aspnet_PersonalizationAllUsers");

                entity.Property(e => e.PathId).ValueGeneratedNever();

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.PageSettings)
                    .IsRequired()
                    .HasColumnType("image(6000)");

                entity.HasOne(d => d.Path)
                    .WithOne(p => p.AspnetPersonalizationAllUsers)
                    .HasForeignKey<AspnetPersonalizationAllUsers>(d => d.PathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pe__PathI__367C1819");
            });

            modelBuilder.Entity<AspnetPersonalizationPerUser>(entity =>
            {
                entity.ToTable("aspnet_PersonalizationPerUser");

                entity.HasIndex(e => new { e.PathId, e.UserId })
                    .HasName("aspnet_PersonalizationPerUser_index1")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.HasIndex(e => new { e.UserId, e.PathId })
                    .HasName("aspnet_PersonalizationPerUser_ncindex2")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.PageSettings)
                    .IsRequired()
                    .HasColumnType("image(6000)");

                entity.HasOne(d => d.Path)
                    .WithMany(p => p.AspnetPersonalizationPerUser)
                    .HasForeignKey(d => d.PathId)
                    .HasConstraintName("FK__aspnet_Pe__PathI__3A4CA8FD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspnetPersonalizationPerUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__aspnet_Pe__UserI__3B40CD36");
            });

            modelBuilder.Entity<AspnetProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("aspnet_Profile");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.PropertyNames)
                    .IsRequired()
                    .HasColumnType("ntext(6000)");

                entity.Property(e => e.PropertyValuesBinary)
                    .IsRequired()
                    .HasColumnType("image(6000)");

                entity.Property(e => e.PropertyValuesString)
                    .IsRequired()
                    .HasColumnType("ntext(6000)");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspnetProfile)
                    .HasForeignKey<AspnetProfile>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pr__UserI__14270015");
            });

            modelBuilder.Entity<AspnetRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("aspnet_Roles");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredRoleName })
                    .HasName("aspnet_Roles_index1")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LoweredRoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetRoles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Ro__Appli__1DB06A4F");
            });

            modelBuilder.Entity<AspnetSchemaVersions>(entity =>
            {
                entity.HasKey(e => new { e.Feature, e.CompatibleSchemaVersion });

                entity.ToTable("aspnet_SchemaVersions");

                entity.Property(e => e.Feature).HasMaxLength(128);

                entity.Property(e => e.CompatibleSchemaVersion).HasMaxLength(128);
            });

            modelBuilder.Entity<AspnetUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("aspnet_Users");

                entity.HasIndex(e => new { e.ApplicationId, e.LastActivityDate })
                    .HasName("aspnet_Users_Index2");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredUserName })
                    .HasName("aspnet_Users_Index")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LoweredUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MobileAlias).HasMaxLength(16);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetUsers)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__Appli__6EF57B66");
            });

            modelBuilder.Entity<AspnetUsersInRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("aspnet_UsersInRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("aspnet_UsersInRoles_index");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspnetUsersInRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__RoleI__22751F6C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspnetUsersInRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__UserI__2180FB33");
            });

            modelBuilder.Entity<AspnetWebEventEvents>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("aspnet_WebEvent_Events");

                entity.Property(e => e.EventId)
                    .HasColumnType("char(32)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ApplicationPath).HasMaxLength(256);

                entity.Property(e => e.ApplicationVirtualPath).HasMaxLength(256);

                entity.Property(e => e.Details).HasColumnType("ntext");

                entity.Property(e => e.EventOccurrence).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.EventSequence).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.EventTime).HasColumnType("datetime");

                entity.Property(e => e.EventTimeUtc).HasColumnType("datetime");

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ExceptionType).HasMaxLength(256);

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Message).HasMaxLength(1024);

                entity.Property(e => e.RequestUrl).HasMaxLength(1024);
            });

            modelBuilder.Entity<Etiket>(entity =>
            {
                entity.Property(e => e.Adi).HasMaxLength(100);
            });

            modelBuilder.Entity<Gorus>(entity =>
            {
                entity.Property(e => e.Baslik)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Begeni).HasDefaultValueSql("((0))");

                entity.Property(e => e.GorusTarihi)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icerik)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Tiksinti).HasDefaultValueSql("((0))");

                entity.Property(e => e.YazarId).HasColumnName("YazarID");

                entity.HasOne(d => d.Yazar)
                    .WithMany(p => p.Gorus)
                    .HasForeignKey(d => d.YazarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gorus_aspnet_Users");
            });

            modelBuilder.Entity<Haber>(entity =>
            {
                entity.Property(e => e.Baslik)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Goruntulenme).HasDefaultValueSql("((0))");

                entity.Property(e => e.Icerik)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

                entity.Property(e => e.KucukResimYol).HasMaxLength(200);

                entity.Property(e => e.Ozet)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ResimYol)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TipId).HasColumnName("TipID");

                entity.Property(e => e.VideoYol).HasMaxLength(200);

                entity.Property(e => e.YayimTarihi)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.YazarId).HasColumnName("YazarID");

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.Haber)
                    .HasForeignKey(d => d.KategoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Haber_Kategori");

                entity.HasOne(d => d.Tip)
                    .WithMany(p => p.Haber)
                    .HasForeignKey(d => d.TipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Haber_HaberTip");

                entity.HasOne(d => d.Yazar)
                    .WithMany(p => p.Haber)
                    .HasForeignKey(d => d.YazarId)
                    .HasConstraintName("FK_Haber_aspnet_Users");
            });

            modelBuilder.Entity<HaberEtiket>(entity =>
            {
                entity.HasKey(e => new { e.HaberId, e.EtiketId });

                entity.Property(e => e.HaberId).HasColumnName("HaberID");

                entity.Property(e => e.EtiketId).HasColumnName("EtiketID");

                entity.HasOne(d => d.Etiket)
                    .WithMany(p => p.HaberEtiket)
                    .HasForeignKey(d => d.EtiketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HaberEtiket_Etiket");

                entity.HasOne(d => d.Haber)
                    .WithMany(p => p.HaberEtiket)
                    .HasForeignKey(d => d.HaberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HaberEtiket_Haber");
            });

            modelBuilder.Entity<HaberTip>(entity =>
            {
                entity.Property(e => e.Adi)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.Property(e => e.Aciklama)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Adi)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ResimYol)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UstKategoriId).HasColumnName("UstKategoriID");

                entity.HasOne(d => d.UstKategori)
                    .WithMany(p => p.InverseUstKategori)
                    .HasForeignKey(d => d.UstKategoriId)
                    .HasConstraintName("FK_Kategori_Kategori");
            });

            modelBuilder.Entity<Resim>(entity =>
            {
                entity.Property(e => e.HaberId).HasColumnName("HaberID");

                entity.Property(e => e.Ozet)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ResimYol)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Haber)
                    .WithMany(p => p.Resim)
                    .HasForeignKey(d => d.HaberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resim_Haber");
            });

            modelBuilder.Entity<Yorum>(entity =>
            {
                entity.Property(e => e.AdSoyad)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Baslik)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Begeni).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GorusId).HasColumnName("GorusID");

                entity.Property(e => e.HaberId).HasColumnName("HaberID");

                entity.Property(e => e.Icerik)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tarih)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tiksinti).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Gorus)
                    .WithMany(p => p.Yorum)
                    .HasForeignKey(d => d.GorusId)
                    .HasConstraintName("FK_Yorum_Gorus");

                entity.HasOne(d => d.Haber)
                    .WithMany(p => p.Yorum)
                    .HasForeignKey(d => d.HaberId)
                    .HasConstraintName("FK_Yorum_Haber1");
            });
        }
    }
}
