declare @username nvarchar(max)

select @username = N'yamldemoappuser'

if not exists (select * from sys.sql_logins where [name] = @username)
BEGIN
	CREATE LOGIN [yamldemoappuser] 
	WITH PASSWORD = 'Pa$$word' 
END

