using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using Ninject;

namespace Cursovaia.ViewModel
{
    class ApplicantViewModel : BaseViewModel
    {
        readonly IGenericRepository<Applicant> applicant;
        public List<Applicant> sourse { get {
            return this.applicant.SelectAll().ToList();
        } }
        public ApplicantViewModel(IGenericRepository<Applicant> app) {
            this.applicant = app;
            InitializeCommands();
        }
    }
}
