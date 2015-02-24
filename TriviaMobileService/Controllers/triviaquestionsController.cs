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
using Newtonsoft.Json.Linq;

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

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [Route("api/triviaquestions/{id}")]
        public SingleResult<QuestionToClient> GetTodoItem(string id)
        {
            var result = Lookup(id).Queryable.Select(x => new QuestionToClient()
            {
                Id = x.Id,
                questionText = x.questionText,
                answerOne = x.answerOne,
                answerTwo = x.answerTwo,
                answerThree = x.answerThree,
                answerFour = x.answerFour
            });

            return SingleResult<QuestionToClient>.Create(result);
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

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [Route("api/triviaquestions/{triviaQCount:int}")]
        public HttpResponseMessage GetTodoItem(int triviaQCount)
        {
            try
            {
                if (triviaQCount <= 0 || triviaQCount > 30)
                {
                    throw new Exception("Invalid triviaQCount!");
                }

                JArray JQuestions = new JArray();

                var questions = Query().OrderBy(c => Guid.NewGuid()).Take(triviaQCount);

                foreach (var question in questions)
                {
                    JQuestions.Add(JObject.FromObject(new
                    {
                        id = question.Id,
                        questionText = question.questionText,
                        answerOne = question.answerOne,
                        answerTwo = question.answerTwo,
                        answerThree = question.answerThree,
                        answerFour = question.answerFour
                    }));
                }

                return Request.CreateResponse(HttpStatusCode.OK, JQuestions);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
        }
    }
}
