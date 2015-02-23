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
        public IQueryable<ScoreItem> Get(string playerid)
        {
            return Query().Where(x => x.playerid == playerid);
        }
    }
}
