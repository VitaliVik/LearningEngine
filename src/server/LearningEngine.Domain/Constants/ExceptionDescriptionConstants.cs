using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Constants
{
    public static class ExceptionDescriptionConstants
    {
        public static string RootThemesNotFount = "User has no root themes connected with him";
        public static string ThemeNotFound = "Theme, connected to this user not found";
        public static string NoRightsForDeleting = "User has no rights to delete this theme";
        public static string NoPermissions = "User has no permissions to get this theme";
        public static string NoRightsForEditing = "User has no rights to edit this theme";
    }
}
