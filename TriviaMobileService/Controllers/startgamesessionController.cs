using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.Models;
using Newtonsoft.Json.Linq;
using TriviaMobileService.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace TriviaMobileService.Controllers
{
    public class startgamesessionController : ApiController
    {
        public ApiServices Services { get; set; }
        MobileServiceContext context = new MobileServiceContext();

        /// <summary>
        /// Start a game session.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [AuthorizeLevel(AuthorizationLevel.Application)]
        [Route("api/startgamesession")]
        public HttpResponseMessage Post([FromBody]dynamic payload)
        {
            try
            {
                if (payload.playerid == null || payload.triviaIds == null)
                {
                    throw new Exception("key not found!");
                }

                string playerid = payload.playerid;
                var Ids = payload.triviaIds;

                // JArray to hold bad ids.
                JArray JNotExist = new JArray();

                List<string> theIDs = new List<string>();
                string temp_id;
                JToken temp_token = null;

                // Check if IDs are valid.
                foreach (var item in Ids)
                {
                    temp_id = item.id;
                    theIDs.Add(temp_id);

                    if (theIDs.Count > 30)
                    {
                        throw new Exception("the # of IDs exceeded the amount allowed");
                    }

                    var result = context.QuestionItems.Find(temp_id);

                    // If such question doesn't exist, add the bad list.
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

                // Create a new game session.
                string new_id = Guid.NewGuid().ToString();
                context.SessionItems.Add(new SessionItem { Id = new_id, playerid = playerid });
                // Add all SessionQuestion items.
                foreach (var theID in theIDs)
                {
                    context.SessionQuestionItems.Add(new SessionQuestionItem { Id = Guid.NewGuid().ToString(), GameSessionID = new_id, QuestionID = theID, proposedAnswer = "?"});
                }

                context.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, new { playerid = playerid, gamesessionid = new_id});
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
        }
    }
}
