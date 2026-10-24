using Microsoft.EntityFrameworkCore;
using QLTV.Models;

namespace QLTV.Data;

public class QLTVDBContext : DbContext
{
    public QLTVDBContext(DbContextOptions<QLTVDBContext> options) : base(options)
    {
    }

    public DbSet<Sach> Sach => Set<Sach>();
    public DbSet<TacGia> TacGia => Set<TacGia>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TacGia>()
            .HasMany(t => t.SachList)
            .WithOne(s => s.TacGia)
            .HasForeignKey(s => s.TacGiaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
