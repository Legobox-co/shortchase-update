using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shortchase.Authorization;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public User ToUser()
        {
            return new User
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                UserName = Username,
                Email = Email,
                PhoneNumber = PhoneNumber
            };
        }
    }

    public class ForgotPwdDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ConfirmationCode { get; set; }

        public string NewPassword { get; set; }
    }

    public class AuthDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }


    public class UserRegistrationCompleteDto
    {
        public bool Result { get; set; }
        public string Code { get; set; }
    }

    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string VerificationType { get; set; }
        public string PhoneCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string Boisterous { get; set; }
        public string ShortchasePro { get; set; }
        public string FullPhone { get; set; }


        public User ToUser()
        {
            var correctBirthDate = BirthDate.Split('-');
            int? countryId = null;
            string phone = null;
            if (!string.IsNullOrWhiteSpace(PhoneCountry) && !string.IsNullOrWhiteSpace(PhoneNumber))
            {
                countryId = Convert.ToInt32(PhoneCountry);
                phone = PhoneNumber;
            }
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = (Guid.NewGuid()).ToString(),
                Email = Email,
                BirthDate = new DateTime(Convert.ToInt32(correctBirthDate[0]), Convert.ToInt32(correctBirthDate[1]), Convert.ToInt32(correctBirthDate[2])),
                PhoneCountryId = countryId,
                PhoneNumber = phone
            };
        }
    }



    public class UserRegisterReferralDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string VerificationType { get; set; }
        public string PhoneCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string Boisterous { get; set; }
        public string ShortchasePro { get; set; }
        public string FullPhone { get; set; }
        public string ReferredByEmail { get; set; }


        public User ToUser()
        {
            var correctBirthDate = BirthDate.Split('-');
            int? countryId = null;
            string phone = null;
            if (!string.IsNullOrWhiteSpace(PhoneCountry) && !string.IsNullOrWhiteSpace(PhoneNumber))
            {
                countryId = Convert.ToInt32(PhoneCountry);
                phone = PhoneNumber;
            }
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = (Guid.NewGuid()).ToString(),
                Email = Email,
                BirthDate = new DateTime(Convert.ToInt32(correctBirthDate[0]), Convert.ToInt32(correctBirthDate[1]), Convert.ToInt32(correctBirthDate[2])),
                PhoneCountryId = countryId,
                PhoneNumber = phone,
                ReferredByEmail = ReferredByEmail
            };
        }
    }

    public class EditUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }


    public class AuthenticatedUserDto
    {
        public User User { get; set; }
        public bool IsBoisterous { get; set; }
        public bool IsProCapper { get; set; }
        public int? TotalItemsInCart { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public Permission CurrentPermission { get; set; }
        public ICollection<Notification> NewNotifications { get; set; }
    }



    public class AccountManagerDto
    {
        public User User { get; set; }
        public int UserRating { get; set; }
        public int UserRatingPoints { get; set; }
        public ICollection<Country> Countries { get; set; }
        public ICollection<RewardsMapping> RewardsOptions { get; set; }
        public DateTime LastUpdatedRewards { get; set; }
        public ICollection<RewardsClaimedMapping> RewardsClaimed { get; set; }
        public Permission CurrentPermission { get; set; }
        public UserSubscription BoisterousActiveSubscription { get; set; }
        public UserSubscription ShortchaseProActiveSubscription { get; set; }
    }


    public class UpdatePersonalInfoDto
    {
        public string BioDescription { get; set; }
        public string Company { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Id { get; set; }
        public string LastName { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public bool PasswordChanged { get; set; }
        public string Phone { get; set; }
        public int PhoneCountry { get; set; }
        public bool PictureChanged { get; set; }
        public string ProfilePicture { get; set; }
        public IFormFile PictureFile { get; set; }
    }


    public class UserListDto
    {
        public ICollection<User> Users { get; set; }
        public ICollection<Country> CountriesOptions { get; set; }
        public int DefaultCountryId { get; set; }
        public string DefaultCountryPhoneCode { get; set; }
    }


    public class BackendUserListDto
    {
        public ICollection<UserListItemDto> Users { get; set; }
        public ICollection<Country> CountriesOptions { get; set; }
        public int DefaultCountryId { get; set; }
        public string DefaultCountryPhoneCode { get; set; }
    }

    public class BackendPaidUserListDto
    {
        public ICollection<PaidUserListItemDto> Users { get; set; }
        public ICollection<Country> CountriesOptions { get; set; }
        public int DefaultCountryId { get; set; }
        public string DefaultCountryPhoneCode { get; set; }
    }

    public class UserListItemDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public int? PhoneCountryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastSeen { get; set; }
        public DateTime DateRegistered { get; set; }
    }


    public class PaidUserListItemDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public int? PhoneCountryId { get; set; }
        public bool IsActive { get; set; }

        public string SubscriptionName { get; set; }
        public bool AutoRenewal { get; set; }
        public Guid ActiveSubscriptionId { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public DateTime SubscriptionEnd { get; set; }
        public string SubscriptionStatus { get; set; }

        public DateTime? LastSeen { get; set; }
        public DateTime DateRegistered { get; set; }
    }

    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Country { get; set; }
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        public User ToUser()
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = (Guid.NewGuid()).ToString(),
                Email = Email,
                BirthDate = DateTime.Now,
                PhoneCountryId = Country,
                PhoneNumber = PhoneNumber
            };
        }
    }



    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Country { get; set; }
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public bool ChangePassword { get; set; }

    }


    public class ManageUserSubscriptionDto
    {
        public User User { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<SubscriptionPlan> SubscriptionPlanOptions { get; set; }
        public UserSubscription ActiveSubscriptionBoisterous { get; set; }
        public UserSubscription ActiveSubscriptionShortchasePro { get; set; }

    }



    public class CreateUserSubscriptionBackendDto
    {
        public Guid UserId { get; set; }
        public Guid GiftById { get; set; }
        public string StartDate { get; set; }
        public int SubscriptionId { get; set; }

    }


    public class UserProfilePageDto
    {
        public Guid Id { get; set; }
        public string Intro { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public DateTime JoinedSince { get; set; }
        public DateTime? LastPosted { get; set; }
        public int Picks { get; set; }
        public int Wins { get; set; }
        public int Rating { get; set; }
        public UserRating UserRating { get; set; }
        public decimal HitRate { get; set; }

        public ICollection<bool?> Last10Picks { get; set; }
        public ICollection<BetListing> Listings { get; set; }
        public bool? IsFollowing { get; set; }
        public bool IsAdmin { get; set; }
    }


    public class UserPayoutList
    {
        public ICollection<UserPayout> Payouts { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }

    }

}