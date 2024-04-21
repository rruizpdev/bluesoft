﻿// <auto-generated />
using System;
using Bluesoft.Bank.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bluesoft.Bank.Data.Migrations
{
    [DbContext(typeof(BluesoftBankContext))]
    [Migration("20240421041455_InitDatabase")]
    partial class InitDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<decimal>("Balance")
                        .HasColumnType("money");

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Number")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_Account");

                    b.HasIndex("BranchId");

                    b.HasIndex("ClientId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.AccountMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<Guid>("TransactionCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_AccountMovement");

                    b.HasIndex("AccountId");

                    b.HasIndex("BranchId");

                    b.ToTable("AccountMovement", (string)null);
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("State")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Street1")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id")
                        .HasName("PK_Address");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_Branch");

                    b.HasIndex("AddressId");

                    b.ToTable("Branch", (string)null);
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_Client");

                    b.HasIndex("AddressId");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Account", b =>
                {
                    b.HasOne("Bluesoft.Bank.Data.Entities.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Account_Branch");

                    b.HasOne("Bluesoft.Bank.Data.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Account_Client");

                    b.Navigation("Branch");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.AccountMovement", b =>
                {
                    b.HasOne("Bluesoft.Bank.Data.Entities.Account", "Account")
                        .WithMany("Movements")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_AccountMovement_Account");

                    b.HasOne("Bluesoft.Bank.Data.Entities.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_AccountMovement_Branch");

                    b.Navigation("Account");

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Branch", b =>
                {
                    b.HasOne("Bluesoft.Bank.Data.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Client", b =>
                {
                    b.HasOne("Bluesoft.Bank.Data.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Bluesoft.Bank.Data.Entities.Account", b =>
                {
                    b.Navigation("Movements");
                });
#pragma warning restore 612, 618
        }
    }
}
