using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace FootballLeagueApp.Common.Helpers
{
    public static class HeadersHelper
    {
        public const string XOutputRequestId = "x-output-request-id";

        public const string XInputRequestId = "x-input-request-id";

        public const string XOutputTimeStamp = "x-output-timestamp";

        public const string XInputTimeStamp = "x-input-timestamp";

        public const string XClientId = "x-client-id";

        public const string DefaultTimeStampFormat = "yyyMMddHHmmssfff";

        public static string GetHeadersValue(HttpContext context, string key)
        { 
            key = key.ToLower();
            return context.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == key).Value.ToString();
        }

        public static KeyValuePair<string, StringValues> GetHadersFirstOrDefault(HttpContext httpContext, string key, string newKey, string defaultvalue)
        {
            if (httpContext == null)
            {
                // id context  is null retuen default value for headers, its going to be used for background services where there is no HttpContext.
                return new KeyValuePair<string, StringValues>(newKey, defaultvalue);
            }

            var header = httpContext.Request.Headers.FirstOrDefault(x => x.Key == key);
            return string.IsNullOrEmpty(header.Key) ? new KeyValuePair<string, StringValues>(newKey, defaultvalue) : new KeyValuePair<string, StringValues>(newKey, header.Value); 
        }

        public static string ProcessTimeStamp()
        {
            return DateTime.Now.ToString(DefaultTimeStampFormat);
        }

        public static string GenerateNewRequestId()
        {
            return  Guid.NewGuid().ToString();
        }

        public static void SetHeaders(this HttpClient httpClient, HttpContext httpContext, Dictionary<string, string> headersDictionary = null)
        {
            var xInputRequestid = GetHadersFirstOrDefault(httpContext, XInputRequestId, XInputRequestId, GenerateNewRequestId());

            httpClient?.DefaultRequestHeaders.Add(xInputRequestid.Key, xInputRequestid.Value.ToString());
            httpClient?.DefaultRequestHeaders.Add(XInputTimeStamp, ProcessTimeStamp());
            
            if (headersDictionary != null && headersDictionary.Count > 0 && httpClient != null)
            {
                foreach (var header in headersDictionary)
                {
                    if(httpClient.DefaultRequestHeaders.Contains(header.Key))
                    {
                        httpClient.DefaultRequestHeaders.Remove(header.Key);
                    }

                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }
    }
}
