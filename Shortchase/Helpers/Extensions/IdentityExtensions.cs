using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Shortchase.Authorization;

namespace Shortchase.Helpers.Extensions
{
    public static class IdentityExtensions
    {
        public static string GivenName(this IIdentity id)
        {
            var claimsIdentity = (ClaimsIdentity)id;
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string Id(this IIdentity id)
        {
            var claimsIdentity = (ClaimsIdentity)id;
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static ICollection<Permission> Permissions(this IIdentity id)
        {
            var claimsIdentity = (ClaimsIdentity)id;
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == PermissionConstants.ClaimType);
            string Permissions = string.Empty;
            if (claim != null) Permissions = claim.Value;

            if (string.IsNullOrWhiteSpace(Permissions)) return System.Array.Empty<Permission>();
            else return PermissionPackers.UnpackPermissionsFromString(Permissions).ToList();
        }

        public static bool HasPermissions(this IIdentity id, params Permission[] perms)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)id;
                var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == PermissionConstants.ClaimType);
                if (claim == null) return false;
                Permission[] UserPermissions = claim.Value.UnpackPermissionsFromString().ToArray();
                return UserPermissions.UserHasThisPermission(perms);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Guid? Id(this ClaimsPrincipal user)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)user.Identity;
                var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (claim == null) return null;
                else return new Guid(claim.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns true if user has any of the given permissions
        /// </summary>
        /// <param name="id"></param>
        /// <param name="perms">Permissions</param>
        /// <returns></returns>
        public static bool HasAnyPermissions(this IIdentity id, params Permission[] perms)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)id;
                var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == PermissionConstants.ClaimType);
                if (claim == null) return false;
                Permission[] UserPermissions = claim.Value.UnpackPermissionsFromString().ToArray();
                return UserPermissions.UserHasAnyPermissions(perms);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Return true if user has all listed permissions
        /// </summary>
        /// <param name="id"></param>
        /// <param name="perms"></param>
        /// <returns></returns>
        public static bool HasAllPermissions(this IIdentity id, params Permission[] perms)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)id;
                var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == PermissionConstants.ClaimType);
                if (claim == null) return false;
                Permission[] UserPermissions = claim.Value.UnpackPermissionsFromString().ToArray();
                return UserPermissions.UserHasAllPermissions(perms);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SetClaim(this IIdentity currentPrincipal, string key, string value)
        {
            if (!(currentPrincipal is ClaimsIdentity identity))
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null) identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
        }





        #region CART


        public static void SetCartClaim(this IIdentity currentPrincipal, string value)
        {
            if (!(currentPrincipal is ClaimsIdentity identity))
                return;

            // add new claim
            identity.AddClaim(new Claim(CartClaimType.Cart, value));
        }

        public static void RemoveCartClaim(this IIdentity currentPrincipal, string value)
        {
            var claimsIdentity = (ClaimsIdentity)currentPrincipal;
            var existingClaims = claimsIdentity.Claims.Where(x => x.Type == CartClaimType.Cart).ToList();
            if (existingClaims != null && existingClaims.Count > 0)
            {
                foreach (var item in existingClaims)
                {
                    if (item != null)
                    {
                        if (item.Value == value) claimsIdentity.RemoveClaim(item);
                    }
                }
            }
        }

        public static bool IsItemOnCartClaim(this IIdentity currentPrincipal, string value)
        {
            bool result = false;
            var claimsIdentity = (ClaimsIdentity)currentPrincipal;
            var existingClaims = claimsIdentity.Claims.Where(x => x.Type == CartClaimType.Cart).ToList();
            if (existingClaims != null && existingClaims.Count > 0)
            {
                foreach (var item in existingClaims)
                {
                    if (item != null)
                    {
                        if (item.Value == value) result = true;
                    }
                }
            }
            return result;
        }

        public static void CleanCartClaim(this IIdentity currentPrincipal)
        {
            var claimsIdentity = (ClaimsIdentity)currentPrincipal;
            var existingClaims = claimsIdentity.Claims.Where(x => x.Type == CartClaimType.Cart).ToList();
            if (existingClaims != null && existingClaims.Count > 0)
            {
                foreach (var item in existingClaims)
                {
                    if (item != null) claimsIdentity.RemoveClaim(item);
                }
            }
        }

        public static ICollection<string> GetCartItems(this IIdentity id)
        {
            var claimsIdentity = (ClaimsIdentity)id;
            var claims = claimsIdentity.Claims.Where(x => x.Type == CartClaimType.Cart).Select(i => i.Value).ToList();
            return claims;
        }

        public static int GetCartItemsCount(this IIdentity id)
        {
            var claimsIdentity = (ClaimsIdentity)id;
            var claims = claimsIdentity.Claims.Where(x => x.Type == CartClaimType.Cart).Select(i => i.Value).ToList();
            return claims.Count;
        }



        public static void CartV2AddItem(this IIdentity id, HttpRequest request, Guid item)
        {
            string itemId = item.ToString();

        }


        public static void CartV2CheckCartCookie(this IIdentity id, IRequestCookieCollection cookies, HttpResponse response)
        {
            var cartCookie = cookies["Cart"];
            if (cartCookie == null)
            {
                CookieOptions option = new CookieOptions();


                option.Expires = DateTime.UtcNow.AddMilliseconds(10);

                response.Cookies.Append("Cart", "", option);
            }
        }

        #endregion
    }
}