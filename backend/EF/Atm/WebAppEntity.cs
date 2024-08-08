using System;
using System.Collections.Generic;
using backend.Extensions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace backend.EF.Atm;

public partial class WebAppEntity : DbContext
{
    public WebAppEntity()
    {
    }

    public WebAppEntity(DbContextOptions<WebAppEntity> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAdmin> TbAdmin { get; set; }

    public virtual DbSet<TbAtmData> TbAtmData { get; set; }

    public virtual DbSet<TbAtmDataHistory> TbAtmDataHistory { get; set; }

    public virtual DbSet<TbCustomer> TbCustomer { get; set; }

    public virtual DbSet<TbTransactionHistory> TbTransactionHistory { get; set; }

    public virtual DbSet<TmDataType> TmDataType { get; set; }

    public virtual DbSet<TmMasterData> TmMasterData { get; set; }

    public virtual DbSet<TmMenu> TmMenu { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     if (!optionsBuilder.IsConfigured)
     {
         optionsBuilder.UseSqlServer(Systemfunction.GetConnectionString("AtmConnection"));
     }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<TbAdmin>(entity =>
        {
            entity.HasKey(e => e.nUserID);

            entity.Property(e => e.nUserID).ValueGeneratedNever();
            entity.Property(e => e.dCreate).HasColumnType("datetime");
            entity.Property(e => e.dUpdate).HasColumnType("datetime");
            entity.Property(e => e.sFname)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sLname)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sPhone)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sSecurityCode)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sUserName)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<TbAtmData>(entity =>
        {
            entity.HasKey(e => e.nAtmID);

            entity.Property(e => e.nAtmID).ValueGeneratedNever();
            entity.Property(e => e.dCreate).HasColumnType("datetime");
            entity.Property(e => e.dUpdate).HasColumnType("datetime");
            entity.Property(e => e.nTotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.sAtmName)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<TbAtmDataHistory>(entity =>
        {
            entity.HasKey(e => e.nItemID);

            entity.Property(e => e.nItemID).ValueGeneratedNever();
            entity.Property(e => e.dCreate).HasColumnType("datetime");
            entity.Property(e => e.nAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.nCustomerID).HasName("PK__TbCustom__5C46112C7661BA40");

            entity.Property(e => e.nCustomerID).ValueGeneratedNever();
            entity.Property(e => e.dCreate).HasColumnType("datetime");
            entity.Property(e => e.dUpdate).HasColumnType("datetime");
            entity.Property(e => e.nTotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.sAddress)
                .HasMaxLength(500)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sCardID)
                .HasMaxLength(13)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sCustomerCode)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sEmail)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sFname)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sLname)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sPhone)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sProfileUrl)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sSecurityCoden)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<TbTransactionHistory>(entity =>
        {
            entity.HasKey(e => e.nHistoryID).HasName("PK__TbTransa__4F05E0139C8D04D8");

            entity.Property(e => e.nHistoryID).ValueGeneratedNever();
            entity.Property(e => e.dCreate).HasColumnType("datetime");
            entity.Property(e => e.nAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.nRemainingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.sRemark)
                .HasMaxLength(500)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<TmDataType>(entity =>
        {
            entity.HasKey(e => e.nTypeID);

            entity.Property(e => e.nTypeID).ValueGeneratedNever();
            entity.Property(e => e.sTypeName)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<TmMasterData>(entity =>
        {
            entity.HasKey(e => e.nMasterID).HasName("PK__TmMaster__20DE0ABFC83C5A8E");

            entity.Property(e => e.nMasterID).ValueGeneratedNever();
            entity.Property(e => e.MasterName)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.nValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.sValue)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<TmMenu>(entity =>
        {
            entity.HasKey(e => e.nMenuID);

            entity.Property(e => e.nMenuID).ValueGeneratedNever();
            entity.Property(e => e.sIcon)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sMenuName)
                .HasMaxLength(120)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.sURL)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.nHeadMenuNavigation).WithMany(p => p.InversenHeadMenuNavigation)
                .HasForeignKey(d => d.nHeadMenu)
                .HasConstraintName("FK_TmMenu_TmMenu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
