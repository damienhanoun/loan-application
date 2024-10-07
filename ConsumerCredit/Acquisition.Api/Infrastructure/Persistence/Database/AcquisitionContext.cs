using Acquisition.Api.Domain.Entities;
using Acquisition.Domain.ValueObjects;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Acquisition.Api.Infrastructure.Persistence.Database;

public class AcquisitionContext : DbContext
{
    private readonly IMediator _mediator;

    public AcquisitionContext(DbContextOptions<AcquisitionContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
    public DbSet<LoanOffer> LoanOffers => Set<LoanOffer>();
    public DbSet<LoanContract> LoanContracts => Set<LoanContract>();
    public DbSet<Resource> Resources => Set<Resource>();

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BuildDomainObjects(modelBuilder);
        BuildResources(modelBuilder);
    }

    private static void BuildDomainObjects(ModelBuilder modelBuilder)
    {
        BuildLoanApplication(modelBuilder);
        BuildLoanContract(modelBuilder);
        BuildLoanOffer(modelBuilder);
    }

    private static void BuildLoanApplication(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanApplication>().Ignore(c => c.DomainEvents);

        modelBuilder.Entity<LoanApplication>()
            .HasKey(k => k.Id);
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
        modelBuilder.Entity<LoanApplication>().OwnsOne(p =>
                p.UserInformation,
            b =>
            {
                b.Property(p => p.Email)
                    .HasConversion(x => x!.Value, x => Email.CreateWithoutValidation(x))
                    .HasMaxLength(100);
            });
    }

    private static void BuildLoanOffer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanOffer>().Ignore(c => c.DomainEvents);
        modelBuilder.Entity<LoanOffer>()
            .HasKey(k => k.Id);

        modelBuilder.Entity<LoanOffer>()
            .Property(p => p.Amount)
            .HasConversion(x => x.Value, x => Amount.CreateWithoutValidation(x));
        modelBuilder.Entity<LoanOffer>()
            .Property(p => p.Maturity)
            .HasConversion(x => x.Value, x => Maturity.CreateWithoutValidation(x));
        modelBuilder.Entity<LoanOffer>()
            .Property(p => p.MonthlyAmount)
            .HasConversion(x => x.Value, x => Amount.CreateWithoutValidation(x));
    }

    private static void BuildLoanContract(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanContract>().Ignore(c => c.DomainEvents);
        modelBuilder.Entity<LoanContract>()
            .HasKey(k => k.Id);
        modelBuilder.Entity<LoanContract>()
            .Property(p => p.LoanApplicationId);
    }

    private static void BuildResources(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>().HasData(
            new Resource { Id = 1, Type = "maturity", Value = "6" },
            new Resource { Id = 2, Type = "maturity", Value = "12" },
            new Resource { Id = 3, Type = "maturity", Value = "24" },
            new Resource { Id = 4, Type = "maturity", Value = "36" },
            new Resource { Id = 5, Type = "maturity", Value = "48" },
            new Resource { Id = 6, Type = "maturity", Value = "72" },
            new Resource { Id = 7, Type = "maturity", Value = "84" },
            new Resource { Id = 8, Type = "amount", Value = "1000" },
            new Resource { Id = 9, Type = "amount", Value = "1500" },
            new Resource { Id = 10, Type = "amount", Value = "2000" },
            new Resource { Id = 11, Type = "amount", Value = "2500" },
            new Resource { Id = 12, Type = "amount", Value = "3000" },
            new Resource { Id = 13, Type = "amount", Value = "3500" },
            new Resource { Id = 14, Type = "amount", Value = "4000" },
            new Resource { Id = 15, Type = "amount", Value = "4500" },
            new Resource { Id = 16, Type = "amount", Value = "5000" },
            new Resource { Id = 17, Type = "amount", Value = "5500" },
            new Resource { Id = 18, Type = "amount", Value = "6000" },
            new Resource { Id = 19, Type = "amount", Value = "6500" },
            new Resource { Id = 20, Type = "amount", Value = "7000" },
            new Resource { Id = 21, Type = "amount", Value = "7500" },
            new Resource { Id = 22, Type = "amount", Value = "8000" },
            new Resource { Id = 23, Type = "amount", Value = "8500" },
            new Resource { Id = 24, Type = "amount", Value = "9000" },
            new Resource { Id = 25, Type = "amount", Value = "9500" },
            new Resource { Id = 26, Type = "amount", Value = "+10000" },
            new Resource { Id = 27, Type = "project", Value = "Wedding" },
            new Resource { Id = 28, Type = "project", Value = "Home Renovation" },
            new Resource { Id = 29, Type = "project", Value = "Vacation" },
            new Resource { Id = 30, Type = "project", Value = "Debt Consolidation" },
            new Resource { Id = 31, Type = "project", Value = "Car Purchase" }
        );
    }
}

public class Resource
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}