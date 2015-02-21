using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.Models;
using TriviaMobileService.DataObjects;
using System.Web.Http.Controllers;

namespace TriviaMobileService.Controllers
{
    public class triviaquestionsController : TableController<QuestionItem>
    {
        MobileServiceContext context = new MobileServiceContext();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            DomainManager = new EntityDomainManager<QuestionItem>(context, Request, Services);
        }

        // GET api/triviaquestions
        public IQueryable<QuestionItem> Get()
        {
            return Query();
        }
    }
}
