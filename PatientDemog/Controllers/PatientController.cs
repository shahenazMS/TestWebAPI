using PatientDemog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientDemog.Controllers
{
    public class PatientsController : ApiController
    {
        private IRepository<Patient> _db;
        public PatientsController(IRepository<Patient> db)
        {
            _db = db;
        }
        public IEnumerable<Patient> Get()
        {
            IEnumerable<Patient> patientList = _db.GetAll();
            return patientList;
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Patient patient)
        {
            try
            {
                if(!patient.IsValid())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data not valid.");
                }
                _db.Insert(patient);
                return Request.CreateResponse (HttpStatusCode.Created, "Success");
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error", e);
           }

        }
        protected override void Dispose(bool disposing)
        {
            if(_db!=null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
