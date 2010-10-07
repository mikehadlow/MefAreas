using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace Mike.MefAreas.Web.IoC
{
    public class IocControllerFactory : IControllerFactory
    {
        readonly IKernel kernel;

        public IocControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }
            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            try
            {
                return kernel.Resolve<IController>(controllerName + "controller");
            }
            catch (ComponentNotFoundException e)
            {
                throw new ApplicationException(string.Format("No controller with name '{0}' found", controllerName), e);
            }
        }

        public void ReleaseController(IController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            kernel.ReleaseComponent(controller);
        }
    }
}