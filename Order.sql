/*
   niedziela, 3 listopada 201909:54:23
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
CREATE TABLE dbo.[Order]
	(
	Id int NOT NULL,
	PrescriptionId int NOT NULL,
	MedicineId int NOT NULL,
	Date datetime2(7) NOT NULL,
	Amount decimal(18, 2) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.[Order] ADD CONSTRAINT
	PK_Order PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.[Order] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'CONTROL') as Contr_Per 