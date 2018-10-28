using PatientDemog.Common;
using PatientDemog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PatientDemog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
               {
                    //read data from api service
                    ServiceRepository serviceObj = new ServiceRepository();
                    HttpResponseMessage response = serviceObj.GetResponse("api/patients/");
                    response.EnsureSuccessStatusCode();
                    List < PatientDemog.Data.Patient > patients = response.Content.ReadAsAsync<List<PatientDemog.Data.Patient>>().Result;
                    ViewBag.Title = "All Patients";
                    //display view
                     return View(patients);
                 }
                 catch (Exception e)
                 {
                     throw;
                 }
        }
        public ActionResult Create()
        {
            Patient patient = Factory.CreatePatient();

            return View(patient );
        }
        [HttpPost]
        public ActionResult Create(Patient patient)
        {  
            if(ModelState.IsValid)
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse ("api/patients/", patient );

                response.EnsureSuccessStatusCode();  
                return RedirectToAction("Index");
            }
            else
            {
                return View(patient);
            }  
        }

}
  }
