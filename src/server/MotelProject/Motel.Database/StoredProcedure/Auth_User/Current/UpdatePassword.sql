CREATE PROCEDURE [dbo].[UpdatePassword]
	@_UserId int,
	@_Password nvarchar(150)
AS
	Update Auth.Auth_User set PasswordHash = @_Password where Id = @_UserId
RETURN 0
