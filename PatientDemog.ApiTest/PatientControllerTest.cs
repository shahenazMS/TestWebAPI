using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientDemog.Data;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace PatientDemog.ApiTest
{
    [TestClass]
    public class PatientControllerTest
    {
        [TestMethod]
        public void Get_Test()
        {
            //arrange
            TestRepository db = new TestRepository();
            PatientDemog.Controllers.PatientsController controller = new Controllers.PatientsController(db);
            //action
            IEnumerable<Patient> list= controller.Get();
            //asset
            Assert.IsNotNull(list);
        }
        [TestMethod]
        public void Put_Test()
        {
            //arrange
            TestRepository db = new TestRepository();
            PatientDemog.Controllers.PatientsController controller = new Controllers.PatientsController(db);
            Patient patient = Factory.CreatePatient();
            patient.FirstName = "Jason";
            patient.LastName = "Coy";
            patient.Gender = "Female";
            patient.BirthDate = new DateTime(2000, 1, 2);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            //action
            HttpResponseMessage ret = controller.Post(patient);
            //

            Assert.AreEqual (ret.StatusCode , HttpStatusCode.Created );
        }
    }

    public class TestRepository : IRepository<Patient>
    {
        private IList<Patient> patientList = new List<Patient>();

        public TestRepository()
        {
            Patient pt = Factory.CreatePatient();
            pt.FirstName = "Shahenaz";
            pt.LastName = "Sangoli";
            pt.Gender = "Female";
            pt.BirthDate = new DateTime(1999, 5, 2);
            patientList.Add(pt);

            Patient pt2 = new Patient();
            pt2.FirstName = "Kevin";
            pt2.LastName = "Emerson";
            pt2.Gender = "Male";
            pt2.BirthDate = new DateTime(1997, 4, 2);
        }
        public void Dispose()
        {
            patientList = null;
        }

        public IEnumerable<Patient> GetAll()
        {
            return patientList;
        }

        public bool Insert(Patient entity)
        {
            patientList.Add(entity);
            return true;
        }
    }
}
