using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public partial class LaundryMiddlePlatformContext : DbContext
    {
        public LaundryMiddlePlatformContext()
        {
        }

        public LaundryMiddlePlatformContext(DbContextOptions<LaundryMiddlePlatformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WashingMachine> WashingMachines { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(getConnectionString());

        private String getConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration["ConnectionStrings:DefaultConnection"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDER");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.FinishDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("finishDateTime");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(255)
                    .HasColumnName("orderStatus");

                entity.Property(e => e.StartDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("startDateTime");

                entity.Property(e => e.StoreId).HasColumnName("storeID");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__ORDER__customerI__2E1BDC42");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__ORDER__storeID__2F10007B");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("ORDER DETAILS");

                entity.Property(e => e.OrderDetailId).HasColumnName("orderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__ORDER DET__order__33D4B598");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__ORDER DET__typeI__34C8D9D1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("STORE");

                entity.Property(e => e.StoreId)
                    .ValueGeneratedNever()
                    .HasColumnName("storeID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .HasColumnName("phone");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .HasColumnName("storeName");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("TYPE");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .HasColumnName("typeName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__USER__roleID__25869641");
            });

            modelBuilder.Entity<WashingMachine>(entity =>
            {
                entity.HasKey(e => e.MachineId)
                    .HasName("PK__WASHING __D1ABE00DF03B92FE");

                entity.ToTable("WASHING MACHINE");

                entity.Property(e => e.MachineId).HasColumnName("machineID");

                entity.Property(e => e.MachineName)
                    .HasMaxLength(50)
                    .HasColumnName("machineName");

                entity.Property(e => e.Performmance).HasColumnName("performmance");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StoreId).HasColumnName("storeID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.WashingMachines)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__WASHING M__store__2A4B4B5E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
