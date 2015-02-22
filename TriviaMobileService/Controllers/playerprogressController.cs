using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.Models;
using TriviaMobileService.DataObjects;

namespace TriviaMobileService.Controllers
{
    public class playerprogressController : ApiController
    {
        public ApiServices Services { get; set; }
        MobileServiceContext context = new MobileServiceContext();

        [Route("api/playerprogress")]
        public HttpResponseMessage Patch([FromBody]dynamic payload)
        {
            try
            {
                if (payload.playerid == null || payload.gamesessionid == null || payload.id == null || payload.proposedAnswer == null)
                {
                    throw new Exception("key not found!");
                }

                if (payload.proposedAnswer != "1" && payload.proposedAnswer != "2" && payload.proposedAnswer != "3" && payload.proposedAnswer != "4")
                {
                    throw new Exception("format error!");
                }

                string SessionID = payload.gamesessionid;
                string PlayerID = payload.playerid;
                string QuestionID = payload.id;

                var sessionItem = context.SessionItems.Find(SessionID);

                if (sessionItem == null || sessionItem.playerid != PlayerID)
                {
                    throw new Exception("PlayerSession Error!");
                }

                SessionQuestionItem SQtoUpdate = context.SessionQuestionItems.Where(p => p.GameSessionID == SessionID && p.QuestionID == QuestionID).FirstOrDefault();

                if (SQtoUpdate == null)
                {
                    throw new Exception("SessionQuestion Error!");
                }

                if (SQtoUpdate.proposedAnswer != "?")
                {
                    throw new Exception("Question has been answered!");
                }

                //Save proposed answer.
                SQtoUpdate.proposedAnswer = payload.proposedAnswer;
                
                var question = context.QuestionItems.Find(QuestionID);

                context.SaveChangesAsync();

                if (question.identifier == SQtoUpdate.proposedAnswer)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { answerEvaluation = "correct" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { answerEvaluation = "incorrect" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
        }
    }
}
