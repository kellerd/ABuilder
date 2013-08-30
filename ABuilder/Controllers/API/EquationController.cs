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
    public class EquationController : ApiController
    {
        private ABuilderContainer db = new ABuilderContainer();

        // GET api/Equation
        public IEnumerable<Equation> GetEquations()
        {
            return db.Equations.AsEnumerable();
        }

        // GET api/Equation/5
        public Equation GetEquation(int id)
        {
            Equation equation = db.Equations.Find(id);
            if (equation == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return equation;
        }

        // PUT api/Equation/5
        public HttpResponseMessage PutEquation(int id, Equation equation)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != equation.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(equation).State = EntityState.Modified;

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

        // POST api/Equation
        public HttpResponseMessage PostEquation(Equation equation)
        {
            if (ModelState.IsValid)
            {
                db.Equations.Add(equation);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, equation);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = equation.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Equation/5
        public HttpResponseMessage DeleteEquation(int id)
        {
            Equation equation = db.Equations.Find(id);
            if (equation == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Equations.Remove(equation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, equation);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}