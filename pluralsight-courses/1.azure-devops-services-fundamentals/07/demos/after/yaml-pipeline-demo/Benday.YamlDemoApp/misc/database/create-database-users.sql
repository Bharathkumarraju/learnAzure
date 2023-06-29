declare @username nvarchar(max)

select @username = N'yamldemoappuser'

if not exists (SELECT * FROM [sys].[database_principals] where [name] = @username)
BEGIN
	CREATE USER yamldemoappuser
		FOR LOGIN yamldemoappuser
		WITH DEFAULT_SCHEMA = dbo

	EXEC sp_addrolemember N'db_datareader', N'yamldemoappuser'

	EXEC sp_addrolemember N'db_datawriter', N'yamldemoappuser'
END
ELSE
BEGIN
	EXEC sp_addrolemember N'db_datareader', N'yamldemoappuser'

	EXEC sp_addrolemember N'db_datawriter', N'yamldemoappuser'
END