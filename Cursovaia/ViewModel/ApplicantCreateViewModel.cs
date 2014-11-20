using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using Ninject;

namespace Cursovaia.ViewModel
{
    class ApplicantCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<Applicant> applicant;
        int? _initializeCommands ;
        public int? test { get { return this._initializeCommands; }
            set { this._initializeCommands = value; RaisePropertyChanged("test"); }
        }
        public List<Applicant> sourse { get {
            return this.applicant.SelectAll().ToList();
        } }
        public ApplicantCreateViewModel(IGenericRepository<Applicant> app) {
            this.applicant = app;
        }
        
        public override  string this[string columnName]
        {
            get
            {
                if (columnName == "test" && this._initializeCommands<10)
                {
                    return "Number is not greater than 10!";
                }


                return null;
            }
        }
    }
}
