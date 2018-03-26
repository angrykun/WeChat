using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace WeChat.Common
{
    /// <summary>
    /// 网络请求Helper
    /// </summary>
    public class HttpClientHelper
    {

        /// <summary>
        /// Post提交对象，无返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Post的对象</param>
        /// <param name="url">服务地址</param>
        public static void Post<T>(T data, string url)
        {
            Post(data, url, null);
        }

        /// <summary>
        /// Post提交，无返回值
        /// </summary>
        /// <param name="url"></param>
        public static void Post(string url)
        {
            Post<object>(null, url, null);
        }


        /// <summary>
        ///  Post提交对象，返回值类型为V
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static V Post<T, V>(T data, string url)
        {
            return Post<T, V>(data, url, null);
        }

        /// <summary>
        /// Post提交,返回值类型为V
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static V Post<V>(string url)
        {
            return Post<object, V>(null, url, null);
        }

        /// <summary>
        /// Get获取
        /// </summary>
        /// <param name="url">服务地址</param>
        public static void Get(string url)
        {
            Get(url, null);

        }


        /// <summary>
        /// get获取
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="url">服务地址</param>
        /// <returns></returns>
        public static T Get<T>(string url)
        {
            return Get<T>(url, null);

        }


        /// <summary>
        /// Post提交对象，无返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Post的对象</param>
        /// <param name="url">服务地址</param>
        public static void Post<T>(T data, string url, Dictionary<string, string> httpHeaders)
        {
            HttpResponseMessage response = null;
            StringContent httpContent = null;

            if (data != null)
            {
                //var json = JsonSerializerHelper.Serializer(data);
                var json = JsonConvert.SerializeObject(data);
                httpContent = new StringContent(json, new UTF8Encoding(), "application/json");
            }

            using (HttpClient client = new HttpClient())
            {
                if (httpHeaders != null)
                {
                    if (httpContent != null)
                    {
                        foreach (var headerItem in httpHeaders)
                        {
                            httpContent.Headers.Add(headerItem.Key, headerItem.Value);
                        }
                    }
                    else
                    {
                        foreach (var headerItem in httpHeaders)
                        {
                            client.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                        }
                    }
                }
                response = client.PostAsync(url, httpContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string errorResult = string.Empty;
                    try
                    {
                        errorResult = response.Content.ReadAsStringAsync().Result;
                    }
                    catch
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            byte[] arrayByte = new byte[stream.Length];
                            stream.Read(arrayByte, 0, (int)stream.Length);
                            throw new Exception(new UTF8Encoding().GetString(arrayByte));
                        }

                    }
                }
            }
        }


        public static string PostJSON(string jsonData, string url, Dictionary<string, string> httpHeaders)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            HttpResponseMessage response = null;
            StringContent httpContent = null;

            if (jsonData != null)
            {
                httpContent = new StringContent(jsonData, new UTF8Encoding(), "application/json");
            }

            using (HttpClient client = new HttpClient())
            {
                if (httpHeaders != null)
                {
                    if (httpContent != null)
                    {
                        foreach (var headerItem in httpHeaders)
                        {
                            httpContent.Headers.Add(headerItem.Key, headerItem.Value);
                        }
                    }
                    else
                    {
                        foreach (var headerItem in httpHeaders)
                        {
                            client.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                        }
                    }
                }
                response = client.PostAsync(url, httpContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string errorResult = string.Empty;
                    try
                    {
                        errorResult = response.Content.ReadAsStringAsync().Result;
                    }
                    catch
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            byte[] arrayByte = new byte[stream.Length];
                            stream.Read(arrayByte, 0, (int)stream.Length);
                            throw new Exception(new UTF8Encoding().GetString(arrayByte));
                        }

                    }

                    return errorResult;
                }
                else
                {
                    return "true";
                }
            }
        }


        /// <summary>
        /// Post提交对象,无内容，无返回值
        /// </summary>
        /// <param name="url">服务地址</param>
        /// <param name="httpHeaders">http头信息</param>
        public static void Post(string url, Dictionary<string, string> httpHeaders)
        {
            Post<object>(null, url, httpHeaders);
        }



        /// <summary>
        ///  Post提交对象，返回值类型为V
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static V Post<T, V>(T data, string url, Dictionary<string, string> httpHeaders)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            V result = default(V);

            StringContent httpContent = null;

            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data);
                httpContent = new StringContent(json, new UTF8Encoding(), "application/json");
            }


            using (HttpClient client = new HttpClient())
            {
                if (httpHeaders != null)
                {
                    if (httpContent != null)
                    {
                        foreach (var headerItem in httpHeaders)
                        {
                            httpContent.Headers.Add(headerItem.Key, headerItem.Value);
                        }
                    }
                    else
                    {
                        foreach (var headerItem in httpHeaders)
                        {
                            client.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                        }
                    }
                }

                var response = client.PostAsync(url, httpContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string errorResult = string.Empty;
                    try
                    {
                        errorResult = response.Content.ReadAsStringAsync().Result;
                    }
                    catch
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            byte[] arrayByte = new byte[stream.Length];
                            stream.Read(arrayByte, 0, (int)stream.Length);
                            throw new Exception(new UTF8Encoding().GetString(arrayByte));
                        }
                    }
                    if (errorResult != null)
                    {
                        //throw new UtilityException(errorResult.Code, errorResult.Message);
                        throw new Exception("1");
                    }
                }
                else
                {
                    return response.Content.ReadAsAsync<V>().Result;
                }

            }


            return result;
        }

        public static V Post<V>(string url, Dictionary<string, string> httpHeaders)
        {
            return Post<object, V>(null, url, httpHeaders);
        }



        /// <summary>
        /// Get获取
        /// </summary>
        /// <param name="url">服务地址</param>
        public static void Get(string url, Dictionary<string, string> httpHeaders)
        {
            using (HttpClient client = new HttpClient())
            {
                if (httpHeaders != null)
                {
                    foreach (var headerItem in httpHeaders)
                    {
                        client.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                    }
                }

                var response = client.GetAsync(url).Result;

                //Logger.WriteLog("SerialNumberServiceProxy StatusCode:"+response.StatusCode.ToString(), System.Diagnostics.EventLogEntryType.Warning);
                if (!response.IsSuccessStatusCode)
                {
                    string errorResult = string.Empty;
                    try
                    {
                        errorResult = response.Content.ReadAsStringAsync().Result;
                    }
                    catch
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            byte[] arrayByte = new byte[stream.Length];
                            stream.Read(arrayByte, 0, (int)stream.Length);
                            throw new Exception(new UTF8Encoding().GetString(arrayByte));
                        }
                    }
                    if (errorResult != null)
                    {
                        throw new Exception("1");
                        //throw new UtilityException(errorResult.Code, errorResult.Message);
                    }
                }

            }

        }


        /// <summary>
        /// get获取
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="url">服务地址</param>
        /// <returns></returns>
        public static T Get<T>(string url, Dictionary<string, string> httpHeaders)
        {
            T result = default(T);
            using (HttpClient client = new HttpClient())
            {
                if (httpHeaders != null)
                {
                    foreach (var headerItem in httpHeaders)
                    {
                        client.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                    }
                }

                var response = client.GetAsync(url).Result;

                //Logger.WriteLog("SerialNumberServiceProxy StatusCode:"+response.StatusCode.ToString(), System.Diagnostics.EventLogEntryType.Warning);
                if (!response.IsSuccessStatusCode)
                {
                    string errorResult = string.Empty;
                    try
                    {
                        errorResult = response.Content.ReadAsStringAsync().Result;
                    }
                    catch
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            byte[] arrayByte = new byte[stream.Length];
                            stream.Read(arrayByte, 0, (int)stream.Length);
                            throw new Exception(new UTF8Encoding().GetString(arrayByte));
                        }
                    }
                    if (errorResult != null)
                    {
                        throw new Exception("1");
                        //throw new UtilityException(errorResult.Code, errorResult.Message);
                    }
                }
                else
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }

            }

            return result;

        }
    }
}
