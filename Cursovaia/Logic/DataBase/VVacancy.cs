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
    
    public partial class VVacancy
    {
        public int Id { get; set; }
        public int IdOrganization { get; set; }
        public int IdProfession { get; set; }
        public string ABout { get; set; }
        public int Status { get; set; }
        public int MinExperience { get; set; }
        public int MaxExperience { get; set; }
        public System.DateTime DateAdd { get; set; }
        public string NameOrganization { get; set; }
        public string NameProfession { get; set; }
    }
}
