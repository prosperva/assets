SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[BranchID] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DivisionID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Branches] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_DivisionID] ON [dbo].[Branches] 
(
	[DivisionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Divisions](
	[DivisionID] [int] IDENTITY(1,1) NOT NULL,
	[DivisionName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_dbo.Divisions] PRIMARY KEY CLUSTERED 
(
	[DivisionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_EmployeeID] ON [dbo].[Divisions] 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OSSystems](
	[OSSystemID] [int] IDENTITY(1,1) NOT NULL,
	[OSSystemname] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.OSSystems] PRIMARY KEY CLUSTERED 
(
	[OSSystemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkStationTypes](
	[WorkStationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[WorkStationTypeName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.WorkStationTypes] PRIMARY KEY CLUSTERED 
(
	[WorkStationTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[testing](
	[AssetID] [int] IDENTITY(1,1) NOT NULL,
	[AssetTag] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PONumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SerialNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReceivedDate] [datetime] NULL,
	[DeployedDate] [datetime] NULL,
	[StartWarrantyDate] [datetime] NULL,
	[EndWarrantyDate] [datetime] NULL,
	[WarrantyTerm] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PartNumnber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MakeModel] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Color] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Size] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[OSSystemID] [int] NULL,
	[DivisionID] [int] NULL,
	[WorkStationTypeID] [int] NULL,
	[AssetStatusID] [int] NULL,
	[BranchID] [int] NULL,
	[EmployeeID] [int] NULL,
	[AssetTypeID] [int] NULL,
	[BuildingID] [int] NULL,
	[Floor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_getAssetSoftwareByDivision]
	@divisionid int
AS

DECLARE @Sql NVARCHAR(MAX) = 'SELECT Divisions.DivisionID, Divisions.DivisionName, Assets.AssetID, Assets.AssetTag, Assets.PONumber, Assets.MakeModel, Assets.EmployeeID, 
Employees.EmployeeFirstName + '' '' + Employees.EmployeeLastName as UserName, Assets.BuildingID, Buildings.BuildingName, Softwares.softwareName
FROM Assets,AssetSoftwares,Softwares,Divisions, Employees,Buildings
where Assets.AssetID = AssetSoftwares.assetid and AssetSoftwares.softwareid = Softwares.SoftwareID
and Assets.DivisionID = Divisions.DivisionID and Assets.EmployeeID = Employees.EmployeeID and Assets.BuildingID = Buildings.BuildingID'

IF @divisionid IS NOT NULL

BEGIN
  SET @Sql += ' and assets.divisionid = @divisionid'
END

EXEC sp_executesql @sql, N'@divisionid int',@divisionid=@divisionid;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_getAssetsByEmployee]
	@divisionid int,
	@employeeid int
AS

DECLARE @Sql NVARCHAR(MAX) = 'SELECT Assets.AssetTag, Assets.PONumber, Assets.SerialNumber, Assets.MakeModel, Assets.DivisionID, Assets.WorkStationTypeID, Assets.AssetStatusID, 
Assets.BranchID, Assets.EmployeeID, Assets.AssetTypeID, Assets.BuildingID,Assets.Floor, Assets.Address,
Buildings.BuildingName,AssetTypes.AssetTypeName,AssetStatus.AssetStatusName,Branches.BranchName,
Divisions.DivisionName,
Employees.EmployeeFirstName + '' '' + Employees.EmployeeLastName as UserName,
WorkStationTypes.WorkStationTypeName 
FROM Assets
left outer join assetstatus on assets.AssetStatusID = assetstatus.assetstatusid
left outer join assettypes on assets.AssetTypeID = AssetTypes.AssetTypeID
left outer join Branches on assets.BranchID = branches.BranchID 
left outer join Buildings on assets.BuildingID = Buildings.BuildingID
left outer join Divisions on assets.DivisionID = Divisions.DivisionID
left outer join employees on assets.EmployeeID = Employees.EmployeeID
left outer join WorkStationTypes on assets.WorkStationTypeID = WorkStationTypes.WorkStationTypeID'

IF @divisionid IS NOT NULL
BEGIN
  SET @Sql += ' where assets.divisionid = @divisionid'
END

IF @employeeid IS NOT NULL AND @divisionid IS NOT NULL
BEGIN
  SET @Sql += ' and assets.employeeid = @employeeid'
END

IF @employeeid IS NOT NULL AND @divisionid IS NULL
BEGIN
  SET @Sql += ' where assets.employeeid = @employeeid'
END

EXEC sp_executesql @sql, N'@divisionid int,@employeeid int',@divisionid=@divisionid,@employeeid=@employeeid;



GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_getAssetsByDivision]
	@divisionid int,
	@assettypeid int
AS

DECLARE @Sql NVARCHAR(MAX) = 'SELECT Assets.AssetTag, Assets.PONumber, Assets.SerialNumber, Assets.MakeModel, Assets.DivisionID, Assets.WorkStationTypeID, Assets.AssetStatusID, 
Assets.BranchID, Assets.EmployeeID, Assets.AssetTypeID, Assets.BuildingID,Assets.Floor, Assets.Address,
Buildings.BuildingName,AssetTypes.AssetTypeName,AssetStatus.AssetStatusName,Branches.BranchName,
Divisions.DivisionName,
Employees.EmployeeFirstName + '' '' + Employees.EmployeeLastName as UserName,
WorkStationTypes.WorkStationTypeName 
FROM Assets
left outer join assetstatus on assets.AssetStatusID = assetstatus.assetstatusid
left outer join assettypes on assets.AssetTypeID = AssetTypes.AssetTypeID
left outer join Branches on assets.BranchID = branches.BranchID 
left outer join Buildings on assets.BuildingID = Buildings.BuildingID
left outer join Divisions on assets.DivisionID = Divisions.DivisionID
left outer join employees on assets.EmployeeID = Employees.EmployeeID
left outer join WorkStationTypes on assets.WorkStationTypeID = WorkStationTypes.WorkStationTypeID'

IF @divisionid IS NOT NULL
BEGIN
  SET @Sql += ' where assets.divisionid = @divisionid'
END

IF @assettypeid IS NOT NULL AND @divisionid IS NOT NULL
BEGIN
  SET @Sql += ' and assets.AssetTypeID = @assettypeid'
END

IF @assettypeid IS NOT NULL AND @divisionid IS NULL
BEGIN
  SET @Sql += ' where assets.AssetTypeID = @assettypeid'
END

EXEC sp_executesql @sql, N'@divisionid int,@assettypeid int',@divisionid=@divisionid,@assettypeid=@assettypeid;


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getAllSoftwares] @softwareid int
AS

DECLARE @Sql NVARCHAR(MAX) = 'SELECT * FROM softwares'

IF @softwareid IS NOT NULL
BEGIN
  SET @Sql += ' where softwareid = @softwareid'
END

EXEC sp_executesql @sql, N'@softwareid int', @softwareid = @softwareid;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoftwarePerAssets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SoftwareID] [int] NOT NULL,
	[AssetID] [int] NOT NULL,
	[SerialKey] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DatePushed] [datetime] NULL,
 CONSTRAINT [PK_dbo.SoftwarePerAssets] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_AssetID] ON [dbo].[SoftwarePerAssets] 
