using Cursovaia.Logic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Cursovaia.Logic.Interface
{
    public interface IGenericRepository<T> where T:class
    {

        /*только для курсача...*/
        WIN_Server db { get; set; }
        DbSet<T> table{get;set;}

        IEnumerable<T> SelectAll();
        T SelectById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object Id);
        void Save();
    }
}
