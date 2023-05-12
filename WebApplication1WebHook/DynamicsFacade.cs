using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.CodeDom;

namespace WebApplication1WebHook
{
    public class DynamicsFacade
    {
        private string Password = $"admin:Password";
        private string ip = "172.18.141.58:7048";
        public async Task CreateOrder(JObject incoming)
        {
            var _token = Password;
            var _tokenBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_token));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);
            
            Info payloade = new Info() { info = incoming.ToString()};
            String jsonData = JsonConvert.SerializeObject(payloade);
            System.Diagnostics.Debug.WriteLine(jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://" + this.ip + "/BC/ODataV4/WooCom_NewSalesOrder?company=CRONUS%20Danmark%20A%2FS", content);
            string data = "";
            
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error");
            }
            System.Diagnostics.Debug.WriteLine("Result: " + data);
        }

        public async Task CreateCustomer(JObject incoming)
        {
            var _token = Password;
            var _tokenBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_token));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Info payloade = new Info() { info = incoming.ToString()};
            String jsonData = JsonConvert.SerializeObject(payloade);
            System.Diagnostics.Debug.WriteLine(jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://" + this.ip + "/BC/ODataV4/WooCom_NewCustomer?company=CRONUS%20Danmark%20A%2FS", content);
            string data = "";
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error");
            }
            System.Diagnostics.Debug.WriteLine("Result: " + data);
        }
    }

    class Info
    {
        [JsonProperty("info")]
        public string info { get; set; }
    }
}
