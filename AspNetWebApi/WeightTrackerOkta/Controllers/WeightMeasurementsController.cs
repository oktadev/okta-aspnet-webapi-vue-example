using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeightTrackerOkta.Data;
using WeightTrackerOkta.Models;

namespace WeightTrackerOkta.Controllers
{
    [Authorize]
    public class WeightMeasurementsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WeightMeasurements
        public IQueryable<WeightMeasurement> GetWeightMeasurements() => db.WeightMeasurements;

        // GET: api/WeightMeasurements/5
        [ResponseType(typeof(WeightMeasurement))]
        public async Task<IHttpActionResult> GetWeightMeasurement(int id)
        {
            var weightMeasurement = await db.WeightMeasurements.FindAsync(id);
            if (weightMeasurement == null)
            {
                return NotFound();
            }

            return Ok(weightMeasurement);
        }

        // PUT: api/WeightMeasurements/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeightMeasurement(int id, WeightMeasurement weightMeasurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != weightMeasurement.Id)
            {
                return BadRequest();
            }

            db.Entry(weightMeasurement).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeightMeasurementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WeightMeasurements
        [ResponseType(typeof(WeightMeasurement))]
        public async Task<IHttpActionResult> PostWeightMeasurement(WeightMeasurement weightMeasurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WeightMeasurements.Add(weightMeasurement);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = weightMeasurement.Id }, weightMeasurement);
        }

        // DELETE: api/WeightMeasurements/5
        [ResponseType(typeof(WeightMeasurement))]
        public async Task<IHttpActionResult> DeleteWeightMeasurement(int id)
        {
            var weightMeasurement = await db.WeightMeasurements.FindAsync(id);
            if (weightMeasurement == null)
            {
                return NotFound();
            }

            db.WeightMeasurements.Remove(weightMeasurement);
            await db.SaveChangesAsync();

            return Ok(weightMeasurement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WeightMeasurementExists(int id) => db.WeightMeasurements.Count(e => e.Id == id) > 0;
    }
}