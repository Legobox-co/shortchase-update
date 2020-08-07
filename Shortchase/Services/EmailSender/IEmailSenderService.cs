using System.Collections.Generic;
using System.Threading.Tasks;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public partial interface IEmailSenderService
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="replyTo">ReplyTo address</param>
        /// <param name="replyToName">ReplyTo display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <param name="attachedDownloadId">Attachment download ID (another attachedment)</param>
        /// <param name="headers">Headers</param>
        Task<bool> SendEmail
        (
            EmailAccount emailAccount,
            string subject,
            string body,
            string fromAddress,
            string fromName,
            string toAddress,
            string replyTo = null,
            string replyToName = null,
            IEnumerable<string> bcc = null,
            IEnumerable<string> cc = null,
            string attachmentFilePath = null,
            string attachmentFileName = null,
            IDictionary<string, string> headers = null
        );

        Task<bool> SendConfirmCode(string Recipient, string ConfirmationCode);

        Task<bool> SendForgotPassword(string Recipient, string ConfirmationCode);

        Task<bool> SendRegistrationComplete(string Recipient, string Name, string ConfirmationCode);
        Task<bool> SendEmailConfirmed(string Recipient, string Name);
        Task<bool> SendToUserRewardClaimed(string Recipient, string Name, int Points, decimal EquivalentAmount, string CouponCode);
        Task<bool> SendToUserDiscount(string Recipient, string Name, decimal EquivalentAmount, string CouponCode);

        Task<bool> SendReferralToNewUser(string Recipient, string NameFrom, string ReferralCode);
        Task<bool> SendReferralDiscount(string Recipient, string Name, string DiscountValue);
        Task<bool> SendUserSubscriptionOrder(string Recipient, string Name, string SubscriptionName, string SubscriptionPrice, string PaidValue, string PaymentStatus, string Start, string End, string WalletBalanceBefore, string WalletBalanceAfter, string PaymentType, string ReceiptPDFPath);
        Task<bool> SendUserSubscriptionOrderPaypal(string Recipient, string Name, string SubscriptionName, string SubscriptionPrice, string PaidValue, string PaymentStatus, string Start, string End, string WalletBalanceBefore, string WalletBalanceAfter, string PaymentType, string PaypalPaidValue, string ReceiptPDFPath);
        Task<bool> SendUserBetListingOrder(string Recipient,string Name, Order Order, ICollection<OrderItem> OrderItems, string receiptPDFPath, int TimezoneOffset = 0);

        Task<bool> SendEmailTemplateToUser(string Recipient, string Name, SecondaryEmailTemplate template);

        Task<bool> SendForgotPasswordShortchase(string Recipient, string Name, string Code);

        Task<bool> SendPasswordChangedShortchase(string Recipient, string Name);
        Task<bool> SendContactFormSubmission(string Recipient, int TimezoneOffset, string Name, string Email, string Phone, string Message);

        Task<bool> SendToListingsInOrder(string Recipient, string Name, string Title, string PickType, string Pick, string Odds, decimal Stake, decimal Profit, string Description, string Schedule, string WhereToPlay, string Prediction, string Analysis, string Price, bool HasButton, string OrderId);
    }
}