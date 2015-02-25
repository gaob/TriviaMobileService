using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.Models;
using TriviaMobileService.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace TriviaMobileService.Controllers
{
    public class endgamesessionController : ApiController
    {
        public ApiServices Services { get; set; }
        MobileServiceContext context = new MobileServiceContext();

        /// <summary>
        /// End a game session.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [AuthorizeLevel(AuthorizationLevel.Application)]
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
                // Do we want to save the score?
                bool SaveScore = true;

                // Check if player session is valid.
                var sessionItem = context.SessionItems.Find(SessionID);

                if (sessionItem == null || sessionItem.playerid != PlayerID)
                {
                    throw new Exception("PlayerSession Error!");
                }

                //Remove from SessionItems
                context.SessionItems.Remove(sessionItem);

                //Get questions from this session.
                var questions = from sq in context.SessionQuestionItems
                                join q in context.QuestionItems
                                on sq.QuestionID equals q.Id
                                where sq.GameSessionID == SessionID
                                select new { sq.proposedAnswer, identifier = q.Identifier};

                int score = 0;

                foreach (var question in questions)
                {
                    //The player ends the game in the middle of the session, don't save the score.
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

                // Get SessionQuestionItems to delete.
                var SQItemToDrop = from sq in context.SessionQuestionItems
                                   where sq.GameSessionID == SessionID
                                   select sq;

                foreach (var SQItem in SQItemToDrop)
                {
                    context.SessionQuestionItems.Remove(SQItem);
                }

                // We don't need to save score, then return.
                if (!SaveScore)
                {
                    context.SaveChangesAsync();

                    return Request.CreateResponse(HttpStatusCode.OK, new { score = "-1", highscorebeat = "-1" });
                }

                // Negative score resets.
                if (score < 0)
                {
                    score = 0;
                }

                // How many scores does this player have?
                int currCount = context.ScoreItems.Where(p => p.playerid == PlayerID).Count();
                ScoreItem StoUpdate = null;
                // -1: no high score beat, 0: new high score, otherwise the high score beat.
                int highscorebeat = -1;

                //No score recorded yet
                if (currCount == 0)
                {
                    context.ScoreItems.Add(new ScoreItem { Id = Guid.NewGuid().ToString(), playerid = PlayerID, score = score, occurred = DateTime.Now});
                    highscorebeat = 0;
                } 
                //There are existing scores.
                else 
                {
                    // Retrieve the score item with same score.
                    StoUpdate = context.ScoreItems.Where(p => p.playerid == PlayerID && p.score == score).FirstOrDefault();
                    // Calculate which highest score beat.
                    highscorebeat = context.ScoreItems.Where(p => p.playerid == PlayerID && p.score >= score).Count() + 1;
                    
                    // Same score exists, overwrite Date and Time to existing score
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
                            // This means no high score beat.
                            if (currCount < highscorebeat)
                            {
                                highscorebeat = -1;
                            }
                        }
                        //When the current score count is 10, always drop the lowest score, and add the current one.
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
