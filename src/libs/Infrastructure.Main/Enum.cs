namespace Infrastructure.Main
{
    public enum StatusMessageEnum
    {
        Successfully = 200,
        Created = 201,
        BadRequest = 400,
        UnauthorizedAccess = 401,
        Forbidden = 403,
        NotFound = 404,
        NotSupported = 415,
        UnexpectedError = 500
    }

    public enum EnableEnum
    {
        Disabled = 0,
        Enabled = 1,
        All = 2,
    }
}
