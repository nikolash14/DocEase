namespace DocEase.Infrastructure.Utility
{
    public static class Message
    {
        #region Authentication
        public static string InvalidUserNameOrPassword = "Username or password is invalid for provided user {0}";
        public static string UserNotAvailable = "User is not available.";
        #endregion

        #region JWT Token.
        public static string InvalidOrExpired = "Invalid or expired refresh token.";
        #endregion


        #region User
        public static string UserCouldNotCreated = "Server Error! User could not created {0}";
        #endregion
    }
}
