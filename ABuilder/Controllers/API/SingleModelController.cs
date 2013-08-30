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
    public class SingleModelController : ApiController
    {
        private ABuilderContainer db = new ABuilderContainer();

        // GET api/SingleModel
        public IEnumerable<SingleModel> GetSingleModels()
        {
            return db.SingleModels.AsEnumerable();
        }

        // GET api/SingleModel/5
        public SingleModel GetSingleModel(int id)
        {
            SingleModel singlemodel = db.SingleModels.Find(id);
            if (singlemodel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return singlemodel;
        }

        // PUT api/SingleModel/5
        public HttpResponseMessage PutSingleModel(int id, SingleModel singlemodel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != singlemodel.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(singlemodel).State = EntityState.Modified;

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

        // POST api/SingleModel
        public HttpResponseMessage PostSingleModel(SingleModel singlemodel)
        {
            if (ModelState.IsValid)
            {
                db.SingleModels.Add(singlemodel);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, singlemodel);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = singlemodel.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SingleModel/5
        public HttpResponseMessage DeleteSingleModel(int id)
        {
            SingleModel singlemodel = db.SingleModels.Find(id);
            if (singlemodel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.SingleModels.Remove(singlemodel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, singlemodel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}