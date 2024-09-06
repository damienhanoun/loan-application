using Acquisition.Api.Domain.Entities;
using Acquisition.Domain.ValueObjects;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Acquisition.Api.Persistence.Database;

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

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection.
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB. This makes
        // a single transaction including side effects from the domain event
        // handlers that are using the same DbContext with Scope lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB. This makes
        // multiple transactions. You will need to handle eventual consistency and
        // compensatory actions in case of failures.
        await _mediator.DispatchDomainEventsAsync(this);

        // After this line runs, all the changes (from the Command Handler and Domain
        // event handlers) performed through the DbContext will be committed
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
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
                    .HasConversion(x => x.Value, x => Email.CreateWithoutValidation(x))
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
}