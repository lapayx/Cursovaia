
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cursovaia.ViewModel;
using System.Windows.Documents;
using System.Windows.Controls;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Ninject.Modules;
using Ninject;
using Cursovaia.Pages;
namespace Cursovaia
{
    /// <summary>
    /// Конфигурация Dependency Injection.
    /// </summary>
    public static  class DIConfig
    {
        static DIConfig() 
        {
            DIConfig.Register();
        }
         public static IKernel  kernel;
        /// <summary>
        /// Регистрация зависимостей.
        /// </summary>
        public static void Register()
        {
            kernel = new StandardKernel(new InjectModule());
            //container.RegisterControllers(Assembly.GetExecutingAssembly());
        }
       
    }
    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INotifyPropertyChanged>().To<vievmodel1>().Named("page1");
           // this.Bind<INotifyPropertyChanged>().To<ViewModel2>().Named("page2");
            this.Bind<INotifyPropertyChanged>().To<ApplicantViewModel>().Named("applicant");

            this.Bind<Page>().To<Page1>().Named("page1");
            this.Bind<Page>().To<ApplicantPage>().Named("applicant");


            this.Bind<DbContext>().To<WIN_Server>().InSingletonScope();
            this.Bind<IGenericRepository<Applicant>>().To<Entities<Applicant>>();
            //container.Register<DbContext, WIN_Server>(new PerScopeLifetime());
        }
    }
}
