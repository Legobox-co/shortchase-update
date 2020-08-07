using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Shortchase.Entities;
using Shortchase.Helpers;

namespace Shortchase.Services
{
    public partial class EmailSenderService : IEmailSenderService
    {
        private readonly IErrorLogService errorLogService;
        private readonly IEmailTemplateService emailTemplateService;
        private readonly IEmailConfigService emailConfigService;

        public EmailSenderService
        (
            IErrorLogService errorLogService,
            IEmailTemplateService emailTemplateService,
            IEmailConfigService emailConfigService
        )
        {
            this.errorLogService = errorLogService;
            this.emailTemplateService = emailTemplateService;
            this.emailConfigService = emailConfigService;
        }

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
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <param name="attachedDownloadId">Attachment download ID (another attachedment)</param>
        /// <param name="headers">Headers</param>
        public virtual async Task<bool> SendEmail
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
        )
        {
            var message = new MailMessage();

            try
            {
                if (emailAccount == null) throw new Exception("email account cannot be null");
                //from, to, reply to
                message.From = new MailAddress(fromAddress, fromName);
                if (toAddress != null)
                {
                    message.To.Add(toAddress.Trim());
                }

                if (!String.IsNullOrEmpty(replyTo))
                {
                    message.ReplyToList.Add(new MailAddress(replyTo, replyToName));
                }

                //BCC
                if (bcc != null)
                {
                    foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                    {
                        message.Bcc.Add(address.Trim());
                    }
                }

                //CC
                if (cc != null)
                {
                    foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                    {
                        message.CC.Add(address.Trim());
                    }
                }

                //content
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                //headers
                if (headers != null)
                    foreach (var header in headers)
                    {
                        message.Headers.Add(header.Key, header.Value);
                    }

                //create the file attachment for this e-mail message
                if (!string.IsNullOrEmpty(attachmentFilePath) &&
                    File.Exists(attachmentFilePath))
                {
                    var attachment = new Attachment(attachmentFilePath);
                    attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentFilePath);
                    attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentFilePath);
                    attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentFilePath);
                    if (!string.IsNullOrEmpty(attachmentFileName))
                    {
                        attachment.Name = attachmentFileName;
                    }
                    message.Attachments.Add(attachment);
                }

                //send email
                String userName = emailAccount.Username;
                String password = emailAccount.Password;
                SmtpClient client = new SmtpClient
                {
                    Host = emailAccount.Host,
                    Credentials = new System.Net.NetworkCredential(userName, password),
                    Port = emailAccount.Port,
                    EnableSsl = emailAccount.EnableSsl
                };
                client.Send(message);
                client.Dispose();
                return true;
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e)) throw;
                return false;
            }
        }

        public async Task<bool> SendConfirmCode(string Recipient, string ConfirmationCode)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.RegistrationCode).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, ConfirmationCode);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }

        public async Task<bool> SendForgotPassword(string Recipient, string ConfirmationCode)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.ForgotPassword).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, ConfirmationCode);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }



        public async Task<bool> SendRegistrationComplete(string Recipient, string Name, string ConfirmationCode)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.RegistrationComplete).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, ConfirmationCode, DateTime.Now.Year, EmailTemplate.Subject);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }



        public async Task<bool> SendEmailConfirmed(string Recipient, string Name)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.EmailConfirmed).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, EmailTemplate.Subject);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }


        public async Task<bool> SendToUserRewardClaimed(string Recipient, string Name, int Points, decimal EquivalentAmount, string CouponCode)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.UserRewardClaimed).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, EmailTemplate.Subject, Points, EquivalentAmount.ToString(), CouponCode);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }

        public async Task<bool> SendToUserDiscount(string Recipient, string Name, decimal EquivalentAmount, string CouponCode)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.UserDiscount).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, EmailTemplate.Subject, EquivalentAmount.ToString(), CouponCode);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }



        public async Task<bool> SendReferralToNewUser(string Recipient, string NameFrom, string ReferralCode)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.SendReferral).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, NameFrom, DateTime.Now.Year, EmailTemplate.Subject, ReferralCode, Recipient, Security.Encrypt(ReferralCode));
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }
        public async Task<bool> SendReferralDiscount(string Recipient, string Name, string DiscountValue)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.SendReferralDiscount).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, EmailTemplate.Subject, DiscountValue);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }

        public async Task<bool> SendUserSubscriptionOrder(string Recipient, string Name, string SubscriptionName, string SubscriptionPrice, string PaidValue, string PaymentStatus, string Start, string End, string WalletBalanceBefore, string WalletBalanceAfter, string PaymentType, string ReceiptPDFPath)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.UserSubscriptionOrder).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, EmailTemplate.Subject, SubscriptionName, SubscriptionPrice, PaidValue, PaymentStatus, Start, End, WalletBalanceBefore, WalletBalanceAfter, PaymentType);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient,
                    null,
                    null,
                    null,
                    null,
                    ReceiptPDFPath
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }
        public async Task<bool> SendUserSubscriptionOrderPaypal(string Recipient, string Name, string SubscriptionName, string SubscriptionPrice, string PaidValue, string PaymentStatus, string Start, string End, string WalletBalanceBefore, string WalletBalanceAfter, string PaymentType, string PaypalPaidValue, string ReceiptPDFPath)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.UserSubscriptionOrderPaypal).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, EmailTemplate.Subject, SubscriptionName, SubscriptionPrice, PaidValue, PaymentStatus, Start, End, WalletBalanceBefore, WalletBalanceAfter, PaymentType, PaypalPaidValue);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient,
                    null,
                    null,
                    null,
                    null,
                    ReceiptPDFPath
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }





        public async Task<bool> SendUserBetListingOrder
        (
            string Recipient,
            string Name,
            Order Order,
            ICollection<OrderItem> OrderItems,
            string receiptPDFPath,
            int TimezoneOffset = 0
        )
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.UserBetListingOrder).ConfigureAwait(false);
                string subject = "Order " + Order.OrderNumber + " Details";
                string datePaidValue = Order.DatePaid.HasValue ? DateHelper.DateFormat(Order.DatePaid.Value.FromUTCData(TimezoneOffset)) : "Pending";
                bool isPaypal = Order.PaymentType == OrderPaymentType.Paypal;
                string PaypalPart = "";
                string OrderItemsPart = "";

                if (isPaypal)
                {
                    PaypalPart += "<tr>";
                    PaypalPart += "<td>";
                    PaypalPart += "<strong>Total Paid on Paypal</strong>: $ " + Order.TotalPaidOnPaypal.ToString("0.00");
                    PaypalPart += "</td>";
                    PaypalPart += "</tr>";

                    PaypalPart += "<tr>";
                    PaypalPart += "<td>";
                    PaypalPart += "<strong>Paypal Order ID</strong>: " + Order.PaypalOrderId;
                    PaypalPart += "</td>";
                    PaypalPart += "</tr>";
                }

                foreach (var i in OrderItems)
                {
                    OrderItemsPart += "<tr>";
                    OrderItemsPart += "<td>";
                    OrderItemsPart += i.ListingTitle;
                    OrderItemsPart += "</td>";
                    OrderItemsPart += "<td>";
                    OrderItemsPart += i.SoldBy;
                    OrderItemsPart += "</td>";
                    OrderItemsPart += "<td>";
                    OrderItemsPart += "$ " + i.Price.ToString("0.00");
                    OrderItemsPart += "</td>";
                    OrderItemsPart += "</tr>";
                }

                string EmailBody = string.Format
                    (
                        EmailTemplate.Body,
                        GlobalURLs.Website,
                        Name,
                        DateTime.Now.Year,
                        EmailTemplate.Subject,
                        Order.PaymentType,
                        Order.PaymentStatus,
                        Order.OrderType,
                        Order.OrderNumber,
                        DateHelper.DateFormat(Order.RowDate.FromUTCData(TimezoneOffset)),
                        datePaidValue,
                        Order.TotalBeforeDiscount.ToString("0.00"),
                        Order.Discount.ToString("0.00"),
                        Order.DiscountPercent.ToString("0.00"),
                        Order.TotalBeforeTaxAndFees.ToString("0.00"),
                        Order.ServiceFee.ToString("0.00"),
                        Order.ServiceFeePercent.ToString("0.00"),
                        Order.EstimatedTax.ToString("0.00"),
                        Order.EstimatedTaxPercent.ToString("0.00"),
                        Order.TotalAfterTax.ToString("0.00"),
                        Order.WalletBalanceBeforePurchase.ToString("0.00"),
                        Order.WalletBalanceAfterPurchase.ToString("0.00"),
                        PaypalPart,
                        OrderItemsPart
                    );
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient,
                    null,
                    null,
                    null,
                    null,
                    receiptPDFPath
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }





        public async Task<bool> SendEmailTemplateToUser(string Recipient, string Name, SecondaryEmailTemplate template)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.SecondaryEmailTemplate).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, template.Title, template.Content);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    template.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }


        public async Task<bool> SendForgotPasswordShortchase(string Recipient, string Name, string Code)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.ForgotPassword).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, Code, DateTime.Now.Year, EmailTemplate.Subject);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }


        public async Task<bool> SendPasswordChangedShortchase(string Recipient, string Name)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.PasswordChanged).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, EmailTemplate.Subject);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }



        public async Task<bool> SendContactFormSubmission(string Recipient, int TimezoneOffset, string Name, string Email, string Phone, string Message)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.ContactForm).ConfigureAwait(false);
                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, EmailTemplate.Subject, DateTime.Now.Year, DateTime.Now.FromUTCData(TimezoneOffset), Name, Email, Phone, Message);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    EmailTemplate.Subject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }



        public async Task<bool> SendToListingsInOrder(string Recipient, string Name, string Title, string PickType, string Pick, string Odds, decimal Stake, decimal Profit, string Description, string Schedule, string WhereToPlay, string Prediction, string Analysis, string Price, bool HasButton, string OrderId)
        {
            try
            {
                var EmailTemplate = await emailTemplateService.GetByName(EmailTemplates.Account.ListingsInOrder).ConfigureAwait(false);
                string emailSubject = "";
                if (PickType == BetListingType.Live)
                {
                    emailSubject = "In-Play Ticket";
                }
                else if (PickType == BetListingType.Premium)
                {
                    emailSubject = "Premium Ticket";
                }
                else {
                    emailSubject = EmailTemplate.Subject;
                }

                string ButtonHtml = "";
                if (HasButton)
                {
                    ButtonHtml = @"<tr style='padding: 10px 0 10px 0;'>
                                    <td style='padding: 20px;'>
                                        <table style='margin: auto;' role='presentation' border='0' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='button-td button-td-primary' style='border-radius: 4px; background: #ffffff;'><br /><a class='button-a button-a-primary' style='background: #31AC5F; border: 1px solid #31AC5F; font-family: sans-serif; font-size: 15px; line-height: 15px; text-decoration: none; padding: 13px 17px; color: #ffffff; display: block; border-radius: 0px; width: auto !important; margin: 0 auto !important;' href='" + GlobalURLs.Website + "Home/ViewListingsInOrder?Order=" + Security.LinkEncode(Security.Encrypt(OrderId)) + @"'>View All</a></td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </td>
                                </tr>";
                }

                string StakeHtml = "";
                if (Stake > 0.0m)
                {
                    StakeHtml = @"
                                    <td width='33%' style='padding: 10px;'>
                                        <strong style='background: #f0ad4e;padding: 0.25rem;border-radius: 6px;color: white;'>STAKE:</strong> " + Stake.ToString("0.00")+@"
                                    </td>";
                }
                string ProfitHtml = "";

                if (Profit > 0.0m)
                {
                    ProfitHtml = @"
                                    <td width='33%' s style='padding:10px;'>
                                        <strong style='background: #f0ad4e;padding: 0.25rem;border-radius: 6px;color: white;'>PROFIT:</strong> " + Profit.ToString("0.00") + @"
                                    </td>";
                }

                string EmailBody = string.Format(EmailTemplate.Body, GlobalURLs.Website, Name, DateTime.Now.Year, emailSubject, Title, PickType, Pick, Odds, StakeHtml, ProfitHtml, Description, Schedule, WhereToPlay, Prediction, Analysis, Price, ButtonHtml);
                var SenderEmail = await emailConfigService.GetDefault().ConfigureAwait(false);
                return await SendEmail
                (
                    SenderEmail,
                    emailSubject,
                    EmailBody,
                    SenderEmail.Email,
                    SenderEmail.DisplayName,
                    Recipient
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return false;
            }
        }
    }
}