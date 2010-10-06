using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Mike.MefAreas.Core.Services;
using Mike.MefAreas.Web.IoC;

namespace Mike.MefAreas.Web
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceVirtualPathProvider());
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            InitializeWindsor();
            InitializeAddins();
        }

        protected void Application_End()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        void InitializeAddins()
        {
            using (var mefContainer = new CompositionContainer(new DirectoryCatalog(HttpRuntime.BinDirectory, "*AddIn.dll")))
            {
                var lazyInstallers = mefContainer.GetExports<IAddinInstaller>();
                foreach (var lazyInstaller in lazyInstallers)
                {
                    var installer = lazyInstaller.Value;
                    Container.Install(new CommonComponentInstaller(installer.GetType().Assembly));
                    installer.DoRegistration(Container);
                }
            }
        }

        void InitializeWindsor()
        {
            container = new WindsorContainer()
                .Install(new CoreComponentsInstaller())
                .Install(new CommonComponentInstaller(Assembly.GetExecutingAssembly()));

            var controllerFactory = Container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        static IWindsorContainer container;
        
        public IWindsorContainer Container
        {
            get { return container; }
        }
    }
}