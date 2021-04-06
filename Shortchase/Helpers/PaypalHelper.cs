using System;
using System.IO;
using System.Text;
using PayPalCheckoutSdk.Core;
using BraintreeHttp;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Shortchase.Helpers
{
    public class PayPalClient
    {
        /**
            Set up PayPal environment with sandbox credentials.
            In production, use LiveEnvironment.
         */
        public static PayPalEnvironment environment()
        {
            return new LiveEnvironment("AXCzzlSJFloTVMB90tsyYZeNmTmF9ZJ0d3TRPG1wTv0TjMdYx0v-OQ6E0fg1ks_Vw7rUcx5WXRzQzVtm", "EHrnzdgSbkVYm_AoWBoMaR_lWoSgvHmxiUraB7u7IYU0IjUmFEVMGB-uwZDWRZTqeyiSTzpynAc3xWsF");
        }

        public static PayPalEnvironment environment(string ClientId, string Secret)
        {
            return new LiveEnvironment(ClientId, Secret);
        }


        public static PayPalEnvironment sandboxenvironment(string ClientId, string Secret)
        {
            return new SandboxEnvironment(ClientId, Secret);
        }
        /**
            Returns PayPalHttpClient instance to invoke PayPal APIs.
         */
        public static PayPalHttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static PayPalHttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }
        public static PayPalHttpClient client(string ClientId, string Secret)
        {
            return new PayPalHttpClient(environment(ClientId, Secret));
        }

        public static PayPalHttpClient client(string refreshToken, string ClientId, string Secret)
        {
            return new PayPalHttpClient(environment(ClientId, Secret), refreshToken);
        }

        /**
            Use this method to serialize Object to a JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }


        public class PayPalTokenModel
        {
            public string scope { get; set; }
            public string nonce { get; set; }
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string app_id { get; set; }
            public int expires_in { get; set; }
        }



        public static async Task<PayPalClient.PayPalTokenModel> RequestPayPalToken(string APIClientId, string APISecret)
        {
            // Discussion about SSL secure channel
            // http://stackoverflow.com/questions/32994464/could-not-create-ssl-tls-secure-channel-despite-setting-servercertificatevalida
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    var byteArray = Encoding.UTF8.GetBytes(APIClientId + ":" + APISecret);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    var url = new Uri(APILinks.PayPal.GetToken, UriKind.Absolute);

                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                    var requestParams = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>("grant_type", "client_credentials")
                            };

                    var content = new FormUrlEncodedContent(requestParams);
                    var webresponse = await client.PostAsync(url, content);
                    var jsonString = await webresponse.Content.ReadAsStringAsync();

                    // response will deserialized using Jsonconver
                    PayPalClient.PayPalTokenModel payPalTokenModel = JsonConvert.DeserializeObject<PayPalClient.PayPalTokenModel>(jsonString);
                    return payPalTokenModel;
                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }



        public static async Task<string> RequestPayPalVerifySubscription(string token, string subscriptionId)
        {
            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var url = new Uri(APILinks.PayPal.VerifySubscriptionPlans+ subscriptionId, UriKind.Absolute);
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;


                    var webresponse = await client.GetAsync(url).ConfigureAwait(true);
                    string jsonString = await webresponse.Content.ReadAsStringAsync();

                    return jsonString;


                }
            }
            catch (System.Exception ex)
            {
                //TODO: Log connection error
                throw;
            }
        }
    }
}
