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
        private Applicant newApplicant;
       
        public string firstName { get { return this.newApplicant.FirstName; }
            set { this.newApplicant.FirstName = value.Trim(); RaisePropertyChanged("firstName"); }
        }
        public string secondName
        {
            get { return this.newApplicant.SecondName; }
            set { this.newApplicant.SecondName = value.Trim(); RaisePropertyChanged("secondName"); }
        }
        public string fatherName
        {
            get { return this.newApplicant.FatherName; }
            set { this.newApplicant.FatherName = value.Trim(); RaisePropertyChanged("fatherName"); }
        }
        public List<Applicant> sourse { get {
            return this.applicant.SelectAll().ToList();
        } }
        public ApplicantCreateViewModel(IGenericRepository<Applicant> app) {
            this.applicant = app;
            this.newApplicant = new Applicant();
        }
        
        public override  string this[string columnName]
        {
            get
            {
                switch (columnName)
                {

                    case "firstName":
                        if (this.newApplicant.FirstName != null && this.newApplicant.FirstName.Length < 2)
                            return "Слишком короткое имя";
                        break;
                    case "secondName":
                        if (this.newApplicant.SecondName != null && this.newApplicant.SecondName.Length < 2)
                            return "Слишком короткая Фамилия";
                        break;
                    case "fatherName":
                        if (this.newApplicant.FatherName != null && this.newApplicant.FatherName.Length < 2)
                            return "Слишком короткая Отчество";
                        break;
                    default :
                        break;
                }
           
                return null;
            }
        }
    }
}
