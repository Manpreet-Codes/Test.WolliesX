using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Answers.Services.Core
{
    public class HttpClientServiceGet : IHttpClientServiceGet
    {
        public async Task<string> GetRequest(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception exp)
            {
                string error_json = "[{\"code\":\"500\",\"Message\":\"Error Happend while accessing product Information, Contact Admin or check App Settings\"}]";
                throw new HttpRequestException(error_json) { };
            }
        }
    }
}