using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.Models;
using Newtonsoft.Json.Linq;

namespace TriviaMobileService.Controllers
{
    public class startgamesessionController : ApiController
    {
        public ApiServices Services { get; set; }
        MobileServiceContext context = new MobileServiceContext();

        [Route("api/startgamesession")]
        public HttpResponseMessage Post([FromBody]dynamic payload)
        {
            string playerid = string.Empty;

            try
            {
                if (payload.playerid == null || payload.triviaIds == null)
                {
                    throw new Exception("key not found!");
                }

                var Ids = payload.triviaIds;

                JArray JNotExist = new JArray();

                List<string> theIDs = new List<string>();
                string temp_id;
                JToken temp_token = null;

                foreach (var item in Ids)
                {
                    temp_id = item.id;
                    theIDs.Add(temp_id);

                    if (theIDs.Count > 30)
                    {
                        throw new Exception("the # of IDs exceeded the amount allowed");
                    }

                    var result = context.QuestionItems.Find(temp_id);

                    if (result == null)
                    {
                        temp_token = JObject.FromObject(new { id = temp_id });
                        JNotExist.Add(temp_token);
                    }
                }

                if (theIDs.Count == 0)
                {
                    throw new Exception("there were no IDs provided");
                }

                //If one invalid id is found
                if (temp_token != null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, JNotExist);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "message" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
        }
    }
}
