using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using Ninject;
using System.Windows.Input;
using MahApps.Metro.Controls;

using System.Threading.Tasks;

namespace Cursovaia.ViewModel
{
    class ApplicantCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<Applicant> applicant;
        private Applicant newApplicant;
        private Dictionary<string, bool> cheakList;
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
        public DateTime birthday
        {
            get { return this.newApplicant.Birthday; }
            set { this.newApplicant.Birthday = value; RaisePropertyChanged("birthday"); }
        }
      
        public string lastWorkSpace
        {
            get { return this.newApplicant.LastWorkSpace; }
            set { this.newApplicant.LastWorkSpace = value.Trim(); RaisePropertyChanged("lastWorkSpace"); }
        }


        public string education
        {
            get { return this.newApplicant.Education; }
            set { this.newApplicant.Education = value.Trim(); RaisePropertyChanged("education"); }
        }
        public string family
        {
            get { return this.newApplicant.Family; }
            set { this.newApplicant.Family = value.Trim(); RaisePropertyChanged("family"); }
        }
        public string socialStatus
        {
            get { return this.newApplicant.SocialStatus; }
            set { this.newApplicant.SocialStatus = value.Trim(); RaisePropertyChanged("socialStatus"); }
        }
        public ApplicantCreateViewModel(IGenericRepository<Applicant> app) {
            this.applicant = app;
            this.newApplicant = new Applicant { 
                Birthday = DateTime.Now
            
            
            };
            this.cheakList = new Dictionary<string, bool>();
            cheakList.Add("firstName", false);
            cheakList.Add("secondName", false);
            cheakList.Add("fatherName", false);
            cheakList.Add("lastWorkSpace", false);
            cheakList.Add("education", false);
            cheakList.Add("family", false);

            InitializeCommands();
        }

        #region Comand
        private ICommand savePrivate;
        public ICommand Save
        {
            get
            {
                return savePrivate;
            }
            set
            {
                savePrivate = value;
                RaisePropertyChanged("Save");
            }
        }

        private ICommand cancelPrivate;
        public ICommand Cancel
        {
            get
            {
                return cancelPrivate;
            }
            set
            {
                cancelPrivate = value;
                RaisePropertyChanged("Cancel");
            }
        }
        #endregion
        protected override void InitializeCommands()
            
        {
            base.InitializeCommands();
            Save = new RelayCommand<string>(SaveApplicant);
            Cancel = new RelayCommand<string>(CancelApplicant);

        }
        private  void SaveApplicant( string s)
        {
            if (cheakList.Where(x => x.Value == false).Count() > 0)
            {

                DIConfig.MainVindow.ShowMessage("Ошибка", "Не все поля заполнены верно");
                return; 
            }
            applicant.Insert(newApplicant);
            applicant.Save();
            GoToReferense("applicant");
        
        }
        private void CancelApplicant(string s) {

            GoToReferense("applicant");
        }
        public override  string this[string columnName]
        {
            get
            {
                if (cheakList.ContainsKey(columnName)) 
                    cheakList[columnName] = false;
                
                switch (columnName)
                {

                    case "firstName":
                        if (this.newApplicant.FirstName != null && this.newApplicant.FirstName.Length < 2)
                            return "Слишком короткое имя";
                        cheakList[columnName] = (this.newApplicant.FirstName != null)? true : false;
                        break;
                    case "secondName":
                        if (this.newApplicant.SecondName != null && this.newApplicant.SecondName.Length < 2)
                            return "Слишком короткая Фамилия";
                        cheakList[columnName] = (this.newApplicant.SecondName != null) ? true : false;
                        break;
                    case "fatherName":
                        if (this.newApplicant.FatherName != null && this.newApplicant.FatherName.Length < 2)
                            return "Слишком короткая Отчество";
                        cheakList[columnName] = (this.newApplicant.FatherName != null) ? true : false;
                        break;
                    case "lastWorkSpace":
                        if (this.newApplicant.LastWorkSpace != null && this.newApplicant.LastWorkSpace.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.newApplicant.LastWorkSpace != null) ? true : false;
                        break;
                    case "education":
                        if (this.newApplicant.Education != null && this.newApplicant.Education.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.newApplicant.Education != null) ? true : false;
                        break;
                    case "family":
                        if (this.newApplicant.Family != null && this.newApplicant.Family.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.newApplicant.Family != null) ? true : false;
                        break;
                    case "socialStatus":
                        if (this.newApplicant.SocialStatus != null && this.newApplicant.SocialStatus.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.newApplicant.SocialStatus != null) ? true : false;
                        break;
                    case "birthday":
                        if (this.newApplicant.Birthday > DateTime.Now.AddYears(-14) )
                            return "неверно выбрана дата рождения";
                        cheakList[columnName] = (this.newApplicant.Birthday != null) ? true : false;
                        break;

                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
