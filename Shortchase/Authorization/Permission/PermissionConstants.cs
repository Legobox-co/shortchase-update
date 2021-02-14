using System.ComponentModel.DataAnnotations;
using Shortchase.Entities;

namespace Shortchase.Authorization
{
    public enum Permission : ushort
    {
        [Display(Name = "NotSet", Description = "User Has no Role")]
        NotSet = 0, //error condition

        [Display(GroupName = "Basic User", Name = "User", Description = "Basic User Role")]
        User = 1,

        [Display(GroupName = "Basic User", Name = "Capper", Description = "Capper User Role")]
        Capper = 2,

        [Display(GroupName = "Basic User", Name = "Bettor", Description = "Bettor User Role")]
        Bettor = 3,

        [Display(GroupName = "Admin", Name = "Admin", Description = "Admin User Role")]
        Admin = 4,

        [Display(GroupName = "Admin", Name = "Technical Support", Description = "Technical support access role")]
        TechSupport = 5,

        [Display(GroupName = "Admin", Name = "General", Description = "General Admin Role")]
        General = 6,

        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = ushort.MaxValue,
    }

    public static class PermissionConstants
    {
        public const string Prefix = "Permissions";
        public const string ClaimType = "Permissions";
        public const string RefreshClaimType = "PermissionRefresh";
        
        /// <summary>
        /// In Seconds
        /// </summary>
        public const int RefreshTime = 60*60;
        public const bool RefreshEnabled = false;
    }
}