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
using Microsoft.WindowsAzure.Mobile.Service.Security;

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

        /// <summary>
        /// Return a trivia question by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizeLevel(AuthorizationLevel.Application)]
        [Route("api/triviaquestions/{id}")]
        public SingleResult<QuestionToClient> GetQuestionItem(string id)
        {
            var result = Lookup(id).Queryable.Select(x => new QuestionToClient()
            {
                Id = x.Id,
                questionText = x.QuestionText,
                answerOne = x.AnswerOne,
                answerTwo = x.AnswerTwo,
                answerThree = x.AnswerThree,
                answerFour = x.AnswerFour
            });

            return SingleResult<QuestionToClient>.Create(result);
        }

        /// <summary>
        /// Return all questions.
        /// </summary>
        /// <returns></returns>
        [AuthorizeLevel(AuthorizationLevel.Application)]
        [Route("api/triviaquestions")]
        public IQueryable<QuestionToClient> Get()
        {
            return Query().Select(x => new QuestionToClient()
            {
                Id = x.Id,
                questionText = x.QuestionText,
                answerOne = x.AnswerOne,
                answerTwo = x.AnswerTwo,
                answerThree = x.AnswerThree,
                answerFour = x.AnswerFour
            });
        }

        /// <summary>
        /// Return random questions of triviaQCount.
        /// </summary>
        /// <param name="triviaQCount"></param>
        /// <returns></returns>
        [AuthorizeLevel(AuthorizationLevel.Application)]
        [Route("api/triviaquestions")]
        public HttpResponseMessage GetQuestionItem(int triviaQCount)
        {
            try
            {
                if (triviaQCount <= 0 || triviaQCount > 30)
                {
                    throw new Exception("Invalid triviaQCount!");
                }

                JArray JQuestions = new JArray();

                // Use new Guid to randomize the questions.
                var questions = Query().OrderBy(c => Guid.NewGuid()).Take(triviaQCount);

                foreach (var question in questions)
                {
                    JQuestions.Add(JObject.FromObject(new
                    {
                        id = question.Id,
                        questionText = question.QuestionText,
                        answerOne = question.AnswerOne,
                        answerTwo = question.AnswerTwo,
                        answerThree = question.AnswerThree,
                        answerFour = question.AnswerFour
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
