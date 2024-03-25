using Acquisition.Domain.Entities;
using Acquisition.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Acquisition.Infrastructure;

public class AcquisitionContext : DbContext
{
    public AcquisitionContext(DbContextOptions<AcquisitionContext> options) : base(options)
    {
    }

    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanApplication>().OwnsOne(p =>
                p.InitialLoanWish,
            b =>
            {
                b.Property(p => p.Project)
                    .HasConversion(x => x.Value, x => Project.CreateWithoutValidation(x))
                    .HasMaxLength(100);
                b.Property(p => p.Amount)
                    .HasConversion(x => x.Value, x => Amount.CreateWithoutValidation(x));
                b.Property(p => p.Maturity)
                    .HasConversion(x => x.Value, x => Maturity.CreateWithoutValidation(x));
            });
    }
}