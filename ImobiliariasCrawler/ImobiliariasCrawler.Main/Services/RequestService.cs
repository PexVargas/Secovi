using HtmlAgilityPack;
using ImobiliariasCrawler.Main.DataObjectTransfer;
using ImobiliariasCrawler.Main.Exceptions;
using ImobiliariasCrawler.Main.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace ImobiliariasCrawler.Main.Services
{
    public delegate void Callback(Response response);


    public class RequestService
    {
        private readonly Scheduling _scheduling;
        private HashSet<byte[]> _fingerPrintRequest;

        public readonly static JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };


        public RequestService(LoggingPerMinuteDto logging, Action callbackFinish)
        {
            _fingerPrintRequest = new HashSet<byte[]>();
            _scheduling = new Scheduling(10, new TimeSpan(0, 0, 0, 0, 1000), logging, callbackFinish);
        }

        public void Get(string url, Callback callback, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            Request(url, callback, dictArgs: dictArgs);

        }
        public void Post(string url, object body, Callback callback, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            var payload = JsonSerializer.Serialize(body, JsonOptions);
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            MakeHeaders(jsonContent.Headers, headers);
            Request(url, callback, stringContent: jsonContent, dictArgs: dictArgs);

        }

        public void FormPost(string url, Callback callback, object objBody=null, Dictionary<string,string> dictBody=null, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            var keyValue = objBody != null ? objBody.ToKeyValue() : dictBody.ToList();
            var formContent = new FormUrlEncodedContent(keyValue);

            MakeHeaders(formContent.Headers, headers);
            Request(url, callback, formContent, dictArgs: dictArgs);
        }

        private void Request(string url, Callback callback, FormUrlEncodedContent formContent=null, StringContent stringContent = null, Dictionary<string, object> dictArgs=null)
        {
            _scheduling.Add(async () =>
            {
                byte[] hashFingerPrint = null;
                string payload = null;
                HttpRequestMessage request = null;

                if (formContent is null && stringContent is null)
                {
                    request = new HttpRequestMessage(HttpMethod.Get, url);
                    hashFingerPrint = HandleHash.BytesSHA256(url);
                }
                else if (formContent is null)
                {
                    request = new HttpRequestMessage(HttpMethod.Post, url) { Content = stringContent };
                    payload = stringContent.ReadAsStringAsync().Result;
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Post, url) { Content = formContent };
                    payload = formContent.ReadAsStringAsync().Result;
                }

                hashFingerPrint = HandleHash.BytesSHA256($"{url}{payload}");
                if (_fingerPrintRequest.Contains(hashFingerPrint))
                    throw new DuplicateRequestException($"Request duplicado: {url} / {payload}");

                using var httpClient = new HttpClient();
                var response = await httpClient.SendAsync(request);
                var selector = await ContentToHtmlDocument(response);
                callback.Invoke(CreateResponse(response, selector, dictArgs));
            });
        }


        private static Response CreateResponse(HttpResponseMessage httpResponse, HtmlDocument selector, Dictionary<string, object> dictArgs) 
            => new Response { HttpResponse = httpResponse, Selector = selector.DocumentNode, DictArgs = dictArgs, Url = httpResponse.RequestMessage.RequestUri.AbsoluteUri };

        private void MakeHeaders(HttpContentHeaders source, Dictionary<string, string> headers = null)
        {
            if (headers != null)
                foreach (var key in headers.Keys)
                    source.Add(key, headers[key]);
        }

        private async Task<HtmlDocument> ContentToHtmlDocument(HttpResponseMessage response)
        {
            var htmlDocument = new HtmlDocument();
            var responseString = await response.Content.ReadAsStringAsync();
            htmlDocument.LoadHtml(responseString);
            return htmlDocument;
        }
    }
}