using InstaTrain.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace InstaTrain.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Relay : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Get(RelayRequest relayRequest)
        {
            
            var httpClient = new HttpClient();
            //var httpRequest = new HttpRequestMessage(relayRequest.Method.ToLower() == "get" ? HttpMethod.Get : HttpMethod.Post, relayRequest.Url);
            //if(relayRequest.Data != null)
            //{
            //    var myContent = JsonConvert.SerializeObject(relayRequest.Data);
            //    httpRequest.Content =  new StringContent(myContent);
            //}
            //httpClient.BaseAddress = new Uri(relayRequest.Url);

            //httpRequest.Headers.Add("Accept", "application/text");


            foreach (var header in relayRequest?.Headers ?? [])
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.2");

            HttpResponseMessage res; 

            if(relayRequest?.Method?.ToLower() == "get")
            {
             res = await httpClient.GetAsync(relayRequest.Url);

            }
            else
            {
                var data = relayRequest?.Data?.ToString();
                byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(data);
                var content = new ByteArrayContent(messageBytes);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                res = await httpClient.PostAsync(relayRequest?.Url, content);
            }


            var response = await res.Content.ReadAsStringAsync();

            return Ok(response);

        }
    }
}
