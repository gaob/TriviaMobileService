using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.DataObjects;
using TriviaMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace TriviaMobileService.Controllers
{
    /// <summary>
    /// Standard Table Controller to access QuestionItem table, all require master key.
    /// </summary>
    public class QuestionItemController : TableController<QuestionItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<QuestionItem>(context, Request, Services);
        }

        [AuthorizeLevel(AuthorizationLevel.Admin)]
        public IQueryable<QuestionItem> GetAllQuestionItems()
        {
            return Query();
        }

        [AuthorizeLevel(AuthorizationLevel.Admin)]
        public SingleResult<QuestionItem> GetQuestionItem(string id)
        {
            return Lookup(id);
        }

        [AuthorizeLevel(AuthorizationLevel.Admin)]
        public Task<QuestionItem> PatchQuestionItem(string id, Delta<QuestionItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        [AuthorizeLevel(AuthorizationLevel.Admin)]
        public async Task<IHttpActionResult> PostQuestionItem(QuestionItem item)
        {
            QuestionItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [AuthorizeLevel(AuthorizationLevel.Admin)]
        public Task DeleteQuestionItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}
