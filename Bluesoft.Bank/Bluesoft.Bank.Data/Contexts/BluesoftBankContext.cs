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

            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

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

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

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
    }
}
