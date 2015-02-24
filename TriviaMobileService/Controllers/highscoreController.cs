using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.DataObjects;
using System.Web.Http.Controllers;
using TriviaMobileService.Models;
using Newtonsoft.Json.Linq;

namespace TriviaMobileService.Controllers
{
    public class highscoreController : TableController<ScoreItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ScoreItem>(context, Request, Services);
        }

        [Route("api/highscore")]
        public HttpResponseMessage Get(string playerid)
        {
            try
            {
                JArray JScores = new JArray();

                var highscores = Query().Where(x => x.playerid == playerid).OrderByDescending(x => x.score);

                foreach (var highscore in highscores)
                {
                    JScores.Add(JObject.FromObject(new
                    {
                        score = highscore.score,
                        occurred = highscore.occurred
                    }));
                }

                return Request.CreateResponse(HttpStatusCode.OK, JScores);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
        }
    }
}
