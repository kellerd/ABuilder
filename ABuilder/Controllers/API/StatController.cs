using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ABuilder.Models;

namespace ABuilder.Controllers.API
{
    public class StatController : ApiController
    {
        private ABuilderContainer db = new ABuilderContainer();

        // GET api/Stat
        public IEnumerable<Stat> GetStats()
        {
            return db.Stats.AsEnumerable();
        }

        // GET api/Stat/5
        public Stat GetStat(int id)
        {
            Stat stat = db.Stats.Find(id);
            if (stat == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return stat;
        }

        // PUT api/Stat/5
        public HttpResponseMessage PutStat(int id, Stat stat)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != stat.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(stat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Stat
        public HttpResponseMessage PostStat(Stat stat)
        {
            if (ModelState.IsValid)
            {
                db.Stats.Add(stat);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, stat);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = stat.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Stat/5
        public HttpResponseMessage DeleteStat(int id)
        {
            Stat stat = db.Stats.Find(id);
            if (stat == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Stats.Remove(stat);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, stat);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}