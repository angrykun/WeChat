using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeChat.Common
{
    public class HttpMethod
    {
        public string HttpGet(string url, Encoding codeName, ref string cookie, bool isGzip = false, string referer = "", string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request = null;
            HttpWebResponse oWebResp = null;
            StreamReader oStream = null;
            string sResp = "";
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Accept = "*/*";
                request.UserAgent = "Mozilla/5.2 (Windows NT 6.1; WOW64; rv:12.0) Gecko/20100101 Firefox/12.0";
                request.Headers["Accept-Language"] = "zh-cn";
                request.KeepAlive = false;

                if (referer != null)
                {
                    request.Referer = referer;
                }
                if (contentType != null)
                {
                    request.ContentType = contentType;
                }
                request.CookieContainer = new CookieContainer();
                if (!string.IsNullOrEmpty(cookie))
                {
                    request.CookieContainer.SetCookies(request.RequestUri, cookie);
                }
                if (isGzip)
                {
                    request.Headers["Accept-Encoding"] = "gzip, deflate";
                }
                request.Timeout = 60000;

                oWebResp = (HttpWebResponse)request.GetResponse();

                CookieCollection tmpCookieCollection = oWebResp.Cookies;
                foreach (Cookie ck in tmpCookieCollection)
                {
                    cookie += ck.Name + "=" + ck.Value + ",";
                }

                Stream s = oWebResp.GetResponseStream();

                oStream = new StreamReader(s, codeName);
                sResp = oStream.ReadToEnd();
                oStream.Close();
                return sResp;
            }
            catch
            {
                return "";
            }
            finally
            {
                if (oStream != null)
                {
                    oStream.Close();
                }
            }
        }
        public string HttpPost(string url, string postData, Encoding codeName, ref string cookie, bool isGzip = false, string referer = "", string contentType = "application/x-www-form-urlencoded", bool isUseCert = false)
        {
            HttpWebRequest myHttpWebRequest = null;
            Stream myRequestStream = null;
            StreamWriter myStreamWriter = null;
            HttpWebResponse myHttpWebResponse = null;
            string ContentType = contentType;

            try
            {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.UserAgent = "Mozilla/5.2 (Windows NT 6.1; WOW64; rv:12.0) Gecko/20100101 Firefox/12.0";
                myHttpWebRequest.Accept = "*/*";

                //if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                if (isUseCert)
                {
                    try
                    {
                        myHttpWebRequest.Credentials = CredentialCache.DefaultCredentials;
                        MyCerts mycert = new MyCerts();

                        try
                        {
                            mycert.Init();
                            myHttpWebRequest.ClientCertificates.Add(mycert[0]);
                        }
                        catch (Exception ex)
                        {
                            //new WxExceptionHelper().LogException(ex); ;
                        }

                        if (mycert.m_certs != null)
                        {
                            foreach (var item in mycert.m_certs)
                            {
                                if (item.Subject.Contains("mp.weixin.qq.com") || item.Subject.Contains("api.weixin.qq.com") || item.Subject.Contains("res.weixin.qq.com"))
                                {
                                    myHttpWebRequest.ClientCertificates.Add(item);
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception exx)
                    {
                        //new WxExceptionHelper().LogException(exx);
                    }
                }

                myHttpWebRequest.Headers.Add("Origin", "https://mp.weixin.qq.com");
                myHttpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                myHttpWebRequest.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
                myHttpWebRequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                myHttpWebRequest.Timeout = 30000;
                if (!string.IsNullOrEmpty(referer))
                {
                    myHttpWebRequest.Referer = referer;
                }
                if (isGzip)
                {
                    myHttpWebRequest.Headers["Accept-Encoding"] = "gzip, deflate";
                }
                myHttpWebRequest.CookieContainer = new CookieContainer();
                myHttpWebRequest.CookieContainer.PerDomainCapacity = 100;
                if (!string.IsNullOrEmpty(cookie))
                {
                    myHttpWebRequest.CookieContainer.SetCookies(myHttpWebRequest.RequestUri, cookie);
                }
                myHttpWebRequest.Method = "POST";

                //if (isGzip)
                //{
                //    myRequestStream = new GZipInputStream(myHttpWebRequest.GetRequestStream());
                //}
                //else
                //{
                //    myRequestStream = myHttpWebRequest.GetRequestStream();
                //}

                myRequestStream = myHttpWebRequest.GetRequestStream();

                myStreamWriter = new StreamWriter(myRequestStream);
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                CookieCollection tmpCookieCollection = myHttpWebResponse.Cookies;
                foreach (Cookie ck in tmpCookieCollection)
                {
                    cookie += ck.Name + "=" + ck.Value + ",";
                }
                //if (isGzip)
                //{
                //    myRequestStream = new GZipStream(myHttpWebResponse.GetResponseStream(), CompressionMode.Decompress);
                //}
                //else
                //{
                //    myRequestStream = myHttpWebResponse.GetResponseStream();
                //}

                myRequestStream = myHttpWebResponse.GetResponseStream();

                string result = new StreamReader(myRequestStream, codeName).ReadToEnd();
                return result;
            }
            catch (Exception ee)
            {
                ee.Data["RequestUrl"] = url;
                ee.Data["PostData"] = postData;
                ee.Data["Message"] = ee.Message;
                //new WxExceptionHelper().LogException(ee);
                return ee.Message;
            }
            finally
            {
                if (myRequestStream != null)
                {
                    myRequestStream.Close();
                }
            }
        }


        public string HttpPost(string url, string filePath, string postData, ref string cookie, Encoding codeName, string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest myHttpWebRequest = null;
            Stream myRequestStream = null;
            string ContentType = contentType;
            string responseData = string.Empty; ;
            try
            {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.UserAgent = "Mozilla/5.2 (Windows NT 6.1; WOW64; rv:12.0) Gecko/20100101 Firefox/12.0";
                myHttpWebRequest.Accept = "*/*";

                var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
                if (webRequest == null) return string.Empty;

                webRequest.Method = "POST";
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.UserAgent = "WinWeiBee";
                webRequest.Timeout = 20000;
                webRequest.KeepAlive = true;

                NameValueCollection qs = HttpUtility.ParseQueryString(postData);
                webRequest.PreAuthenticate = true;
                webRequest.AllowWriteStreamBuffering = true;

                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] boundarybytes1 = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

                webRequest.ContentType = "multipart/form-data;boundary=" + boundary;

                Stream requestStream = webRequest.GetRequestStream();

                const string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

                MultipartformBody(qs, boundarybytes, boundarybytes1, requestStream, formdataTemplate);

                // Write file type to head
                const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string header = "";
                string fileType = System.IO.Path.GetExtension(filePath);
                if (string.Equals(fileType, ".jpg", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileType, ".jpeg", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileType, ".bmp", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileType, ".png", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileType, ".gif", StringComparison.CurrentCultureIgnoreCase))
                {
                    header = string.Format(headerTemplate, "media", string.Format("{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmss")), "image/pjpeg");
                }


                byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                requestStream.Write(headerbytes, 0, headerbytes.Length);

                // Write picture file binary to post data
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                }

                // The trailer data
                byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(trailer, 0, trailer.Length);
                requestStream.Close();

                try
                {
                    using (WebResponse wr = webRequest.GetResponse())
                    {
                        Stream s = wr.GetResponseStream();
                        StreamReader sr = new StreamReader(s, codeName);
                        responseData = sr.ReadToEnd();
                    }
                }
                catch (WebException we)
                {
                    Stream s = we.Response.GetResponseStream();
                    StreamReader sr = new StreamReader(s, codeName);
                    responseData = sr.ReadToEnd();
                }
                catch
                {

                }
                finally
                {

                }

                return responseData;
            }
            catch
            {
                //throw ex;
                return "";
            }
            finally
            {
                if (myRequestStream != null)
                {
                    myRequestStream.Close();
                }
            }
        }

        private static void MultipartformBody(NameValueCollection qs, byte[] boundarybytes, byte[] boundarybytes1, Stream requestStream, string formdataTemplate)
        {
            requestStream.Write(boundarybytes1, 0, boundarybytes1.Length);
            string key = "status";
            string formitem = string.Format(formdataTemplate, key, qs[key]);
            byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
            requestStream.Write(formitembytes, 0, formitembytes.Length);
            requestStream.Write(boundarybytes, 0, boundarybytes.Length);
            string formitem1 = string.Format(formdataTemplate, "source", qs["source"]);
            byte[] formitembytes1 = Encoding.UTF8.GetBytes(formitem1);
            requestStream.Write(formitembytes1, 0, formitembytes1.Length);
            requestStream.Write(boundarybytes, 0, boundarybytes.Length);
        }
    }
}
