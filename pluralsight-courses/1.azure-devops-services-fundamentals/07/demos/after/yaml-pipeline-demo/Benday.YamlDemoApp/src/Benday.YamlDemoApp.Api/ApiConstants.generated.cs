using System;

namespace Benday.YamlDemoApp.Api
{
    public static partial class ApiConstants
    {
        public const string DefaultAttributeStatus = "ACTIVE";
        public const string StatusActive = "ACTIVE";
        public const string DefaultStatus = StatusActive;
        public const string StatusDeleted = "DELETED";
        public const string StatusInactive = "INACTIVE";
        public const int UnsavedId = 0;
        public const string ClaimName_UserId = "Local.UserId";
        public const string ClaimName_Role = "role";
        public const string Username_GlobalUser = "GLOBAL_USER";
        public const string ClaimLogicType_DateTimeBased = "TIME-BASED";
        public const string ClaimLogicType_Default = "DEFAULT";
    }
}