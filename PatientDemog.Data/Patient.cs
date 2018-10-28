using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemog.Data
{
    [DataContract]
    public class Patient: BaseEntity
    {
        public Patient()
        {
            Phones = new List<Phone>();
        }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public string Gender { get; set; }
        public IList<Phone> Phones { get; set; }

        public override bool IsValid()
        {
            if(string.IsNullOrEmpty(FirstName ) || FirstName.Length<3)
            {
                return false;
            }
            if (string.IsNullOrEmpty(LastName ) || LastName .Length < 3)
            {
                return false;
            }
            return true;
        }
    }
}
