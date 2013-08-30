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
    public class SingleModel_StatController : ApiController
    {
        private ABuilderContainer db = new ABuilderContainer();

        // GET api/SingleModel_Stat
        public IEnumerable<SingleModel_Stat> GetSingleModel_Stat()
        {
            var singlemodel_stat = db.SingleModel_Stat.Include(s => s.Stat);
            return singlemodel_stat.AsEnumerable();
        }

        // GET api/SingleModel_Stat/5
        public SingleModel_Stat GetSingleModel_Stat(int SingleModelId, int StatId)
        {
            SingleModel_Stat singlemodel_stat = db.SingleModel_Stat.Find(SingleModelId, StatId);
            if (singlemodel_stat == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return singlemodel_stat;
        }

        // PUT api/SingleModel_Stat/5
        public HttpResponseMessage PutSingleModel_Stat(int SingleModelId, int StatId, SingleModel_Stat singlemodel_stat)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (SingleModelId != singlemodel_stat.SingleModelId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            if (StatId != singlemodel_stat.StatId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            db.Entry(singlemodel_stat).State = EntityState.Modified;

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

        // POST api/SingleModel_Stat
        public HttpResponseMessage PostSingleModel_Stat(SingleModel_Stat singlemodel_stat)
        {
            if (ModelState.IsValid)
            {
                db.SingleModel_Stat.Add(singlemodel_stat);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, singlemodel_stat);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = singlemodel_stat.SingleModelId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SingleModel_Stat/5
        public HttpResponseMessage DeleteSingleModel_Stat(int SingleModelId, int StatId)
        {
            SingleModel_Stat singlemodel_stat = db.SingleModel_Stat.Find(SingleModelId, StatId);
            if (singlemodel_stat == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.SingleModel_Stat.Remove(singlemodel_stat);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, singlemodel_stat);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}