using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cursovaia.Logic.Model
{
    public enum TypeAction { 
        Create,
        Change,
        Delete
    }
    public enum PageAction
    {
        None,
        Appplicant,
        Organization,
        ShereProfession,
        Speciality,
        Employee,
        Profession,
        ProfessionSkill,
        Vacancy
    }
    
}
