using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cursovaia.Logic.DataBase;

namespace Cursovaia.Logic.Interface
{
    public interface IEmplayeeRepository
    {
    
            /// <summary>
            /// Таблица Соискателей.
            /// </summary>
            IDbSet<Employee> Emplayees { get; }

        
    }
}
