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
        [Route("api/triviaquestions")]
        public IQueryable<QuestionToClient> Get()
        {
            return Query().Select(x => new QuestionToClient()
            {
                Id = x.Id,
                questionText = x.questionText,
                answerOne = x.answerOne,
                answerTwo = x.answerTwo,
                answerThree = x.answerThree,
                answerFour = x.answerFour
            });
        }

        [Route("api/triviaquestions")]
        public IQueryable<QuestionItem> Post()
        {
            return Query();
        }
    }
}
