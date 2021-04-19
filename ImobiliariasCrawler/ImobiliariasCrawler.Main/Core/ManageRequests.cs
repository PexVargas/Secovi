using HtmlAgilityPack;
using ImobiliariasCrawler.Main.Core;
using ImobiliariasCrawler.Main.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main
{
    public delegate void Callback(Response response);


    public class ManageRequests
    {
        private readonly HashSet<string> _fingerPrintRequest;
        private readonly SemaphoreSlim _semaphore;
        private readonly MonitorSpiders _logging;
        private readonly ConfigurationSpider _configuration;


        public readonly static JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        public ManageRequests(MonitorSpiders logging, ConfigurationSpider configuration)
        {
            _configuration = configuration;
            _semaphore = new SemaphoreSlim(_configuration.ConcurrentRequests);
            _logging = logging;
            _fingerPrintRequest = new HashSet<string>();

            Console.WriteLine($"DOWNLOAD_DELAY: [{_configuration.DownloadDelay}] - CONCURRENT_REQUESTS: [{_configuration.ConcurrentRequests}]");
        }

        public void Get(string url, Callback callback, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
            => Request(url, callback, dictArgs: dictArgs, headers: headers);

        public void Post(string url, object body, Callback callback, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            var payload = JsonSerializer.Serialize(body, JsonOptions);
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");
            Request(url, callback, httpContent: jsonContent, dictArgs: dictArgs, headers: headers);
        }

        public void FormPost(string url, Callback callback, object objBody=null, Dictionary<string,string> dictBody=null, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            var keyValue = objBody != null ? objBody.ToKeyValue() : dictBody.ToList();
            var formContent = new FormUrlEncodedContent(keyValue);
            Request(url, callback, httpContent: formContent, dictArgs: dictArgs, headers: headers);
        }

        private void Request(string url, Callback callback, HttpContent httpContent = null, Dictionary<string, object> dictArgs=null, Dictionary<string, string> headers=null)
        {
            Task.Run(async () =>
            {
                await _semaphore.WaitAsync();
                await Task.Delay(_configuration.DownloadDelay);

                if (httpContent != null) MakeHeaders(httpContent.Headers, headers);

                string payload = httpContent != null ? await httpContent.ReadAsStringAsync() : null;
                var hashFingerPrint = HandleHash.StringSHA256($"{url}{payload}");

                if (_fingerPrintRequest.Contains(hashFingerPrint))
                    _logging.AddCountDuplicateRequest();
                else
                {
                    _fingerPrintRequest.Add(hashFingerPrint);

                    using var httpClient = new HttpClient();

                    _logging.AddCountRequest();
                    var request = CreateRequest(url, httpContent);
                    if (headers != null && headers.ContainsKey("User-Agent"))
                        request.Headers.Add("User-Agent", headers["User-Agent"]);


                    try
                    {
                        var response = await httpClient.SendAsync(request);
                        var selector = await ContentToHtmlDocument(response);
                        callback.Invoke(CreateResponse(response, selector, dictArgs));
                    }
                    catch { 
                        Console.WriteLine($"ERRO REQUEST {hashFingerPrint}");
                        _logging.AddCountDuplicateRequest(); 
                    }
                    finally
                    {
                        _logging.CloseRequest();
                        _semaphore.Release();
                    }
                }
            });
        }


        private static HttpRequestMessage CreateRequest(string url, HttpContent httpContent) =>
            httpContent is null ? new HttpRequestMessage(HttpMethod.Get, url) : new HttpRequestMessage(HttpMethod.Post, url) { Content = httpContent };

        private static Response CreateResponse(HttpResponseMessage httpResponse, HtmlDocument selector, Dictionary<string, object> dictArgs) 
            => new Response { 
                HttpResponse = httpResponse,
                Content = httpResponse.Content,
                Selector = selector.DocumentNode, 
                DictArgs = dictArgs, 
                Url = httpResponse.RequestMessage.RequestUri.AbsoluteUri 
            };

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