//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cursovaia.Logic.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class History
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public int IdVacancy { get; set; }
        public int IdApplicant { get; set; }
        public int Status { get; set; }
        public System.DateTime AddDate { get; set; }
    
        public virtual Applicant APPLICANT { get; set; }
        public virtual Employee EMPLOYEE { get; set; }
        public virtual Vacancy VACANCY { get; set; }
    }
}
