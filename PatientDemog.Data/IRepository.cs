using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemog.Data
{
    [DataContract]
    public abstract class BaseEntity
    {
         public abstract bool IsValid(); 
    }

    public interface IRepository<T>: IDisposable where T: BaseEntity 
    {
        IEnumerable<T> GetAll();
        bool Insert(T entity);
    }
}
