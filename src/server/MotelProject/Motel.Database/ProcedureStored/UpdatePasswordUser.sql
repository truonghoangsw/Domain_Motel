CREATE PROCEDURE [dbo].[UpdatePasswordUser]
	@_UserId int,
	@_PasswordHash nvarchar(Max)
AS
	Update  Auth.Auth_User set PasswordHash = @_PasswordHash 
	where Id = @_UserId
RETURN 0