(
	[AssetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_SoftwareID] ON [dbo].[SoftwarePerAssets] 
(
	[SoftwareID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetSoftwares](
	[assetid] [int] NOT NULL,
	[softwareid] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AssetSoftwares] PRIMARY KEY CLUSTERED 
(
	[assetid] ASC,
	[softwareid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_assetid] ON [dbo].[AssetSoftwares] 
(
	[assetid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_softwareid] ON [dbo].[AssetSoftwares] 
(
	[softwareid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Softwares](
	[SoftwareID] [int] IDENTITY(1,1) NOT NULL,
	[PONumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[softwareName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[InstallNumber] [int] NOT NULL,
	[LicenceNumber] [int] NOT NULL,
	[NumberOfLicenses] [int] NOT NULL,
	[Assisted] [bit] NOT NULL,
	[SerialNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.Softwares] PRIMARY KEY CLUSTERED 
(
	[SoftwareID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeFirstName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmployeeLastName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Floor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UserID] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmployeeTitle] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DivisionID] [int] NULL,
	[Manager] [int] NULL,
	[BranchID] [int] NULL,
	[BuildingID] [int] NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_BranchID] ON [dbo].[Employees] 
(
	[BranchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_BuildingID] ON [dbo].[Employees] 
(
	[BuildingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_DivisionID] ON [dbo].[Employees] 
(
	[DivisionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_Manager] ON [dbo].[Employees] 
(
	[Manager] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[BuildingID] [int] IDENTITY(1,1) NOT NULL,
	[BuildingName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BuildingAddresss] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.Buildings] PRIMARY KEY CLUSTERED 
(
	[BuildingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetTypes](
	[AssetTypeID] [int] IDENTITY(1,1) NOT NULL,
	[AssetTypeName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[WarantyTerm] [int] NULL,
 CONSTRAINT [PK_dbo.AssetTypes] PRIMARY KEY CLUSTERED 
(
	[AssetTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assets](
	[AssetID] [int] IDENTITY(1,1) NOT NULL,
	[AssetTag] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PONumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SerialNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReceivedDate] [datetime] NULL,
	[DeployedDate] [datetime] NULL,
	[StartWarrantyDate] [datetime] NULL,
	[EndWarrantyDate] [datetime] NULL,
	[WarrantyTerm] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PartNumnber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MakeModel] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Color] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Size] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[OSSystemID] [int] NULL,
	[DivisionID] [int] NULL,
	[WorkStationTypeID] [int] NULL,
	[AssetStatusID] [int] NULL,
	[BranchID] [int] NULL,
	[EmployeeID] [int] NULL,
	[AssetTypeID] [int] NULL,
	[BuildingID] [int] NULL,
	[Floor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.Assets] PRIMARY KEY CLUSTERED 
(
	[AssetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_AssetStatusID] ON [dbo].[Assets] 
(
	[AssetStatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_AssetTypeID] ON [dbo].[Assets] 
(
	[AssetTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_BranchID] ON [dbo].[Assets] 
(
	[BranchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_BuildingID] ON [dbo].[Assets] 
(
	[BuildingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_DivisionID] ON [dbo].[Assets] 
(
	[DivisionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_EmployeeID] ON [dbo].[Assets] 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_OSSystemID] ON [dbo].[Assets] 
(
	[OSSystemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_WorkStationTypeID] ON [dbo].[Assets] 
(
	[WorkStationTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetStatus](
	[AssetStatusID] [int] IDENTITY(1,1) NOT NULL,
	[AssetStatusName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AssetStatus] PRIMARY KEY CLUSTERED 
(
	[AssetStatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
ALTER TABLE [dbo].[Branches]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Branches_dbo.Divisions_DivisionID] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[Divisions] ([DivisionID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Branches] CHECK CONSTRAINT [FK_dbo.Branches_dbo.Divisions_DivisionID]
GO
ALTER TABLE [dbo].[Divisions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Divisions_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Divisions] CHECK CONSTRAINT [FK_dbo.Divisions_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[SoftwarePerAssets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SoftwarePerAssets_dbo.Assets_AssetID] FOREIGN KEY([AssetID])
REFERENCES [dbo].[Assets] ([AssetID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SoftwarePerAssets] CHECK CONSTRAINT [FK_dbo.SoftwarePerAssets_dbo.Assets_AssetID]
GO
ALTER TABLE [dbo].[SoftwarePerAssets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SoftwarePerAssets_dbo.Softwares_SoftwareID] FOREIGN KEY([SoftwareID])
REFERENCES [dbo].[Softwares] ([SoftwareID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SoftwarePerAssets] CHECK CONSTRAINT [FK_dbo.SoftwarePerAssets_dbo.Softwares_SoftwareID]
GO
ALTER TABLE [dbo].[AssetSoftwares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AssetSoftwares_dbo.Assets_assetid] FOREIGN KEY([assetid])
REFERENCES [dbo].[Assets] ([AssetID])
GO
ALTER TABLE [dbo].[AssetSoftwares] CHECK CONSTRAINT [FK_dbo.AssetSoftwares_dbo.Assets_assetid]
GO
ALTER TABLE [dbo].[AssetSoftwares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AssetSoftwares_dbo.Softwares_softwareid] FOREIGN KEY([softwareid])
REFERENCES [dbo].[Softwares] ([SoftwareID])
GO
ALTER TABLE [dbo].[AssetSoftwares] CHECK CONSTRAINT [FK_dbo.AssetSoftwares_dbo.Softwares_softwareid]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employees_dbo.Branches_BranchID] FOREIGN KEY([BranchID])
REFERENCES [dbo].[Branches] ([BranchID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_dbo.Employees_dbo.Branches_BranchID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employees_dbo.Buildings_BuildingID] FOREIGN KEY([BuildingID])
REFERENCES [dbo].[Buildings] ([BuildingID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_dbo.Employees_dbo.Buildings_BuildingID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employees_dbo.Divisions_DivisionID] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[Divisions] ([DivisionID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_dbo.Employees_dbo.Divisions_DivisionID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employees_dbo.Employees_Manager] FOREIGN KEY([Manager])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_dbo.Employees_dbo.Employees_Manager]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.AssetStatus_AssetStatusID] FOREIGN KEY([AssetStatusID])
REFERENCES [dbo].[AssetStatus] ([AssetStatusID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.AssetStatus_AssetStatusID]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.AssetTypes_AssetTypeID] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.AssetTypes_AssetTypeID]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.Branches_BranchID] FOREIGN KEY([BranchID])
REFERENCES [dbo].[Branches] ([BranchID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.Branches_BranchID]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.Buildings_BuildingID] FOREIGN KEY([BuildingID])
REFERENCES [dbo].[Buildings] ([BuildingID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.Buildings_BuildingID]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.Divisions_DivisionID] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[Divisions] ([DivisionID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.Divisions_DivisionID]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.OSSystems_OSSystemID] FOREIGN KEY([OSSystemID])
REFERENCES [dbo].[OSSystems] ([OSSystemID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.OSSystems_OSSystemID]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assets_dbo.WorkStationTypes_WorkStationTypeID] FOREIGN KEY([WorkStationTypeID])
REFERENCES [dbo].[WorkStationTypes] ([WorkStationTypeID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_dbo.Assets_dbo.WorkStationTypes_WorkStationTypeID]
GO
