using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.Interface;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using Ninject;
namespace Cursovaia.Logic.DataBase
{
    public partial class Entities<T> : IGenericRepository<T> where T:class
    {
        [Inject]
        private DbContext db {get;set;}
        public  DbSet<T> table = null;
        public Entities()
        {
           
            //this.db = new WIN_Server();
            this.table = db.Set<T>();
        }
        public Entities(DbContext db)
        {
            this.db = db;
            this.table = db.Set<T>();
    
        }
        public IEnumerable<T> SelectAll(){
            return this.table.ToList(); 
        }
        public T SelectById(object id)
        {
            return this.table.Find(id);
        }
        public void Insert(T obj) 
        {
            this.table.Add(obj);
        }
        public void Update(T obj) 
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        { 
            T exists = this.table.Find(id);
            table.Remove(exists);
        }
        public void Save() {
            db.SaveChanges();
        }
    }
    
}
