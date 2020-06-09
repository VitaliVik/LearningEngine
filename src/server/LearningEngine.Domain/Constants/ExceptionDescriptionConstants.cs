namespace LearningEngine.Domain.Constants
{
    public static class ExceptionDescriptionConstants
    {
        public static string RootThemesNotFount => "User has no root themes connected with him";

        public static string CardNotFound => "Card, connected to this theme not found";

        public static string NoteNotFound => "Note, connected to this theme not found";

        public static string StatisticNotFound => "Statistic, connected to this card not found";

        public static string UserNotFound => "User not found";

        public static string GettingThemeError => "Error while getting theme";

        public static string RegistrationError => "Registration failed";

        public static string TransactionInterrupted => "Transaction was interrupted";

        public static string CreatingThemeError => "Error while creating theme";

        public static string ThemeNotFound => "Theme, connected to this user not found";

        public static string SubThemesNotFound => "Subthemes, connected to this theme not found";

        public static string NoPermissions => "User has no permissions to do this action";
    }
}
