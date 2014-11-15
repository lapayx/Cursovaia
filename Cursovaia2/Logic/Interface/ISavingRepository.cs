using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cursovaia.Logic.Interface
{
    /// <summary>
    /// Интерфейс базового репозитория с возможностью сохраниения.
    /// </summary>
    public interface ISavingRepository
    {
        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        int SaveChanges();
    }
}
