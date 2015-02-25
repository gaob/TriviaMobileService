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
    public class endgamesessionController : ApiController
    {
        public ApiServices Services { get; set; }
        MobileServiceContext context = new MobileServiceContext();

        [Route("api/endgamesession")]
        public HttpResponseMessage Post([FromBody]dynamic payload)
        {
            try
            {
                if (payload.playerid == null || payload.gamesessionid == null)
                {
                    throw new Exception("key not found!");
                }

                string SessionID = payload.gamesessionid;
                string PlayerID = payload.playerid;
                bool SaveScore = true;

                var sessionItem = context.SessionItems.Find(SessionID);

                if (sessionItem == null || sessionItem.playerid != PlayerID)
                {
                    throw new Exception("PlayerSession Error!");
                }

                //Remove from SessionItems
                context.SessionItems.Remove(sessionItem);

                var questions = from sq in context.SessionQuestionItems
                                join q in context.QuestionItems
                                on sq.QuestionID equals q.Id
                                where sq.GameSessionID == SessionID
                                select new { sq.proposedAnswer, q.identifier};

                int score = 0;

                foreach (var question in questions)
                {
                    //The player ends the game in the middle of the session
                    if (question.proposedAnswer == "?")
                    {
                        SaveScore = false;
                        break;
                    }

                    if (question.proposedAnswer == question.identifier)
                    {
                        score++;
                    }
                    else
                    {
                        score--;
                    }
                }

                var SQItemToDrop = from sq in context.SessionQuestionItems
                                   where sq.GameSessionID == SessionID
                                   select sq;

                foreach (var SQItem in SQItemToDrop)
                {
                    context.SessionQuestionItems.Remove(SQItem);
                }

                if (!SaveScore)
                {
                    context.SaveChangesAsync();

                    return Request.CreateResponse(HttpStatusCode.OK, new { score = "-1", highscorebeat = "-1" });
                }

                if (score < 0)
                {
                    score = 0;
                }

                int currCount = context.ScoreItems.Where(p => p.playerid == PlayerID).Count();
                ScoreItem StoUpdate = null;
                int highscorebeat = -1;

                //No score recorded yet
                if (currCount == 0)
                {
                    context.ScoreItems.Add(new ScoreItem { Id = Guid.NewGuid().ToString(), playerid = PlayerID, score = score, occurred = DateTime.Now});
                    highscorebeat = 0;
                } 
                else 
                {
                    StoUpdate = context.ScoreItems.Where(p => p.playerid == PlayerID && p.score == score).FirstOrDefault();
                    highscorebeat = context.ScoreItems.Where(p => p.playerid == PlayerID && p.score >= score).Count() + 1;
                    
                    //Overwrite Date and Time to existing score
                    if (StoUpdate != null)
                    {
                        StoUpdate.occurred = DateTime.Now;
                        //If is updating the last score
                        if (currCount+1 == highscorebeat) {
                            highscorebeat = -1;
                        }
                    } 
                    else 
                    {
                        //When the current score count is less than 10, always add.
                        if (currCount < 10)
                        {
                            context.ScoreItems.Add(new ScoreItem { Id = Guid.NewGuid().ToString(), playerid = PlayerID, score = score, occurred = DateTime.Now });
                            if (currCount < highscorebeat)
                            {
                                highscorebeat = -1;
                            }
                        }
                        //When the current score count is 10.
                        else if (currCount == 10)
                        {
                            var StoDrops = context.ScoreItems.Where(x => x.playerid == PlayerID).OrderBy(x => x.score);

                            if (StoDrops.Count() == 0)
                            {
                                throw new Exception("Invalid code path!");
                            }

                            var StoDrop = StoDrops.FirstOrDefault();
                            context.ScoreItems.Remove(StoDrop);

                            context.ScoreItems.Add(new ScoreItem { Id = Guid.NewGuid().ToString(), playerid = PlayerID, score = score, occurred = DateTime.Now });
                        }
                        else
                        {
                            throw new Exception("Invalid code path!");
                        }
                    }
                }

                context.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, new { score = score, highscorebeat = highscorebeat });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
        }
    }
}
