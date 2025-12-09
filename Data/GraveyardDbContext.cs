using Cmentarz.Models;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Data;

public class GraveyardDbContext(DbContextOptions<GraveyardDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Grave> Graves { get; set; }
    public DbSet<GraveStatus> GraveStatuses { get; set; }
    public DbSet<Deceased> Deceaseds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<Grave>()
            .HasOne(grave => grave.Owner)
            .WithMany(user => user.Graves)
            .HasForeignKey(grave => grave.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Grave>()
            .HasOne(grave => grave.Status)
            .WithMany(status => status.Graves)
            .HasForeignKey(grave => grave.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Grave>()
            .HasOne(grave => grave.Deceased)
            .WithOne(deceased => deceased.Grave)
            .HasForeignKey<Deceased>(deceased => deceased.GraveId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}