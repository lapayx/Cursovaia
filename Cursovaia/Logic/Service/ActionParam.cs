using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.Model;
using Cursovaia.Logic.Interface;

namespace Cursovaia.Logic.Service
{
    public class ActionParam : IActionParam
    {
        public TypeAction TypeAction { get; set; }

        public PageAction Action { get; set; }

        public object Parameter { get; set; }




        public void Set(PageAction action) 
        {
            this.Set(action, TypeAction.Create, null);
        }
        public void Set(PageAction action, TypeAction typeAction)
        {
            this.Set(action, typeAction, null);
        }
        public void Set(PageAction action, TypeAction typeAction = TypeAction.Create, object parameter = null)
        {
            this.TypeAction = typeAction;
            this.Action = action;
            this.Parameter = parameter;
        }
    }
}
