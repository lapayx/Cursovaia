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
    class VacancyCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<Vacancy> repository;
        readonly IGenericRepository<Organization> organizationRepository;
        readonly IGenericRepository<Profession> professionalRepository;

        private Vacancy item;
        private Dictionary<string, bool> cheakList;
        public string Caption { get; set; }
        private bool _isNewItem = true;

        #region Свойства полей формы
        public List<Organization> organizationSourse
        {
            get { return organizationRepository.SelectAll().ToList(); }
        }
        public List<Profession> professionSourse
        {
            get { return professionalRepository.SelectAll().ToList(); }
        } 
        public List<object> actualSourse
        {
            get {
                List<object> res = new List<object>();
                res.Add(new { Name = "Актуальна", Id = 1 });
                res.Add(new { Name = "Неактуальная", Id = 0 });
                return res;

            }
        }
        public string about
        {
            get { return this.item.About; }
            set { this.item.About = value.Trim(); RaisePropertyChanged("about"); }
        }
        
        public string maxExperience
        {
            get { return item.MaxExperience.ToString(); }
            set
            {
                item.MaxExperience = int.Parse(value);
                RaisePropertyChanged("maxExperience");
            }
        }
        public string minExperience
        {
            get { return item.MinExperience.ToString(); }
            set
            {
                item.MinExperience = int.Parse(value);
                RaisePropertyChanged("minExperience");
            }
        }
        public int idProfession
        {
            get { return this.item.IdProfession; }
            set { 
                this.item.IdProfession = value;
                RaisePropertyChanged("idProfession");
            }
        }
        public int idOrganization
        {
            get { return this.item.IdOrganization; }
            set { 
                this.item.IdOrganization = value;
                RaisePropertyChanged("idOrganization");
            }
        }
        public int isActual
        {
            get { return this.item.Status; }
            set
            {
                this.item.Status = value;
                RaisePropertyChanged("isActual");
            }
        }
        #endregion


        public VacancyCreateViewModel(IGenericRepository<Vacancy> app, IGenericRepository<Organization> org, IGenericRepository<Profession> prof, IActionParamService param)
        {
            this.repository = app;
            this.professionalRepository = prof;
            this.organizationRepository = org;

            this.Caption = "Создание новой Вакансии";
            if (param.Action == PageAction.Vacancy && param.TypeAction != TypeAction.Create)
            {
                this.item = this.repository.SelectById(param.Parameter);
                if (this.item == null)
                {
                    this.item = new Vacancy
                   {
                       AddDate = DateTime.Now
                   };
                }
                else {
                    this.Caption = "Изменение данных вакансии";
                    this._isNewItem = false;
                }

            }
            else
            {
                this.item = new Vacancy
                {
                    AddDate = DateTime.Now
                };
            }
            this.cheakList = new Dictionary<string, bool>();
            cheakList.Add("about", false);
            cheakList.Add("idOrganization", false);
            cheakList.Add("idProfession", false);
            cheakList.Add("isActual", false);
            cheakList.Add("maxExperience", false);
            cheakList.Add("minExperience", false);

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
            if (this._isNewItem)
                repository.Insert(item);
            else
                repository.Update(item); ;
            repository.Save();
            GoToReferense("vacancy");
        
        }
        private void CancelApplicant(string s) {

            GoToReferense("vacancy");
        }
        public override  string this[string columnName]
        {
            get
            {
                if (cheakList.ContainsKey(columnName)) 
                    cheakList[columnName] = false;
                
                switch (columnName)
                {

                    case "about":
                        if (this.item.About != null && this.item.About.Length < 2)
                            return "Слишком короткое описание";
                        cheakList[columnName] = (this.item.About != null)? true : false;
                        break;
                    case "idOrganization":
                        if (this.item.IdOrganization != null && this.item.IdOrganization<1)
                            return "Не выбрано";
                        cheakList[columnName] = (this.item.IdOrganization != null)? true : false;
                        break;
                    case "idProfession":
                        if (this.item.IdProfession!= null && this.item.IdProfession < 1)
                            return "Не выбрано";
                        cheakList[columnName] = (this.item.IdProfession != null) ? true : false;
                        break;
                    case "isActual":
                        if (this.item.Status != null && this.item.Status < 0)
                            return "Не выбрано";
                        cheakList[columnName] = (this.item.Status != null) ? true : false;
                        break;
                    case "maxExperience":
                        if (this.item.MaxExperience != null && this.item.MaxExperience < 0)
                            return "неверное значение";
                        cheakList[columnName] = (this.item.MaxExperience != null) ? true : false;
                        break;
                    case "minExperience":
                        if (this.item.MinExperience != null && this.item.MinExperience < 0)
                            return "неверное значение";
                        cheakList[columnName] = (this.item.MinExperience != null) ? true : false;
                        break;
                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
