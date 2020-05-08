using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Constants
{
    public static class ExceptionDescriptionConstants
    {
        public static string RootThemesNotFount = "User has no root themes connected with him";
        public static string UserNotFound = "User not found";
        public static string ThemeNotFound = "Theme, connected to this user not found";
        public static string SubThemesNotFound = "Subthemes, connected to this theme not found";
        public static string NoPermissions = "User has no permissions to do this action";
    }
}
