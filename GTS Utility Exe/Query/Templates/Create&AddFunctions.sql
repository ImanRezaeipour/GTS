
EXEC dbo.sp_changedbowner @loginame = N'sa', @map = false

if  exists(select * from sysobjects where name='GTS_ASM_CompileAssemblies')
	drop procedure GTS_ASM_CompileAssemblies
	
if  exists(select * from sysobjects where name='GTS_ASM_FillPFile')
	drop procedure GTS_ASM_FillPFile
	
if  exists(select * from sysobjects where name='GTS_ASM_ExecuteSQL')
	drop function GTS_ASM_ExecuteSQL		
	
if  exists(select * from sysobjects where name='GTS_ASM_MiladiToShamsi')
	drop function GTS_ASM_MiladiToShamsi	
	
if  exists(select * from sysobjects where name='GTS_ASM_ShamsiToMiladi')
	drop function GTS_ASM_ShamsiToMiladi	
	
if  exists(select * from sysobjects where name='GTS_ASM_AddShamsiDay')
	drop function GTS_ASM_AddShamsiDay	
		
if  exists(select * from sysobjects where name='GTS_ASM_AddShamsiMonth')
	drop function GTS_ASM_AddShamsiMonth	
	
if  exists(select * from sysobjects where name='GTS_ASM_GetEndOfShamsiMonth')
	drop function GTS_ASM_GetEndOfShamsiMonth
	
if  exists(select * from sysobjects where name='TA_ASM_CalculateFromDateRange')
drop function TA_ASM_CalculateFromDateRange

if  exists(select * from sysobjects where name='TA_ASM_CalculateToDateRange')
drop function TA_ASM_CalculateToDateRange

if exists(SELECT name from sys.assemblies where name='GTSAssemblyXmlSerializers')
	DROP ASSEMBLY GTSAssemblyXmlSerializers		

if exists(SELECT name from sys.assemblies where name='GTSAssembly')
	DROP ASSEMBLY GTSAssembly		

	

EXEC sp_configure 'show advanced options' , '1';
go
reconfigure;
go
EXEC sp_configure 'clr enabled' , '1'
go
reconfigure;
EXEC sp_configure 'show advanced options' , '0';
go
declare @DBName varchar(50)
set @DBName='GTSOrginInstance'

Execute ('ALTER DATABASE ' +  @DBName + ' SET TRUSTWORTHY ON;')
    
    
    
GO
CREATE ASSEMBLY GTSAssembly
   FROM 'E:\Ghadir_Win_Prg\GTS\Construction\Phase1\GTS.Clock\GTS.Clock.Model.SQLServerProject\bin\Debug\GTS.Clock.Model.SQLServerProject.dll'		 
   WITH PERMISSION_SET = UNSAFE;    
GO
--CREATE ASSEMBLY GTSAssemblyXmlSerializers
--    FROM 'E:\Ghadir_Win_Prg\GTS\Construction\Phase1\GTS.Clock\GTS.Clock.Model.SQLServerProject\bin\Debug\GTS.Clock.Model.SQLServerProject.XmlSerializers.dll'
--    WITH PERMISSION_SET = UNSAFE;
GO    



CREATE FUNCTION GTS_ASM_ExecuteSQL(@SQL NVARCHAR(MAX))
RETURNS INT
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.GTS_ASM_ExecuteSQL;
GO    
CREATE FUNCTION GTS_ASM_MiladiToShamsi(@GregoriandDate NVARCHAR(10))
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.GTS_ASM_MiladiToShamsi;
GO    
CREATE FUNCTION GTS_ASM_ShamsiToMiladi(@ShamsiDate NVARCHAR(10))
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.GTS_ASM_ShamsiToMiladi;
GO    
CREATE FUNCTION GTS_ASM_AddShamsiDay(@Year int, @Month int, @Day int, @Value int)
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.GTS_ASM_AddShamsiDay;
GO
CREATE FUNCTION GTS_ASM_AddShamsiMonth(@Year int, @Month int, @Day int, @Value int)
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.GTS_ASM_AddShamsiMonth;
GO
CREATE FUNCTION GTS_ASM_GetEndOfShamsiMonth(@ShamsiDate NVARCHAR(10))
RETURNS NVARCHAR(10)
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.GTS_ASM_GetEndOfShamsiMonth;
GO
CREATE FUNCTION TA_ASM_CalculateFromDateRange(@dt DateTime, @RangeOrder int, @RangeFromMonth int, @RangeFromDay int, @RangeToMonth int, @RangeToDay int, @culture int)
RETURNS DATETIME
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.TA_ASM_CalculateFromDateRange;
GO
CREATE FUNCTION TA_ASM_CalculateToDateRange(@dt DateTime, @RangeOrder int, @RangeFromMonth int, @RangeFromDay int, @RangeToMonth int, @RangeToDay int, @culture int)
RETURNS DATETIME
AS EXTERNAL NAME [GTSAssembly].UserDefinedFunctions.TA_ASM_CalculateToDateRange;
GO