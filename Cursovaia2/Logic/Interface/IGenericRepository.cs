using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cursovaia.Logic.Interface
{
    public interface IGenericRepository<T> where T:class
    {
        IEnumerable<T> SelectAll();
        T SelectById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object Id);
        void Save();
    }
}
