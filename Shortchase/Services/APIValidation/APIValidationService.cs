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

namespace Shortchase.Services
{
    public class APIValidationService : IAPIValidationService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public APIValidationService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }


        private async Task<string> CallAPI(string url, string method = "GET", string ContentType = "application/json; charset=UTF-8")
        {
            try
            {

                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = method;
                myReq.ContentType = ContentType;


                UTF8Encoding enc = new UTF8Encoding();

                WebResponse wr = myReq.GetResponse();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }
        public async Task<string> StandardCall(string url, string method = "GET", string ContentType = "application/json; charset=UTF-8")
        {
            try
            {

                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = method;
                myReq.ContentType = ContentType;


                UTF8Encoding enc = new UTF8Encoding();

                WebResponse wr = myReq.GetResponse();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        private async Task<bool?> ValidateSummariesMethod(JArray summaries, BetListing listingData)
        {
            try
            {
                bool? isPredictionCorrect = null;
                foreach (var summary in summaries)
                {
                    var sportEvent = summary["sport_event"];
                    var competitors = sportEvent["competitors"];
                    string HomeTeam = "";
                    string AwayTeam = "";
                    int iterationCount = 0;
                    foreach (var competitor in competitors)
                    {
                        iterationCount++;
                        if (iterationCount == 1) HomeTeam = competitor["name"].ToString();
                        else AwayTeam = competitor["name"].ToString();
                    }

                    if (HomeTeam.ToUpper().Contains(listingData.Pick.Team1.ToUpper()) && AwayTeam.ToUpper().Contains(listingData.Pick.Team2.ToUpper()))
                    {
                        var sportEventStatus = summary["sport_event_status"];
                        int homeTeamScore = Convert.ToInt32(sportEventStatus["home_score"]);
                        int awayTeamScore = Convert.ToInt32(sportEventStatus["away_score"]);


                        if (homeTeamScore == awayTeamScore)
                        {
                            if (listingData.Tip.Description.ToUpper() == PredictionValue.Draw.ToUpper())
                            {
                                isPredictionCorrect = true;
                                break;
                            }
                            else
                            {
                                isPredictionCorrect = false;
                                break;
                            };
                        }
                        else
                        {
                            if (homeTeamScore > awayTeamScore)
                            {
                                if (listingData.Tip.Description.ToUpper() == PredictionValue.HomeWin.ToUpper())
                                {
                                    isPredictionCorrect = true;
                                    break;
                                }
                                else
                                {
                                    isPredictionCorrect = false;
                                    break;
                                };
                            }
                            else
                            {
                                if (listingData.Tip.Description.ToUpper() == PredictionValue.AwayWin.ToUpper())
                                {
                                    isPredictionCorrect = true;
                                    break;
                                }
                                else
                                {
                                    isPredictionCorrect = false;
                                    break;
                                };
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
                return isPredictionCorrect;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        private async Task<bool?> ValidateResultsMethod(JArray results, BetListing listingData)
        {
            try
            {
                bool? isPredictionCorrect = null;
                foreach (var result in results)
                {
                    var sportEvent = result["sport_event"];
                    var competitors = sportEvent["competitors"];
                    string HomeTeam = "";
                    string AwayTeam = "";
                    string HomeTeamId = "";
                    string AwayTeamId = "";
                    int iterationCount = 0;
                    foreach (var competitor in competitors)
                    {
                        iterationCount++;
                        if (iterationCount == 1)
                        {
                            HomeTeam = competitor["name"].ToString();
                            HomeTeamId = competitor["id"].ToString();
                        }
                        else
                        {
                            AwayTeam = competitor["name"].ToString();
                            AwayTeamId = competitor["id"].ToString();
                        }
                    }

                    if (HomeTeam.ToUpper().Contains(listingData.Pick.Team1.ToUpper()) && AwayTeam.ToUpper().Contains(listingData.Pick.Team2.ToUpper()))
                    {
                        var sportEventStatus = result["sport_event_status"];
                        string winnerId = sportEventStatus["winner_id"].ToString().ToUpper();


                        if (winnerId == AwayTeamId.ToUpper())
                        {
                            if (listingData.Tip.Description.ToUpper() == PredictionValue.AwayWin.ToUpper())
                            {
                                isPredictionCorrect = true;
                                break;
                            }
                            else
                            {
                                isPredictionCorrect = false;
                                break;
                            };
                        }
                        else if (winnerId == HomeTeamId.ToUpper())
                        {
                            if (listingData.Tip.Description.ToUpper() == PredictionValue.HomeWin.ToUpper())
                            {
                                isPredictionCorrect = true;
                                break;
                            }
                            else
                            {
                                isPredictionCorrect = false;
                                break;
                            };
                        }
                        else isPredictionCorrect = null;
                    }
                    else
                    {
                        continue;
                    }

                }
                return isPredictionCorrect;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<string> GetAPIResponse(string url)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);


                return responseFromAPI;

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<bool?> ValidateAmericanFootball(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray summaries = jsonData.summaries;

                if (summaries == null || summaries.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateSummariesMethod(summaries, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }



        public async Task<bool?> ValidateBadminton(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray summaries = jsonData.summaries;

                if (summaries == null || summaries.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateSummariesMethod(summaries, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<bool?> ValidateBaseball(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray summaries = jsonData.summaries;

                if (summaries == null || summaries.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateSummariesMethod(summaries, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<bool?> ValidateBasketball(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray summaries = jsonData.summaries;

                if (summaries == null || summaries.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateSummariesMethod(summaries, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }


        public async Task<bool?> ValidateCricket(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.results;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }



        public async Task<bool?> ValidateDarts(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.summaries;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<bool?> ValidateHandball(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.summaries;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<bool?> ValidateIceHockey(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.results;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }


        public async Task<bool?> ValidateUFC(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.summaries;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<bool?> ValidateMotorsports(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.results;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }




        public async Task<bool?> ValidateSnooker(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.summaries;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<bool?> ValidateSquash(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.results;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }


        public async Task<bool?> ValidateTableTennis(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.summaries;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }


        public async Task<bool?> ValidateVolleyball(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;
                dynamic jsonData = JObject.Parse(responseFromAPI);

                JArray results = jsonData.summaries;

                if (results == null || results.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return await ValidateResultsMethod(results, listingData).ConfigureAwait(true);
                }

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }



        public async Task<bool?> ValidateFootball(string url, BetListing listingData)
        {
            try
            {
                string responseFromAPI = await CallAPI(url).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseFromAPI)) return null;

                dynamic jsonData = JObject.Parse(responseFromAPI);

                string api_call_success = jsonData.success;
                if (api_call_success.ToLower() == true.ToString().ToLower()) {
                    dynamic data = jsonData.data;

                    if (data == null || data.Count <= 0)
                    {
                        return null;
                    }
                    else {
                        JArray matches = data.match;
                        if (matches == null || matches.Count <= 0)
                        {
                            return null;
                        }
                        else
                        {
                            bool? isPredictionCorrect = null;
                            foreach (var match in matches) {
                                string homeTeam = match["home_name"].ToString();
                                string awayTeam = match["away_name"].ToString();
                                var scoreString = match["score"].ToString().Trim().Split("-");
                                int homeTeamScore = Convert.ToInt32(scoreString[0]);
                                int awayTeamScore = Convert.ToInt32(scoreString[1]);
                                if (homeTeam.ToUpper().Contains(listingData.Pick.Team1.ToUpper()) && awayTeam.ToUpper().Contains(listingData.Pick.Team2.ToUpper()))
                                {
                                    if (homeTeamScore == awayTeamScore)
                                    {
                                        if (listingData.Tip.Description.ToUpper() == PredictionValue.Draw.ToUpper())
                                        {
                                            isPredictionCorrect = true;
                                            break;
                                        }
                                        else
                                        {
                                            isPredictionCorrect = false;
                                            break;
                                        };
                                    }
                                    else
                                    {
                                        if (homeTeamScore > awayTeamScore)
                                        {
                                            if (listingData.Tip.Description.ToUpper() == PredictionValue.HomeWin.ToUpper())
                                            {
                                                isPredictionCorrect = true;
                                                break;
                                            }
                                            else
                                            {
                                                isPredictionCorrect = false;
                                                break;
                                            };
                                        }
                                        else
                                        {
                                            if (listingData.Tip.Description.ToUpper() == PredictionValue.AwayWin.ToUpper())
                                            {
                                                isPredictionCorrect = true;
                                                break;
                                            }
                                            else
                                            {
                                                isPredictionCorrect = false;
                                                break;
                                            };
                                        }
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            return isPredictionCorrect;
                        }
                    }
                }
                else return null;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<bool?> Validate(string url, BetListing listingData)
        {
            try
            {
                bool? result = null;
                switch (listingData.Category.Description)
                {
                    case APIValidationCategories.AmericanFootball:
                        result = await ValidateAmericanFootball(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Badminton:
                        result = await ValidateBadminton(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Baseball:
                        result = await ValidateBaseball(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Basketball:
                        result = await ValidateBasketball(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Cricket:
                        result = await ValidateCricket(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Darts:
                        result = await ValidateDarts(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Handball:
                        result = await ValidateHandball(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.IceHockey:
                        result = await ValidateIceHockey(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Motorsport:
                        result = await ValidateMotorsports(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Snooker:
                        result = await ValidateSnooker(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Squash:
                        result = await ValidateSquash(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.UFC:
                        result = await ValidateUFC(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Volleyball:
                        result = await ValidateVolleyball(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.TableTennis:
                        result = await ValidateTableTennis(url, listingData).ConfigureAwait(false);
                        break;
                    case APIValidationCategories.Football:
                        result = await ValidateFootball(url, listingData).ConfigureAwait(false);
                        break;
                    default:
                        result = null;
                        break;
                }
                return result;


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }
    }
}