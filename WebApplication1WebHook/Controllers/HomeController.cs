using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebApplication1WebHook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        
        public async Task<ActionResult> CreateCustomer(int customerNo = 10, string customerName = "")
        {
            DynamicsFacade dynamicsFacade = new DynamicsFacade();
            //await dynamicsFacade.CreateCustomer("Carlos", "carlos@easv.dk");
            await dynamicsFacade.InsertData("Carlos", "carlos@easv.dk");

            //await TestCall();

            /*
            // Call create customer ODataV4
            System.Diagnostics.Debug.WriteLine("CreateCustomer call with: " + customerNo);

            //var options = new NTLMOptions { };

            //var handler = new HttpClientHandler
            //{
            //    Credentials = new NetworkCredential(options.Username, options.Password, options.Domain)
            //};


            //Uri adress = new Uri(@"http://easv-fha-q119:7048/BC170/api/beta/companies");
            //Uri adress = new Uri(@"http://easv-fha-q119:7048/BC170/ODataV4/Company('CRONUS%20UK%20Ltd.')/SalesOrder");
            Uri adress = new Uri(@"http://easv-fha-q119:7048/BC170/ODataV4/WordPressWS_createcustomer?company=CRONUS%20UK%20Ltd.");

            



            var credentialsCache = new CredentialCache();
            credentialsCache.Add(adress, "NTLM", new NetworkCredential("FHA", secret,"EASV"));//Negotiate
            var handler = new HttpClientHandler() { Credentials = credentialsCache, PreAuthenticate = true };//, PreAuthenticate = true


            using (var httpClient = new HttpClient(handler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(2);
                //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = await httpClient.PostAsJsonAsync(adress, "{'id':1}");
                //var response = await httpClient.GetAsync(adress);
                var response = await httpClient.PostAsJsonAsync(adress, new Test { id = customerNo, name= customerName }); ;

                var result = await response.Content.ReadAsStringAsync();
                
                if ( response.IsSuccessStatusCode )
                    System.Diagnostics.Debug.WriteLine("CreateCustomer call result: " + result);
                else
                    System.Diagnostics.Debug.WriteLine("CreateCustomer call error: " + response.StatusCode.ToString());
            }



            //BasicHttpBinding binding = new BasicHttpBinding();
            //EndpointAddress address = new EndpointAddress(adress);
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            //binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            //binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;

            //binding.MessageEncoding = WSMessageEncoding.Text;
            //binding.TextEncoding = Encoding.UTF8;
            //binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;


            //using (var client = new HttpClient())
            //{
            //    client.cre
            //    client.Timeout = TimeSpan.FromMinutes(2);
            //}

            */
            return RedirectToAction("Index");
        }


        private String secret = "2012Elias";
    

        private async Task TestCall()
        {
            //l/p
            var _token = $"admin:Password";
            var _tokenBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Basic", "YWRtaW46UGFzc3dvcmQ=");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);
            HttpResponseMessage response = await client.GetAsync("http://test:7048/BC/ODataV4/Company('CRONUS%20Danmark%20A%2FS')/Job_List");

            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error: " + response.ReasonPhrase);
            }

            System.Diagnostics.Debug.WriteLine("Result: " + data);
        }

}


    class Test
    {
        public int id { get; set; }
        public string name { get; set; }
        
    }

    
}
