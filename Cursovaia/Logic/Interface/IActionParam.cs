using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.Model;

namespace Cursovaia.Logic.Interface
{
    public interface IActionParam
    {
         TypeAction TypeAction { get; set; }

         PageAction Action { get; set; }

         object Parameter { get; set; }

        void Set(PageAction action);
        void Set(PageAction action, TypeAction typeAction);
        void Set(PageAction action, TypeAction typeAction, object parameter);
    }
}
