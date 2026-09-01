namespace AuthService.Common;

public static class Constants
{
    // Authentication cookie names.
    public const string AccessTokenCookie = "X-Access-Token";
    public const string RefreshTokenCookie = "X-Refresh-Token";

    // Token expiration durations.
    public const int AccessTokenExpirationMinutes = 1;
    public const int RefreshTokenExpirationDays = 7;
}