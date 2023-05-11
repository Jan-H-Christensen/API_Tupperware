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
        private string ip = "";
        public async Task CreateOrderAndCustomer(string incoming)
        {
            var _token = Password;
            var _tokenBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_token));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);
            Payloade payloade = new Payloade() {Info = incoming };
            String jsonData = JsonConvert.SerializeObject(payloade);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://" + this.ip + "/BC/ODataV4/wordpress_createcustomerws?company=CRONUS%20Danmark%20A%2FS", content);
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

    public class Payloade
    {
        public string Info;
    }
}
