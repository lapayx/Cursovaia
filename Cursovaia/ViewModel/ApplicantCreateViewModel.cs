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
using Cursovaia.Logic.Model;

namespace Cursovaia.ViewModel
{
    class ApplicantCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<Applicant> repository;

        private Applicant Applicant;
        private Dictionary<string, bool> cheakList;
        public string Caption { get; set; }
        private bool _isNewApplicant = true;

        #region Свойства полей формы
        public string firstName { get { return this.Applicant.FirstName; }
            set {this.Applicant.FirstName = value.Trim(); RaisePropertyChanged("firstName"); }
        }
        public string secondName
        {
            get { return this.Applicant.SecondName; }
            set { this.Applicant.SecondName = value.Trim(); RaisePropertyChanged("secondName"); }
        }
        public string fatherName
        {
            get { return this.Applicant.FatherName; }
            set { this.Applicant.FatherName = value.Trim(); RaisePropertyChanged("fatherName"); }
        }
        public DateTime birthday
        {
            get { return this.Applicant.Birthday; }
            set { this.Applicant.Birthday = value; RaisePropertyChanged("birthday"); }
        }
      
        public string lastWorkSpace
        {
            get { return this.Applicant.LastWorkSpace; }
            set { this.Applicant.LastWorkSpace = value.Trim(); RaisePropertyChanged("lastWorkSpace"); }
        }


        public string education
        {
            get { return this.Applicant.Education; }
            set { this.Applicant.Education = value.Trim(); RaisePropertyChanged("education"); }
        }
        public string family
        {
            get { return this.Applicant.Family; }
            set { this.Applicant.Family = value.Trim(); RaisePropertyChanged("family"); }
        }
        public string socialStatus
        {
            get { return this.Applicant.SocialStatus; }
            set { this.Applicant.SocialStatus = value.Trim(); RaisePropertyChanged("socialStatus"); }
        }
        #endregion


        public ApplicantCreateViewModel(IGenericRepository<Applicant> app,IActionParamService param) {
            this.repository = app;
            this.Caption = "Создание нового Соискателя";
            if (param.Action == PageAction.Appplicant && param.TypeAction != TypeAction.Create)
            {
                this.Applicant = this.repository.SelectById(param.Parameter);
                if (this.Applicant == null)
                {
                    this.Applicant = new Applicant
                   {
                       Birthday = DateTime.Now
                   };
                }
                else {
                    this.Caption = "Изменение данных соискателя";
                    this._isNewApplicant = false;
                }

            }
            else
            {
                this.Applicant = new Applicant
                {
                    Birthday = DateTime.Now
                };
            }
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
            if (this._isNewApplicant)
                repository.Insert(Applicant);
            else
                repository.Update(Applicant); ;
            repository.Save();
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
                        if (this.Applicant.FirstName != null && this.Applicant.FirstName.Length < 2)
                            return "Слишком короткое имя";
                        cheakList[columnName] = (this.Applicant.FirstName != null)? true : false;
                        break;
                    case "secondName":
                        if (this.Applicant.SecondName != null && this.Applicant.SecondName.Length < 2)
                            return "Слишком короткая Фамилия";
                        cheakList[columnName] = (this.Applicant.SecondName != null) ? true : false;
                        break;
                    case "fatherName":
                        if (this.Applicant.FatherName != null && this.Applicant.FatherName.Length < 2)
                            return "Слишком короткая Отчество";
                        cheakList[columnName] = (this.Applicant.FatherName != null) ? true : false;
                        break;
                    case "lastWorkSpace":
                        if (this.Applicant.LastWorkSpace != null && this.Applicant.LastWorkSpace.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.Applicant.LastWorkSpace != null) ? true : false;
                        break;
                    case "education":
                        if (this.Applicant.Education != null && this.Applicant.Education.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.Applicant.Education != null) ? true : false;
                        break;
                    case "family":
                        if (this.Applicant.Family != null && this.Applicant.Family.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.Applicant.Family != null) ? true : false;
                        break;
                    case "socialStatus":
                        if (this.Applicant.SocialStatus != null && this.Applicant.SocialStatus.Length < 2)
                            return "Больше 2 символов";
                        cheakList[columnName] = (this.Applicant.SocialStatus != null) ? true : false;
                        break;
                    case "birthday":
                        if (this.Applicant.Birthday > DateTime.Now.AddYears(-14) )
                            return "неверно выбрана дата рождения";
                        cheakList[columnName] = (this.Applicant.Birthday != null) ? true : false;
                        break;

                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
