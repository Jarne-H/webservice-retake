using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PD4ExamAPI.Models;

public partial class MazeContext : DbContext
{
    public MazeContext()
    {
    }

    public MazeContext(DbContextOptions<MazeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GameSession> GameSessions { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Maze> Mazes { get; set; }

    public virtual DbSet<MazeTile> MazeTiles { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VIVOBOOK-JARNE;Database=Maze;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.ToTable("GameSession");

            entity.Property(e => e.GameSessionId)
                .ValueGeneratedNever()
                .HasColumnName("GameSessionID");
            entity.Property(e => e.MazeId).HasColumnName("MazeID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Maze).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.MazeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameSession_GameSession");

            entity.HasOne(d => d.Player).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameSession_Player");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("images");

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("ImageID");
            entity.Property(e => e.Link)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Maze>(entity =>
        {
            entity.ToTable("Maze");

            entity.Property(e => e.MazeId)
                .ValueGeneratedNever()
                .HasColumnName("MazeID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.OriginalMazeId).HasColumnName("OriginalMazeID");
        });

        modelBuilder.Entity<MazeTile>(entity =>
        {
            entity.HasKey(e => e.TileId);

            entity.ToTable("MazeTile");

            entity.Property(e => e.TileId)
                .ValueGeneratedNever()
                .HasColumnName("TileID");
            entity.Property(e => e.MazeId).HasColumnName("MazeID");
            entity.Property(e => e.TileType)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Maze).WithMany(p => p.MazeTiles)
                .HasForeignKey(d => d.MazeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MazeTile_Maze");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Player");

            entity.Property(e => e.PlayerId)
                .ValueGeneratedNever()
                .HasColumnName("PlayerID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
