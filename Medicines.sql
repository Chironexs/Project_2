/*
   niedziela, 3 listopada 201909:43:35
   User: 
   Server: MSI-GE72-6QF\SQLEXPRESS
   Database: Apteka
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Medicines
	(
	id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(50) NOT NULL,
	Manufacturer nvarchar(50) NULL,
	Price numeric(18, 0) NOT NULL,
	Amount numeric(18, 0) NOT NULL,
	WithPrescription bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Medicines ADD CONSTRAINT
	DF_Medicines_Amount DEFAULT 0 FOR Amount
GO
ALTER TABLE dbo.Medicines ADD CONSTRAINT
	PK_Medicines PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Medicines SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Medicines', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Medicines', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Medicines', 'Object', 'CONTROL') as Contr_Per 