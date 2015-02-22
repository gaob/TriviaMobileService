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
    public class SessionItemController : TableController<SessionItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SessionItem>(context, Request, Services);
        }

        // GET tables/SessionItem
        public IQueryable<SessionItem> GetAllSessionItem()
        {
            return Query(); 
        }

        // GET tables/SessionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SessionItem> GetSessionItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/SessionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SessionItem> PatchSessionItem(string id, Delta<SessionItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/SessionItem
        public async Task<IHttpActionResult> PostSessionItem(SessionItem item)
        {
            SessionItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/SessionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSessionItem(string id)
        {
             return DeleteAsync(id);
        }

    }
}