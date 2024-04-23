using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bluesoft.Bank.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street1 = table.Column<string>(type: "varchar(200)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "varchar(20)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_Client",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountMovement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    TransactionCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountMovement_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountMovement_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_BranchId",
                table: "Account",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_ClientId",
                table: "Account",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "UIX_AccountNumber",
                table: "Account",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovement_AccountId",
                table: "AccountMovement",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovement_BranchId",
                table: "AccountMovement",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_AddressId",
                table: "Branch",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_AddressId",
                table: "Client",
                column: "AddressId");

            migrationBuilder.Sql(@"
IF OBJECT_ID('[dbo].[FnClientTransactionCountInRange]') IS NOT NULL
	DROP FUNCTION [dbo].[FnClientTransactionCountInRange]
GO
CREATE FUNCTION [dbo].[FnClientTransactionCountInRange]
(
	@from datetime,
	@to datetime
)
RETURNS TABLE 
AS
RETURN
(
	SELECT 
		 [Client].[Id]
		,[Client].[Name]
		,[Client].[Type]
		,ISNULL([ClientMonthlyTransaction].[TransactionsQty], 0) as [TransactionsQty]
	FROM [Client]
	LEFT JOIN 
	(
		SELECT 
			 [Account].[ClientId]
			,COUNT([AccountMovement].Id) as [TransactionsQty]
		FROM [AccountMovement]
		INNER JOIN [Account] ON [AccountMovement].[AccountId] = [Account].[Id]
		WHERE [AccountMovement].[CreatedOn] BETWEEN @from AND @To
		GROUP BY [Account].[ClientId]
	)as [ClientMonthlyTransaction]
	ON [Client].[Id] = [ClientMonthlyTransaction].[ClientId]
)
GO
CREATE OR ALTER VIEW [dbo].[VW_Transaction]
as
SELECT
	 [AccountMovement].[Id] as [TransactionId]
	,[Account].[ClientId]
	,[AccountBranchAddress].[CityId] as [AccountCityId]
	,[MovementBranchAddress].[CityId] as [TransactionCityId]
	,[Account].[Id] as [AccountId]
	,[Account].[Number] as [AccountNumber]
	,[AccountMovement].[TransactionCode]
	,[AccountMovement].[Type] as [TransactionTypeId]
	,CASE [AccountMovement].[Type]
		WHEN 1 THEN 'Consignación'
		WHEN 2 THEN 'Retiro'
		ELSE '[Desconocido]'
	 END [TransactionType]
	,[AccountMovement].[CreatedOn] as [TransactionDate]
	,[AccountMovement].[Amount] as [Amount]
	,CASE
		WHEN [AccountMovement].[Type] = 1 THEN 1 --Deposit
		ELSE -1
	END * [AccountMovement].[Amount] as [SignedAmount]
FROM [AccountMovement]
INNER JOIN [Account] ON [AccountMovement].[AccountId] = [Account].[Id]
INNER JOIN [Branch]as [AccountBranch] ON [Account].[BranchId] = [AccountBranch].[Id]
INNER JOIN [Address] as [AccountBranchAddress] ON [AccountBranch].[AddressId] = [AccountBranchAddress].[Id]
INNER JOIN [Branch]as [MovementBranch] ON [AccountMovement].[BranchId] = [MovementBranch].[Id]
INNER JOIN [Address] as [MovementBranchAddress] ON [MovementBranch].[AddressId] = [MovementBranchAddress].[Id]
GO

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMovement");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
