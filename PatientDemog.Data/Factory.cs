using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PatientDemog.Data
{
    public class Factory
    {
        public static Patient CreatePatient(SqlDataReader r = null)
        {
            Patient patient = new Patient();
            if (r != null)
            { 
                patient.id = (int)r["id"];
                patient.FirstName = r["FirstName"].ToString();
                patient.LastName = r["lastname"].ToString();
                if (r["Gender"] != null)
                {
                    patient.Gender = r["Gender"].ToString();

                }
                if (r["BirthDate"] != null)
                {
                    patient.BirthDate = (DateTime)r["BirthDate"];

                }
            }
            return patient;
        }
        public static Phone CreatePhone(SqlDataReader r = null)
        {
            Phone phone = new Data.Phone();
            if (r != null)
            {
                phone.Id = (int)r["id"];
                phone.Type = r["Type"].ToString();
                phone.PhoneNumber = r["Phonenumber"].ToString();
            }
            return phone;
        }
    }
}
