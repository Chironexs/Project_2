/*
   niedziela, 3 listopada 201909:50:11
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
CREATE TABLE dbo.Prescription
	(
	Id int NULL,
	CustomerName nvarchar(50) NULL,
	Pesel nvarchar(12) NULL,
	PrescriptionNumber nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Prescription SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Prescription', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Prescription', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Prescription', 'Object', 'CONTROL') as Contr_Per 