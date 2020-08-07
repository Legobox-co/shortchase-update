using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;
using Shortchase.Helpers;
using Shortchase.Authorization;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Shortchase.Services
{
    public class SchedulerTasksService : ISchedulerTasksService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;
        private readonly IBetListingService betListingService;
        private readonly IAPIValidationService apiValidationService;
        private readonly IPOTDListingPredictionService potdListingPredictionService;
        private readonly IPaypalSettingsService paypalSettingsService;
        private readonly IUserSubscriptionService userSubscriptionService;
        private readonly ISubscriptionPlanService subscriptionPlanService;

        public SchedulerTasksService
        (
            DataContext context,
            IErrorLogService logService,
            IBetListingService betListingService,
            IAPIValidationService apiValidationService,
            IPOTDListingPredictionService potdListingPredictionService,
            IPaypalSettingsService paypalSettingsService,
            IUserSubscriptionService userSubscriptionService,
            ISubscriptionPlanService subscriptionPlanService
        )
        {
            _context = context;
            this.errorLogService = logService;
            this.betListingService = betListingService;
            this.apiValidationService = apiValidationService;
            this.potdListingPredictionService = potdListingPredictionService;
            this.paypalSettingsService = paypalSettingsService;
            this.userSubscriptionService = userSubscriptionService;
            this.subscriptionPlanService = subscriptionPlanService;
        }


        public async Task RenewSubscriptions()
        {
            try
            {
                ICollection<UserSubscription> subscriptionsToRenew = await userSubscriptionService.GetSubscriptionsToRenew().ConfigureAwait(true);

                if (subscriptionsToRenew != null && subscriptionsToRenew.Count > 0)
                {


                    ICollection<SubscriptionPlan> subscriptionPlans = await subscriptionPlanService.GetAll().ConfigureAwait(true);

                    PaypalSettings paypalSettings = await paypalSettingsService.GetDefault().ConfigureAwait(true);
                    string ClientId = paypalSettings.ClientID;
                    string Secret = paypalSettings.Secret;


                    var requestPaypal = await PayPalClient.RequestPayPalToken(ClientId, Secret).ConfigureAwait(true);
                    if (requestPaypal == null) throw new Exception("No token obtained");
                    string paypalToken = requestPaypal.access_token;


                    foreach (var subscription in subscriptionsToRenew)
                    {
                        SubscriptionPlan chosenPlan = subscriptionPlans.Where(i => i.Id == subscription.SubscriptionId).SingleOrDefault();

                        var responseFromPaypalAPI = await PayPalClient.RequestPayPalVerifySubscription(paypalToken, subscription.PaypalSubscriptionId).ConfigureAwait(true);

                        dynamic parsedResponse = JsonConvert.DeserializeObject(responseFromPaypalAPI);


                        DateTime startDate = subscription.SubscriptionEnd.AddDays(1).Date;

                        string paypalOrderStatus = parsedResponse.status;
                        string PaymentStatus = null;
                        DateTime? DatePaid = null;
                        DateTime? DateRejected = null;
                        bool? continueToCreateSubscription = null;
                        if (paypalOrderStatus == PaypalSubscriptionPaymentStatus.ACTIVE)
                        {
                            DatePaid = DateTime.UtcNow;
                            PaymentStatus = UserSubscriptionPaymentStatus.Paid;
                            continueToCreateSubscription = true;
                        }
                        else
                        {
                            if (paypalOrderStatus == PaypalSubscriptionPaymentStatus.CANCELLED || paypalOrderStatus == PaypalSubscriptionPaymentStatus.SUSPENDED || paypalOrderStatus == PaypalSubscriptionPaymentStatus.EXPIRED)
                            {

                                continueToCreateSubscription = null;
                            }
                            else
                            {

                                continueToCreateSubscription = false;
                                PaymentStatus = UserSubscriptionPaymentStatus.Pending;
                            }
                        }

                        if (continueToCreateSubscription.HasValue)
                        {
                            if (continueToCreateSubscription.Value)
                            {
                                DateTime endDate = startDate.AddMonths(chosenPlan.DurationInMonths);
                                decimal totalPaidOnPaypal = chosenPlan.TotalValuePerDuration;


                                string subscriptionName = "";
                                if (chosenPlan.Type == SubscriptionPlanType.ShortchasePro)
                                {
                                    subscriptionName = "Shortchase Pro";
                                }
                                else
                                {
                                    subscriptionName = "Boisterous " + chosenPlan.Name;
                                }
                                UserSubscription newSubscription = new UserSubscription
                                {
                                    UserId = subscription.UserId,
                                    GiftById = null,
                                    SubscriptionId = subscription.SubscriptionId,
                                    Deleted = false,
                                    Name = subscriptionName,
                                    PaidValue = chosenPlan.TotalValuePerDuration,
                                    PaymentStatus = PaymentStatus,
                                    Type = chosenPlan.Type,
                                    SubscriptionPrice = chosenPlan.TotalValuePerDuration,
                                    SubscriptionStart = startDate.Date,
                                    SubscriptionEnd = endDate.Date,
                                    WalletBalanceBeforePurchase = subscription.WalletBalanceBeforePurchase,
                                    WalletBalanceAfterPurchase = subscription.WalletBalanceAfterPurchase,
                                    PaypalOrderId = subscription.PaypalOrderId,
                                    DateCancelled = null,
                                    DatePaid = DatePaid,
                                    DateRejected = DateRejected,
                                    PaypalOrderStatus = paypalOrderStatus,
                                    TotalPaidOnPaypal = totalPaidOnPaypal,
                                    AutoRenew = true,
                                    PaypalSubscriptionId = subscription.PaypalSubscriptionId,
                                    PaypalFacilitatorAccessToken = "",
                                    RowDate = DateTime.UtcNow,
                                    HasBeenAutoRenewed = false
                                };


                                var UserHasActiveSubscriptionPlan = await userSubscriptionService.UserHasActiveSubscriptionPlan(newSubscription).ConfigureAwait(true);

                                if (UserHasActiveSubscriptionPlan)
                                {
                                    continue;//throw new Exception("User has active plan of the same type!");
                                }
                                else
                                {
                                    subscription.HasBeenAutoRenewed = true;
                                    _context.Entry(subscription).State = EntityState.Modified;

                                    //var result = await userSubscriptionService.Insert(newSubscription).ConfigureAwait(true);

                                    _context.UserSubscriptions.Add(newSubscription);
                                }
                            }
                            else continue;

                        }
                        else
                        {
                            subscription.AutoRenew = false;
                            _context.Entry(subscription).State = EntityState.Modified;
                        }
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task ValidateByAPIJob()
        {
            try
            {
                #region Bet Listing Validation

                ICollection<BetListing> BetListings = await betListingService.GetAllWaitingForValidation().ConfigureAwait(true);
                if (BetListings != null && BetListings.Count > 0)
                {
                    foreach (var listing in BetListings)
                    {
                        string date = DateHelper.GetDateStringForAPI(listing.Pick.StartTime.FromUTCData(listing.TimezoneOffset));
                        string url = Utility.GetAPICallUrl(listing.Category, date);
                        if (listing.Category.Description == APIValidationCategories.Football)
                        {
                            string dateFrom = date;
                            string dateTo = date;
                            url = Utility.GetLiveScoreAPICallUrl(listing.Category, dateFrom, dateTo);
                        }
                        var result = await apiValidationService.Validate(url, listing).ConfigureAwait(true);
                        if (result.HasValue)
                        {
                            listing.DateVerifiedByApi = DateTime.UtcNow;
                            listing.ResultVerificationByApi = result.Value;
                            listing.IsCorrect = result.Value;

                            _context.Entry(listing).State = EntityState.Modified;
                        }
                    }
                }

                #endregion


                #region POTD Listing Validation
                ICollection<POTDListingPrediction> predictions = await potdListingPredictionService.GetAllWaitingValidation().ConfigureAwait(true);
                if (predictions != null && predictions.Count > 0)
                {
                    foreach (var prediction in predictions)
                    {
                        if (prediction.MarketId == prediction.POTD.MarketId && prediction.TipId == prediction.POTD.TipId)
                        {
                            prediction.VerifiedAsCorrect = true;
                            prediction.DateVerified = DateTime.UtcNow;
                            User user = await _context.Users.Where(i => i.Id == prediction.PredictedById).SingleOrDefaultAsync().ConfigureAwait(false);
                            user.TotalPointsAvailable += 1;
                            _context.Entry(user).State = EntityState.Modified;

                        }
                        else
                        {
                            prediction.VerifiedAsCorrect = false;
                            prediction.DateVerified = DateTime.UtcNow;
                        }
                        _context.Entry(prediction).State = EntityState.Modified;
                    }
                }
                #endregion


                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

    }
}