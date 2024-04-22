using Bluesoft.Bank.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
    public virtual DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.Property(e => e.State)
                .IsRequired();
            entity.Property(e => e.City)
                .IsRequired();

            entity.Property(e => e.Street1).HasColumnType("varchar(200)");
            entity.Property(e => e.State).HasColumnType("varchar(40)");
            entity.Property(e => e.City).HasColumnType("varchar(80)");
            entity.Property(e => e.ZipCode).HasColumnType("varchar(15)");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch")
                .HasKey(e => e.Id)
                .HasName("PK_Branch");

            entity.Property(e => e.Name)
                .IsRequired();

            entity.Property(e => e.Name).HasColumnType("varchar(200)");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client")
                .HasKey(e => e.Id)
                .HasName("PK_Client");

            entity.Property(e => e.Name)
                .IsRequired();
            entity.Property(e => e.Type)
                .IsRequired();

            entity.Property(e => e.Name).HasColumnType("varchar(200)");
        });
    }
}
