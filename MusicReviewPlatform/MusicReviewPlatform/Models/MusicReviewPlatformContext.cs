using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicReviewPlatform.Models;

public partial class MusicReviewPlatformContext : DbContext
{
    public MusicReviewPlatformContext()
    {
    }

    public MusicReviewPlatformContext(DbContextOptions<MusicReviewPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Download> Downloads { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistSong> PlaylistSongs { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MAKUOSPC;Database=MusicReviewPlatform;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__97B4BE17EAF31CE2");

            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Albums__ArtistID__3D5E1FD2");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B703C1EFC84");

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAA3E4155BA");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CommentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Review).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("FK__Comments__Review__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__UserID__5AEE82B9");
        });

        modelBuilder.Entity<Download>(entity =>
        {
            entity.HasKey(e => e.DownloadId).HasName("PK__Download__73D5A7103DE6E1EC");

            entity.Property(e => e.DownloadId).HasColumnName("DownloadID");
            entity.Property(e => e.DownloadDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SongId).HasColumnName("SongID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Song).WithMany(p => p.Downloads)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("FK__Downloads__SongI__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Downloads)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Downloads__UserI__5165187F");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAF5F14ED36D");

            entity.Property(e => e.FavoriteId).HasColumnName("FavoriteID");
            entity.Property(e => e.SongId).HasColumnName("SongID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Song).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("FK__Favorites__SongI__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Favorites__UserI__5535A963");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__B3016780AE979570");

            entity.Property(e => e.PlaylistId).HasColumnName("PlaylistID");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Playlists__UserI__49C3F6B7");
        });

        modelBuilder.Entity<PlaylistSong>(entity =>
        {
            entity.HasKey(e => e.PlaylistSongId).HasName("PK__Playlist__D58F7B0EFD7D0F0F");

            entity.Property(e => e.PlaylistSongId).HasColumnName("PlaylistSongID");
            entity.Property(e => e.PlaylistId).HasColumnName("PlaylistID");
            entity.Property(e => e.SongId).HasColumnName("SongID");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistSongs)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("FK__PlaylistS__Playl__4CA06362");

            entity.HasOne(d => d.Song).WithMany(p => p.PlaylistSongs)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("FK__PlaylistS__SongI__4D94879B");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE9CBC54ED");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Album).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Reviews__AlbumID__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__45F365D3");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__Songs__12E3D6F7B2C14A54");

            entity.Property(e => e.SongId).HasColumnName("SongID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Songs__AlbumID__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACB527938A");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053433D27B61").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
