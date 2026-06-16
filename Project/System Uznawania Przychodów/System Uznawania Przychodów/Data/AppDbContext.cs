using Microsoft.EntityFrameworkCore;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<IndividualClient> IndividualClients => Set<IndividualClient>();
    public DbSet<CompanyClient> CompanyClients => Set<CompanyClient>();
    public DbSet<Software> Softwares => Set<Software>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<SoftwareDiscount> SoftwareDiscounts => Set<SoftwareDiscount>();
    public DbSet<SoftwareVersion> SoftwareVersions => Set<SoftwareVersion>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<ContractPayment> ContractPayments => Set<ContractPayment>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<SubscriptionPayment> SubscriptionPayments => Set<SubscriptionPayment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
