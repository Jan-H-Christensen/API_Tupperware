using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1WebHook
{
    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public GenericJsonWebHookHandler()
        {
            this.Receiver = "genericjson";
        }

        public override Task ExecuteAsync( string receiver, WebHookHandlerContext context)
        {
            //// Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();

            try
            {
                String topic = context.Request.Headers.GetValues("X-WC-Webhook-Topic").First();
                String eventType = context.Request.Headers.GetValues("x-wc-webhook-event").First();

                if (topic.ToLower().Equals("order.created"))
                {
                    DynamicsFacade dynamicsFacade = new DynamicsFacade();
                    dynamicsFacade.CreateOrder(data);
                    System.Diagnostics.Debug.WriteLine("new order");
                }
                else if (topic.ToLower().Equals("customer.updated"))
                {
                    DynamicsFacade dynamicsFacade = new DynamicsFacade();
                    dynamicsFacade.CreateCustomer(data);
                    System.Diagnostics.Debug.WriteLine("new customer");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error");
            }

            System.Diagnostics.Debug.WriteLine("Time: " + DateTime.Now.TimeOfDay.ToString());
            return Task.FromResult(HttpStatusCode.OK);
        }
    }
}