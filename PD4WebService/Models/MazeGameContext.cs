using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PD4ExamAPI.Models;

public partial class MazeGameContext : DbContext
{

    public MazeGameContext(DbContextOptions<MazeGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExternalResource> ExternalResources { get; set; }

    public virtual DbSet<GameSession> GameSessions { get; set; }

    public virtual DbSet<Maze> Mazes { get; set; }

    public virtual DbSet<MazeTile> MazeTiles { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<PlayfabItem> PlayfabItems { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=DINGUS-10; Database=MazeGame; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExternalResource>(entity =>
        {
            entity.HasKey(e => e.ResourceId);

            entity.Property(e => e.ResourceId).ValueGeneratedOnAdd();
            entity.Property(e => e.ResourceType).HasMaxLength(30);
            entity.Property(e => e.ResourceUrl).HasMaxLength(200);
        });

        modelBuilder.Entity<Maze>(entity =>
        {
            entity.ToTable("Maze");

            entity.Property(e => e.MazeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("MazeID");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<MazeTile>(entity =>
        {
            entity.HasKey(e => e.TileId);

            entity.ToTable("MazeTile");

            entity.Property(e => e.TileId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TileID");
            entity.Property(e => e.MazeId).HasColumnName("MazeID");
            entity.Property(e => e.TileType).HasMaxLength(1);

            entity.HasOne(d => d.Maze).WithMany(p => p.MazeTiles)
                .HasForeignKey(d => d.MazeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MazeTile_Maze");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Player");

            entity.Property(e => e.PlayerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PlayerID");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.ToTable("GameSession");
            entity.Property(e => e.GameSessionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("GameSessionID");
            entity.Property(e => e.MazeId).HasColumnName("MazeID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.HasOne(d => d.Maze).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.MazeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameSession_Maze");
            entity.HasOne(d => d.Player).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameSession_Player");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            //ID, name and link
            entity.ToTable("images");
            entity.Property(e => e.ImageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ImageID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Link)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("link");
        });

        modelBuilder.Entity<PlayfabItem>(entity =>
        {
            entity.HasKey(e => e.PlayfabItemId);
            entity.ToTable("PlayfabItem");

            //add playfabitemid
            entity.Property(e => e.PlayfabItemId)
                .ValueGeneratedOnAdd()
                .HasMaxLength(60)
                .HasColumnName("PlayfabItemID");


            entity.Property(e => e.playfabid)
                .ValueGeneratedNever()
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("playfabid");

            entity.Property(e => e.displayname)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("displayname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}