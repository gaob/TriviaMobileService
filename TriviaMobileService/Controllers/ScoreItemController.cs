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
    public class ScoreItemController : TableController<ScoreItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ScoreItem>(context, Request, Services);
        }

        // GET tables/ScoreItem
        public IQueryable<ScoreItem> GetAllScoreItem()
        {
            return Query(); 
        }

        // GET tables/ScoreItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ScoreItem> GetScoreItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ScoreItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ScoreItem> PatchScoreItem(string id, Delta<ScoreItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/ScoreItem
        public async Task<IHttpActionResult> PostScoreItem(ScoreItem item)
        {
            ScoreItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ScoreItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteScoreItem(string id)
        {
             return DeleteAsync(id);
        }

    }
}