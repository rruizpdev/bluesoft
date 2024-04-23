--SELECT * FROM [dbo].[__EFMigrationsHistory]

IF NOT EXISTS(SELECT Id FROM [State])
BEGIN
	INSERT INTO [State]
	(
		[Name]
	)
	VALUES('Amazonas')
	,('Antioquia')
	,('Arauca')
	,('Atlántico')
	,('Bogotá D.C.')
	,('Bolívar')
	,('Boyacá')
	,('Caldas')
	,('Caquetá')
	,('Casanare')
	,('Cauca')
	,('Cesar')
	,('Chocó')
	,('Córdoba')
	,('Cundinamarca')
	,('Guainía')
	,('Guaviare')
	,('Huila')
	,('La Guajira')
	,('Magdalena')
	,('Meta')
	,('Nariño')
	,('Norte de Santander')
	,('Putumayo')
	,('Quindío')
	,('Risaralda')
	,('San Andrés y Providencia')
	,('Santander')
	,('Sucre')
	,('Tolima')
	,('Valle del Cauca')
	,('Vaupés')
	,('Vichada')

	INSERT INTO [dbo].[City]
	(
		 [Name]
		,[StateId]
	)
	SELECT
		'Bogotá D.C.'
		,[Id] as [StateId]
	FROM [dbo].[State]
	WHERE Name = 'Bogotá D.C.'
	UNION ALL
	SELECT
		'Santa Marta'
		,[Id] as [StateId]
	FROM [dbo].[State]
	WHERE Name = 'Magdalena'
	UNION ALL
	SELECT
		'Medellín'
		,[Id] as [StateId]
	FROM [dbo].[State]
	WHERE Name = 'Antioquia'
END

IF NOT EXISTS(SELECT [Id] FROM [dbo].[Branch])
BEGIN
	INSERT INTO [dbo].[Address]
	(
	 [Street1]
	,[CityId]
	,[ZipCode]
	)
	VALUES('CL 14 N° 52A-25',3, NULL)--1 Aeropuerto Olaya
	,('CR 43A N° 34-25 LOCAL 104',3, NULL)--2 Almacentro
	,('CR 43A N° 11A-40',3, NULL) --3 Avenida El Poblado
	,('CR 58 N° 42-125',3, NULL)--4 Edificio Empresas Públicas
	,('CL 29 N° 15-100 LOCAL 106, 107',2, NULL)--5 Ocean Mall
	,('CR 4 N° 26-43 LOCAL 120A, 120B, 120C',2, NULL)--6 Prado Plaza
	,('CR 3 N° 14-10',2, NULL)--7 Santa Marta
	,('CL 127A N° 53A-45 OFICINA 401A',1, NULL)--8 Avenida 127
	,('AV 116 N° 19A-05',1, NULL)--9 Avenida Pepe Sierra
	,('CR 58 N° 127-59 LOCAL 108',1, NULL)--10 Bulevar
	,('AV SUBA N° 105-08',1, NULL)--11 Puente Largo
	,('CR 15 N° 123-30 LC 1-1033',1, NULL)--12 Unicentro Bogotá
	,('CR 15 N° 102-44',1, NULL)--13 Calle 100

	INSERT INTO [dbo].[Branch]
	(
		[Name]
		,[AddressId]
	)
	VALUES('Aeropuerto Olaya',1)
	,('Almacentro',2)
	,('Avenida El Poblado',3)
	,('Edificio Empresas Públicas',4)
	,('Ocean Mall',5)
	,('Prado Plaza',6)
	,('Santa Marta',7)
	,('Avenida 127',8)
	,('Avenida Pepe Sierra',9)
	,('Bulevar',10)
	,('Puente Largo',11)
	,('Unicentro Bogotá',12)
	,('Calle 100',13)
END 

