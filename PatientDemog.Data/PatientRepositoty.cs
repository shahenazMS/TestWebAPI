using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PatientDemog.Data
{
    // Patient repository class
    public class PatientRepository : IRepository<Patient>
    {
        private string constr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PatientDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection db;
        public PatientRepository()
        {
            //open connection
            db = new SqlConnection(constr);
            db.Open();

        }
        //read all patient from database
        public IEnumerable<Patient> GetAll()
        {
            IList<Patient> patientList = new List<Patient>();
            SqlCommand cmd = db.CreateCommand();
            cmd.CommandText = "select * from patients";
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read() )
            {
                //build patient from data reader
                patientList.Add(Factory.CreatePatient (r));
            }
            r.Close();

            foreach (Patient p in patientList)
            {
                cmd.CommandText = "select * from phones where patientId=" + p.id.ToString();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    //build phone entity from data reader
                    p.Phones.Add(Factory.CreatePhone(r));
                }
                r.Close();
            }
            return patientList;
        }

        public bool Insert(Patient entity)
        {
            bool result = false;

            if(!entity.IsValid())
            {
                return  result;
            }
            try
            {
                //insert patient details
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "insert into Patients(firstname,lastname,birthdate,gender)" +
                    "values ('" + entity.FirstName + "','" + entity.LastName + "','" + entity.BirthDate + "','" + entity.Gender + "'); SELECT SCOPE_IDENTITY()";
                //get inserted id
                object  Patientid=cmd.ExecuteScalar();

                // insert phone numbers
                foreach (Phone ph in entity.Phones )
                {
                    cmd.CommandText = "insert into phones(patientid,type,phonenumber)" +
                                        "values (" + Patientid + ",'" + ph.Type + "' , '" + ph.PhoneNumber + "')";
                    cmd.ExecuteNonQuery();
                }
                result = true;   
            }
            catch (Exception e)
            {
                // log exception
            }
            return result;
        }
        //clean up
        public void Dispose()
        {
            if(db!=null)
            {
                db.Close();
                db.Dispose();
            }
        }

        
    }
}
