﻿using HtmlAgilityPack;
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


    public class RequestService : IDisposable
    {
        private readonly HttpClient _httpClient;

        public readonly static JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };


        public RequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Get(string url, Callback callback, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            var httpResponse = await _httpClient.GetAsync(url);
            var selector = await ContentToHtmlDocument(httpResponse);
            callback.Invoke(CreateResponse(httpResponse, selector, dictArgs));
        }
        public async Task Post(string url, object body, Callback callback, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            var payload = JsonSerializer.Serialize(body, JsonOptions);

            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");
            MakeHeaders(jsonContent.Headers, headers);

            var httpResponse = await _httpClient.PostAsync(url, jsonContent);
            var selector = await ContentToHtmlDocument(httpResponse);
            callback.Invoke(CreateResponse(httpResponse, selector, dictArgs));
        }

        public async Task FormPost(string url, object body, Callback callback, Dictionary<string, string> headers = null, Dictionary<string, object> dictArgs = null)
        {
            var keyValue = body.ToKeyValue();
            var formContent = new FormUrlEncodedContent(keyValue);
            MakeHeaders(formContent.Headers, headers);
            var httpResponse = await _httpClient.PostAsync(url, formContent);
            var selector = await ContentToHtmlDocument(httpResponse);
            callback.Invoke(CreateResponse(httpResponse, selector, dictArgs));
        }

        public static Response CreateResponse(HttpResponseMessage httpResponse, HtmlDocument selector, Dictionary<string, object> dictArgs) 
            => new Response { HttpResponse = httpResponse, Selector = selector.DocumentNode, DictArgs = dictArgs, Url = httpResponse.RequestMessage.RequestUri.AbsoluteUri };

        public void MakeHeaders(HttpContentHeaders source, Dictionary<string, string> headers = null)
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36");
            if (headers != null)
                foreach (var key in headers.Keys)
                {
                    source.Add(key, headers[key]);
                }
        }

        private async Task<HtmlDocument> ContentToHtmlDocument(HttpResponseMessage response)
        {
            var htmlDocument = new HtmlDocument();
            var responseString = await response.Content.ReadAsStringAsync();
            htmlDocument.LoadHtml(responseString);
            return htmlDocument;
        }

        public void Dispose() => _httpClient.Dispose();
    }
}