IF NOT EXISTS(SELECT [Id] FROM [Client])
BEGIN
	DECLARE @Person int= 1,@Company int = 2

	INSERT INTO [dbo].[Address]
	(
	 [Street1]
	,[CityId]
	,[ZipCode]
	)
	VALUES ('CR 5 N° 5-25',2, NULL)--14 Anthony Stark
	,('CLL 5 N° 8-23',2, NULL)--15 Naruto Uzumaki
	,('CR4 2 N° 18-23',2, NULL)--16 Microsoft
	,('CR 11 N° 72-59 Piso 11',2, NULL)--17 Wayne Enterprises
	,('CR 7 N° 116-08',2, NULL)--18 Lenovo Colombia

	DECLARE @Current int = 1,@Savings int = 2
	INSERT INTO Client
	(
		 [Name]
		,[Type]
		,[AddressId]
	)
	VALUES('Anthony Stark', @Person,14)
	,('Naruto Uzumaki', @Person,15)
	,('Microsoft', @Company,16)
	,('Wayne Enterprises', @Company,17)
	,('Lenovo', @Company,18)

	INSERT INTO Account
	(
		 [Number]
		,[BranchId]
		,[ClientId]
		,[Active]
		,[Blocked]
		,[CreatedOn]
		,[Type]
		,[Balance]
	)
	VALUES ('0001',7, 1,1,0,GETDATE(),@Savings,1500000000.0)
	,('0002',5, 2,1,0,GETDATE(),@Savings,400000000.0)
	,('0003',12, 4,1,0,GETDATE(),@Current,55000000000.0)

	INSERT INTO [AccountMovement]
	(
		 [AccountId]
		,[Type]
		,[Amount]
		,[CreatedOn]
		,[BranchId]
		,[TransactionCode]
	)
	SELECT
		[Account].[Id]
		,1 --Deposit
		,[Account].[Balance]
		,[Account].[CreatedOn]
		,[Account].[BranchId]
		,NEWID()
	FROM [Account]
END


SELECT
	 [Client].[Id]
	,[Client].[Name]
	,[Client].[Type]
	,[Address].[Street1]
	,[City].[Name] as [City]
	,[State].[Name] as [State]
	,[Address].[ZipCode]
FROM [Client]
INNER JOIN [Address] ON [Client].[AddressId] = [Address].[Id]
INNER JOIN [City] ON [Address].[CityId] = [City].[Id]
INNER JOIN [State] ON [City].[StateId] = [State].[Id]
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

IF OBJECT_ID('[dbo].[FnDateRangeOtherCitiesTransaction]') IS NOT NULL
	DROP FUNCTION [dbo].[FnDateRangeOtherCitiesTransaction]
GO
CREATE FUNCTION [dbo].[FnDateRangeOtherCitiesTransaction]
(
	@from datetime,
	@to datetime
)
RETURNS TABLE
AS
RETURN
(
	SELECT
		 [Account].[ClientId]
		,[AccountBranchAddress].[CityId] as [AccountCityId]
		,[MovementBranchAddress].[CityId] as [TransactionCityId]
		,[Account].[Id] as [AccountId]
		,[Account].[Number] as [AccountNumber]
		,[AccountMovement].[TransactionCode]
		,[AccountMovement].[Type] as [TransactionType]
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
	WHERE [AccountBranchAddress].[CityId] <> [MovementBranchAddress].[CityId]
	AND [AccountMovement].[CreatedOn] BETWEEN @from AND @To
)
GO

/*
	Listado de clientes con el número de transacciones para un mes es particular,
	organizado descendentemente (primero el cliente con mayor # de transacciones en el mes)

*/

SELECT * FROM [dbo].[VW_Transaction]

DECLARE	@from datetime ='20240401',@to datetime ='20240430'

SELECT
	 *
FROM [dbo].[FnClientTransactionCountInRange](@from, @to) as [ClientMonthlyTransaction]
ORDER BY [ClientMonthlyTransaction].[TransactionsQty] DESC

/*
-	Clientes que retiran dinero fuera de la ciudad de origen de la cuenta con el valor total de los retiros realizados superior a $1.000.000.
*/

--SELECT 
-- 	 [Client].[Id]
--	,[Client].[Name]
--	,[Client].[Type]
--FROM [Client]
--INNER JOIN 
--(
--	SELECT
--		 [Account].[ClientId]
--	FROM [dbo].[FnDateRangeOtherCitiesTransaction](@from, @to)
--	GROUP BY Account.ClientId
--) [ClientWithOutCityTransaction] ON [Client].[Id] = [ClientWithOutCityTransaction].[ClientId]
