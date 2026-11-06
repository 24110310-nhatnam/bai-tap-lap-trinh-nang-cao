using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using MySql.Data.Entity;
using QuanLySach.Entity.Entities;

namespace QuanLySach.Entity.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class QuanLySachContext : DbContext
    {
        public QuanLySachContext()
            : base("name=QuanLySachDB")
        {
        }

        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<Sach> Sachs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remove PluralizingTableNameConvention so table names match entity names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configure decimal precision via reflection on DecimalPrecisionAttribute
            modelBuilder.Properties<decimal>().Configure(config =>
            {
                var attr = config.ClrPropertyInfo.GetCustomAttribute<DecimalPrecisionAttribute>();
                if (attr != null)
                    config.HasPrecision(attr.Precision, attr.Scale);
                else
                    config.HasPrecision(18, 2);
            });

            // TheLoai config
            modelBuilder.Entity<TheLoai>()
                .HasKey(t => t.MaLoai)
                .Property(t => t.MaLoai).HasMaxLength(10).IsRequired();

            modelBuilder.Entity<TheLoai>()
                .Property(t => t.TenLoai).HasMaxLength(100).IsRequired();

            // Sach config
            modelBuilder.Entity<Sach>()
                .HasKey(s => s.MaSach)
                .Property(s => s.MaSach).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Sach>()
                .Property(s => s.TenSach).HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Sach>()
                .Property(s => s.MaLoai).HasMaxLength(10).IsRequired();

            modelBuilder.Entity<Sach>()
                .Property(s => s.DonGia).HasPrecision(18, 2);

            // Relationship
            modelBuilder.Entity<Sach>()
                .HasRequired(s => s.TheLoai)
                .WithMany(t => t.DanhSachSach)
                .HasForeignKey(s => s.MaLoai)
                .WillCascadeOnDelete(false);
        }
    }
}
