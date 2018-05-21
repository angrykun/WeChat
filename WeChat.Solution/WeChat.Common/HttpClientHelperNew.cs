using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Common
{
    public class HttpClientHelperNew
    {
        private const int waitTime = 30000;
        //private static HttpClient httpClient = new HttpClient();

        public static string Get(string requestUri, string webapiBaseUrl = "")
        {
            //HttpClient httpClient = new HttpClient();
            //{
            //    MaxResponseContentBufferSize = 1024 * 1024 * 2,
            //    BaseAddress = new Uri(webapiBaseUrl)
            //};

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(webapiBaseUrl))
                    {
                        httpClient.BaseAddress = new Uri(webapiBaseUrl);
                    }
                    string result = string.Empty;
                    httpClient.GetAsync(requestUri).ContinueWith(
                           (requestTask) =>
                           {
                               HttpResponseMessage response = requestTask.Result;

                               response.EnsureSuccessStatusCode();

                               result = response.Content.ReadAsStringAsync().Result;
                               //response.Content.ReadAsStringAsync().ContinueWith(
                               //    (readTask) => result = readTask.Result);
                           }).Wait(waitTime);
                    return result;
                }
            }
            catch
            {
                return string.Empty;
            }
            //return result;
        }

        public static T Get<T>(string requestUri, string webapiBaseUrl = "") where T : class
        {
            T t = Activator.CreateInstance<T>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(webapiBaseUrl))
                    {
                        httpClient.BaseAddress = new Uri(webapiBaseUrl);
                    }
                    httpClient.GetAsync(requestUri).ContinueWith(
                        (task) =>
                        {
                            HttpResponseMessage response = new HttpResponseMessage();

                            response.EnsureSuccessStatusCode();

                            t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                            //t = response.Content.ReadAsAsync<T>().Result;

                        }).Wait(waitTime);

                    return t;
                }
            }
            catch
            {
                return t;
            }

        }

        public static string Post(string requestUri, HttpContent httpContent, string webapiBaseUrl = "")
        {
            //HttpClient httpClient = new HttpClient();            
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(webapiBaseUrl))
                    {
                        httpClient.BaseAddress = new Uri(webapiBaseUrl);
                    }
                    string result = string.Empty;
                    httpClient.PostAsync(requestUri, httpContent).ContinueWith(
                       (requestTask) =>
                       {
                           HttpResponseMessage response = requestTask.Result;

                           response.EnsureSuccessStatusCode();

                           result = response.Content.ReadAsStringAsync().Result;

                       }).Wait(waitTime);
                    return result;
                }
            }
            catch
            {
                return string.Empty;
            }

        }

        public static T Post<T>(string requestUri, HttpContent httpContent, string webapiBaseUrl = "")
        {
            T t = Activator.CreateInstance<T>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(webapiBaseUrl))
                    {
                        httpClient.BaseAddress = new Uri(webapiBaseUrl);
                    }
                    httpClient.PostAsync(requestUri, httpContent).ContinueWith((task) =>
                    {
                        HttpResponseMessage response = new HttpResponseMessage();

                        response.EnsureSuccessStatusCode();

                        t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    }).Wait(waitTime);

                    return t;
                }
            }
            catch
            {
                return t;
            }
        }

        public static T Post<T>(string requestUri, Dictionary<string, string> dic, string webapiBaseUrl = "")
        {
            var httpContent = new FormUrlEncodedContent(dic);
            return Post<T>(requestUri, httpContent, webapiBaseUrl);
        }


        public static T Post<T>(string requestUri, string jsonString, string webapiBaseUrl = "")
        {
            HttpContent httpContent = new StringContent(jsonString);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return Post<T>(requestUri, httpContent, webapiBaseUrl);
        }

    }
}
