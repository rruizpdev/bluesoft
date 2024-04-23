using Bluesoft.Bank.Data.Entities;
using Bluesoft.Bank.Data.FunctionResults;
using Bluesoft.Bank.Data.Views;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace Bluesoft.Bank.Data.Contexts;

public class BluesoftBankContext 
    : DbContext
{
    public BluesoftBankContext(
        DbContextOptions<BluesoftBankContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<AccountMovement> AccountMovements { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Branch> Branches { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<State> States { get; set; }
    public virtual DbSet<TransactionView> Transactions { get; set; }

    public virtual DbSet<ClientTransactionsCount> TransactionsCountsInRange { get; set; }

    public IQueryable<ClientTransactionsCount> FnClientTransactionCountInRange(DateTime from, DateTime to)
        => FromExpression(() => FnClientTransactionCountInRange(from, to));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionView>(entity =>
        {
            entity.ToView("VW_Transaction")
                .HasKey(t=> t.TransactionId);
        });

        modelBuilder.Entity<ClientTransactionsCount>(entity =>
        {
            entity.HasKey(c=> c.Id);
        });

        modelBuilder.HasDbFunction(typeof(BluesoftBankContext)
            .GetMethod(nameof(FnClientTransactionCountInRange), new[] { typeof(DateTime), typeof(DateTime) }))
            .HasName("FnClientTransactionCountInRange"); ;


        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account")
                .HasKey(e => e.Id)
                .HasName("PK_Account");

            entity.Property(e => e.Number)
                .HasColumnType("varchar(20)")
                .IsRequired();


            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .IsRequired();
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .IsRequired();

            entity.HasIndex(a => a.Number)
                .HasDatabaseName("UIX_AccountNumber")
                .IsUnique();

            entity.HasOne(account => account.Client).WithMany()
                .HasForeignKey(account => account.ClientId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Account_Client");

            entity.HasOne(account => account.Branch).WithMany()
                .HasForeignKey(account => account.BranchId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Account_Branch");
        });

        modelBuilder.Entity<AccountMovement>(entity =>
        {
            entity.ToTable("AccountMovement")
                .HasKey(e => e.Id)
                .HasName("PK_AccountMovement");

            entity.Property(e => e.Type)
                .IsRequired();
            entity.Property(e => e.TransactionCode)
                .IsRequired();

            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .IsRequired();
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .IsRequired();

            entity.HasOne(movement => movement.Account)
                .WithMany(a => a.Movements)
                .HasForeignKey(movement => movement.AccountId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_AccountMovement_Account");

            entity.HasOne(movement => movement.Branch)
                .WithMany()
                .HasForeignKey(movement => movement.BranchId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_AccountMovement_Branch");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address")
                .HasKey(e => e.Id)
                .HasName("PK_Address");

            entity.Property(e => e.Street1)
                .IsRequired();
            
            entity.Property(e => e.Street1).HasColumnType("varchar(200)");
            
            entity.Property(e => e.ZipCode).HasColumnType("varchar(15)");

            entity.HasOne(address => address.City)
                .WithMany()
                .HasForeignKey(address => address.CityId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Address_City");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City")
                .HasKey(e => e.Id)
                .HasName("PK_City");

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();
            entity.HasOne(city => city.State)
                .WithMany()
                .HasForeignKey(city => city.StateId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_City_State");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State")
                .HasKey(e => e.Id)
                .HasName("PK_State");

            entity.Property(e => e.Name)
                .HasColumnType("varchar(30)")
                .IsRequired();
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch")
                .HasKey(e => e.Id)
                .HasName("PK_Branch");

            entity.Property(e => e.Name)
                .HasColumnType("varchar(200)")
                .IsRequired();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client")
                .HasKey(e => e.Id)
                .HasName("PK_Client");

            entity.Property(e => e.Name)
                .HasColumnType("varchar(200)")
                .IsRequired();
            entity.Property(e => e.Type)
                .IsRequired();
        });
    }
}
