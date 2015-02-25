using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.Models;
using TriviaMobileService.DataObjects;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace TriviaMobileService.Controllers
{
    public class playerprogressController : ApiController
    {
        public ApiServices Services { get; set; }
        MobileServiceContext context = new MobileServiceContext();

        /// <summary>
        /// Retrieve all questions the player has in the current game.
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="gamesessionid"></param>
        /// <returns></returns>
        [AuthorizeLevel(AuthorizationLevel.Application)]
        [Route("api/playerprogress")]
        public HttpResponseMessage Get(string playerid, string gamesessionid)
        {
            try
            {
                if (playerid == null || gamesessionid == null)
                {
                    throw new Exception("key not found!");
                }

                // Check if Game session is valid.
                var sessionItem = context.SessionItems.Find(gamesessionid);

                if (sessionItem == null || sessionItem.playerid != playerid)
                {
                    throw new Exception("PlayerSession Error!");
                }

                // Get all questions in this session.
                var questions = from sq in context.SessionQuestionItems
                                join q in context.QuestionItems
                                on sq.QuestionID equals q.Id
                                where sq.GameSessionID == gamesessionid
                                select new { q.Id, questionText = q.QuestionText, answerOne = q.AnswerOne, answerTwo = q.AnswerTwo, answerThree = q.AnswerThree, answerFour = q.AnswerFour, sq.proposedAnswer };

                JArray JQuestions = new JArray();

                foreach (var question in questions)
                {
                    JQuestions.Add(JObject.FromObject(new
                    {
                        id = question.Id,
                        questionText = question.questionText,
                        answerOne = question.answerOne,
                        answerTwo = question.answerTwo,
                        answerThree = question.answerThree,
                        answerFour = question.answerFour,
                        proposedAnswer = question.proposedAnswer
                    }));
                }

                return Request.CreateResponse(HttpStatusCode.OK, JQuestions);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update a question with the results of the player's answer.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [AuthorizeLevel(AuthorizationLevel.Application)]
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

                // Check if game session is valid.
                var sessionItem = context.SessionItems.Find(SessionID);

                if (sessionItem == null || sessionItem.playerid != PlayerID)
                {
                    throw new Exception("PlayerSession Error!");
                }

                // Get this question by session and id.
                SessionQuestionItem SQtoUpdate = context.SessionQuestionItems.Where(p => p.GameSessionID == SessionID && p.QuestionID == QuestionID).FirstOrDefault();

                if (SQtoUpdate == null)
                {
                    throw new Exception("SessionQuestion Error!");
                }

                // Prevent update a question that has been answered.
                if (SQtoUpdate.proposedAnswer != "?")
                {
                    throw new Exception("Question has been answered!");
                }

                //Save proposed answer.
                SQtoUpdate.proposedAnswer = payload.proposedAnswer;
                
                var question = context.QuestionItems.Find(QuestionID);

                // Always put Save() right before returning.
                context.SaveChangesAsync();

                if (question.Identifier == SQtoUpdate.proposedAnswer)
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
