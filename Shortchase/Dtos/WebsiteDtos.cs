using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shortchase.Entities;

namespace Shortchase.Dtos
{
    public class SearchFilters
    {
        public string Keyword { get; set; }
        public int? Category { get; set; }
        public int? SubCategory { get; set; }
        public decimal? Odds { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public string SortBy { get; set; }
        public string SortByPrice { get; set; }
        public string SortByPickType { get; set; }
        public string SortByOdds { get; set; }
        public string SortByCurrency { get; set; }
        public int TimeOffset { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }


    public class SearchResults
    {
        public SearchFilters Filters { get; set; }
        public ICollection<SelectListItem> CategoriesOptions { get; set; }
        public ICollection<SelectListItem> SubCategoriesOptions { get; set; }
        public ICollection<SelectListItem> PickTypeOptions { get; set; }
        public ICollection<SelectListItem> CurrencyOptions { get; set; }
        public bool HasSubCategoriesOptions { get; set; }
        public bool HasAnyFilter { get; set; }
        public bool CanSortBy { get; set; }
        public bool IsAuth { get; set; }
        public ICollection<BetListing> Listings { get; set; }
    }


    public class ListingCardCollectionDto
    {
        public bool HasAnyFilter { get; set; }
        public bool IsAuth { get; set; }
        public string root { get; set; }
        public string Currency { get; set; }
        public int Offset { get; set; }
        public ICollection<BetListing> Listings { get; set; }
    }

    public class ViewListingDto
    {
        public bool HasPurchasedBefore { get; set; }
        public bool IsAuth { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsInCart { get; set; }
        public BetListing Listing { get; set; }
        public ICollection<BetListing> RelatedListings { get; set; }
    }

    public class FooterData
    {
        public bool ShowCookieMessage { get; set; }
        public bool IsAdmin { get; set; }
        public WebsiteAddressDto AddressData { get; set; }
        public ICollection<SocialMedia> SocialMediaData { get; set; }
    }

    public class HomeCategoriesDisplayData
    {
        public ICollection<ListingCategory> Categories { get; set; }
    }

    public class UserRatingDto
    {
        public double Rating { get; set; }
    }

    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string Action { get; set; }
    }
    public class ProfilePagination
    {
        public Guid UserId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string Action { get; set; }
    }


    public class ListingReportSubmission
    {
        public string ReportContent { get; set; }
        public string ReportTopic { get; set; }
        public Guid ReportedListingId { get; set; }
    }

    public class ShoppingCartItemDto
    {
        public decimal Price { get; set; }
        public string ListingTitle { get; set; }
        public string SoldBy { get; set; }
        public Guid SoldById { get; set; }
        public Guid ListingId { get; set; }
    }

    public class ShoppingCartDto
    {
        public ICollection<ShoppingCartItemDto> Items { get; set; }
        public decimal TotalBeforeTaxAndFees { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalBeforeDiscount { get; set; }
        public decimal TotalAfterTax { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal EstimatedTax { get; set; }
        public decimal ServiceFeePercent { get; set; }
        public decimal EstimatedTaxPercent { get; set; }
        public decimal WalletFunds { get; set; }
        public decimal WalletOrderDifference { get; set; }
        public decimal WalletBalanceAfterPurchase { get; set; }
        public bool HasEnoughFunds { get; set; }
        public bool CanCheckout { get; set; }
        public decimal FundsNeeded { get; set; }
    }


    public class OrderManagerDto
    {
        public bool IsCapper { get; set; }
        public string root { get; set; }
        public ICollection<OrderManagerOrderDto> Orders { get; set; }

    }


    public class OrderManagerOrderDto
    {

        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderValue { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal ServiceFeePercent { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public Guid OrderUserId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderManagerOrderItemsDto> Items { get; set; }

    }

    public class OrderManagerOrderItemsDto
    {

        public string PostedByName { get; set; }
        public DateTime PostedByDate { get; set; }
        public string Title { get; set; }
        public string Subcategory { get; set; }
        public string Image { get; set; }
        public string UserProfilePicture { get; set; }
        public string Pick { get; set; }
        public string Odds { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }

    }

    public class MessagerModel
    {

        public string root { get; set; }
        public string AdminName { get; set; }
        public string AdminProfilePicture { get; set; }
        public string UserProfilePicture { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Message> Messages { get; set; }
        public int UnreadMessagesCount { get; set; }
        public bool IsAdminOnline { get; set; }

    }


    public class MessageRetrievedDto
    {

        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSent { get; set; }
        public string ParsedDateRead { get; set; }
        public string ParsedDateSent { get; set; }
        public string SentByProfilePicture { get; set; }
        public string SentByName { get; set; }
        public Guid SentById { get; set; }
        public string SentToProfilePicture { get; set; }
        public string SentToName { get; set; }
        public Guid SentToId { get; set; }

    }


    public class MessagesRetrievedDto
    {

        public ICollection<MessageRetrievedDto> Messages { get; set; }

    }


    public class ReferAFriendDto
    {
        public string EmailReferred { get; set; }
        public string UserReferralCode { get; set; }

    }


    public class ReferralRegistrationDto
    {
        public string Email { get; set; }
        public ICollection<Country> CountryOptions { get; set; }
    }


    public class SubscriptionReceiptEmail
    {
        public string SubscriptionName { get; set; }
        public string SubscriptionPrice { get; set; }
        public string PaidValue { get; set; }
        public string PaymentStatus { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string WalletBalanceBefore { get; set; }
        public string WalletBalanceAfter { get; set; }
        public string PaymentType { get; set; }
        public string PaypalPaidValue { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }
    }

    public class OrderReceiptEmail
    {
        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderType { get; set; }
        public string DatePlaced { get; set; }
        public string DatePaid { get; set; }
        public string TotalBeforeTaxAndFee { get; set; }
        public string ServiceFee { get; set; }
        public string ServiceFeePercent { get; set; }
        public string EstimatedTax { get; set; }
        public string EstimatedTaxPercent { get; set; }
        public string TotalAfterTaxAndFee { get; set; }
        public string WalletBalanceBefore { get; set; }
        public string WalletBalanceAfter { get; set; }
    }





    public class PayoutResponseJSON
    {
        public PayoutResponseJSONBatchHeader batch_header { get; set; }
        public PayoutResponseJSONLinksItem[] links { get; set; }

    }
    public class PayoutResponseJSONBatchHeader
    {
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
        public PayoutResponseJSONSenderBatchHeader sender_batch_header { get; set; }

    }
    public class PayoutResponseJSONSenderBatchHeader
    {
        public string sender_batch_id { get; set; }
        public string recipient_type { get; set; }
        public string email_subject { get; set; }
        public string email_message { get; set; }

    }
    public class PayoutResponseJSONLinksItem
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public string encType { get; set; }

    }



    public class PayoutResponseCheckJSON
    {
        public PayoutResponseJSONBatchHeaderCheck batch_header { get; set; }
        public PayoutResponseJSONLinksItem[] links { get; set; }
        public string[] items { get; set; }

    }
    public class PayoutResponseJSONBatchHeaderCheck
    {
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
        public string time_created { get; set; }
        public string time_completed { get; set; }
        public string time_closed { get; set; }
        public string funding_source { get; set; }
        public PayoutResponseJSONMoneyCheck amount { get; set; }
        public PayoutResponseJSONMoneyCheck fees { get; set; }
        public PayoutResponseJSONSenderBatchHeaderCheck sender_batch_header { get; set; }

    }
    public class PayoutResponseJSONSenderBatchHeaderCheck
    {
        public string sender_batch_id { get; set; }
        public string email_subject { get; set; }
        public string email_message { get; set; }

    }
    public class PayoutResponseJSONMoneyCheck
    {
        public string currency { get; set; }
        public string value { get; set; }

    }
    public class AppTitleDto
    {
        public string Value { get; set; }

    }
    public class AppLogoDto
    {
        public string Value { get; set; }

    }

    public class DropdownResultsDto
    {
        public ICollection<ListingCategory> Categories { get; set; }
        public ICollection<ListingSubCategory> SubCategories { get; set; }
        public bool HasPremiumPicks { get; set; }
        public bool HasFreePicks { get; set; }
        public bool HasLivePicks { get; set; }
        public bool CanUserSeeLivePicks { get; set; }
        public ICollection<User> Cappers { get; set; }

    }


    public class DropdownResultsItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }

    public class SendContactMessageDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int TimezoneOffset { get; set; }

    }
}