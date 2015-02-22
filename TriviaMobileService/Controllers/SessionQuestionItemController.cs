using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using TriviaMobileService.DataObjects;
using TriviaMobileService.Models;

namespace TriviaMobileService.Controllers
{
    public class SessionQuestionItemController : TableController<SessionQuestionItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SessionQuestionItem>(context, Request, Services);
        }

        // GET tables/SessionQuestionItem
        public IQueryable<SessionQuestionItem> GetAllSessionQuestionItem()
        {
            return Query(); 
        }

        // GET tables/SessionQuestionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SessionQuestionItem> GetSessionQuestionItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/SessionQuestionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SessionQuestionItem> PatchSessionQuestionItem(string id, Delta<SessionQuestionItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/SessionQuestionItem
        public async Task<IHttpActionResult> PostSessionQuestionItem(SessionQuestionItem item)
        {
            SessionQuestionItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/SessionQuestionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSessionQuestionItem(string id)
        {
             return DeleteAsync(id);
        }

    }
}