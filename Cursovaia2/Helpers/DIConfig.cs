using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cursovaia.ViewModel;
using System.Windows.Documents;
using System.Windows.Controls;
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
        public static IServiceContainer container;
        /// <summary>
        /// Регистрация зависимостей.
        /// </summary>
        public static void Register()
        {
            container = new ServiceContainer();
            //container.RegisterControllers(Assembly.GetExecutingAssembly());

            Register(container);
            RegisterRepositores(container);
            RegisterProviders(container);
            RegisterAlias(container);
            RegisterPages(container);
            //LightInjectHttpModule.SetServiceContainer(container);
            //DependencyResolver.SetResolver(new LightInjectMvcDependencyResolver(container));
            // container.EnableMvc()  ;
        }

        internal static void RegisterRepositores(IServiceContainer container)
        {/*
            container.Register<Entities>(new PerScopeLifetime());

            container.Register<IRoleRepository, Entities>();
            container.Register<IAccountRepository, Entities>();
            container.Register<IMedalRepository, Entities>();
            container.Register<IVersionControlSystemRepository, Entities>();
            container.Register<IImageRepository, Entities>();*/
        }


        internal static void RegisterProviders(IServiceContainer container)
        {
            // container.Register<RoleProvider, Roles.Provider>();
            /* container.Register<RoleProvider>(factory => Roles.Provider);
             container.Register<MembershipProvider>(factory => Membership.Providers["SampleMembership"]);

             ((SampleMembershipProvider)Membership.Providers["SampleMembership"]).MembershipFactory =
                 new Factory<IAccountRepository>(() => container.GetInstance<IAccountRepository>());

             ((SampleMembershipProvider)Membership.Providers["SampleMembership"]).RoleFactory =
                 new Factory<IRoleRepository>(() => container.GetInstance<IRoleRepository>());

             ((SampleRoleProvider)Roles.Provider).RolesFactory =
                 new Factory<IRoleRepository>(() => container.GetInstance<IRoleRepository>());

             ((SampleRoleProvider)Roles.Provider).MembershipFactory =
                 new Factory<IAccountRepository>(() => container.GetInstance<IAccountRepository>());*/
        }

        internal static void Register(IServiceContainer container)
        {
            /* container.Register<IImportService, ImportService>();
             container.Register<ISearchService, SearchSevice>();
             container.Register<IVcsService, VcsService>();
             container.Register<IMedalService, MedalService>();
             container.Register<IAdminService, AdminService>();
             container.Register<IAccountService, AccountService>();
             container.Register<IImageService, ImageService>();*/
        }
        internal static void RegisterAlias(IServiceContainer container)
        {
            container.Register<INotifyPropertyChanged, vievmodel1>("page1");
            container.Register<INotifyPropertyChanged, ViewModel2>("page2");

        }
        internal static void RegisterPages(IServiceContainer container)
        {
            container.Register<Page, Page1>("page1");
            container.Register<Page, Page2>("page2");

        }
    }
}
