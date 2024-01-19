using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bancard.DAL.Models;

public partial class DbsistemaCorisContext : DbContext
{
    //private string connStr = "Data Source=(local);Database=dbsistema_coris_dev;User ID=user_mipc;Password=simon1;Trusted_Connection=SSPI;MultipleActiveResultSets=true;Trust Server Certificate=true;";
    private string connStr = "Data Source = 119.8.77.230\\MSSQLSERVER2017;Database=dbsistema_coris_dev;User ID = user_sistemacoris_dev; Password=vU0swfXhnmcWJ23SQiz7p1OMNxjARYLr;Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False;";
    
    public DbsistemaCorisContext()
    {
    }

    public DbsistemaCorisContext(DbContextOptions<DbsistemaCorisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FpgBancard> FpgBancards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(connStr);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FpgBancard>(entity =>
        {
            entity.ToTable("FPG_Bancard");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("amount");
            entity.Property(e => e.AuthorizationNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("authorization_number");
            entity.Property(e => e.Currency)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("currency");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.ExtendedResponseDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("extended_response_description");
            entity.Property(e => e.Response)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("response");
            entity.Property(e => e.ResponseCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("response_code");
            entity.Property(e => e.ResponseDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("response_description");
            entity.Property(e => e.ResponseDetails)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("response_details");
            entity.Property(e => e.SecurityInformation)
                .IsUnicode(false)
                .HasColumnName("security_information");
            entity.Property(e => e.ShopProcessId).HasColumnName("shop_process_id");
            entity.Property(e => e.TicketNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ticket_number");
            entity.Property(e => e.Token)
                .IsUnicode(false)
                .HasColumnName("token");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
