using System;
using System.IO;
using System.Web.Mvc;

namespace Mike.MefAreas.Core.Controllers
{
    public class AddinControllerBase : Controller
    {
        protected override ViewResult View(string viewName, string masterName, object model)
        {
            if (viewName == null)
            {
                throw new ArgumentNullException("viewName", "You must provide a view name in AddinControllers");
            }

            var assembly = this.GetType().Assembly;
            var assemblyFileName = Path.GetFileName(assembly.Location);
            var assmblyName = assembly.GetName().Name;
            var controllerName = this.GetType().Name;
            if (!controllerName.EndsWith("Controller"))
            {
                throw new ApplicationException(
                    "Controllers must have a name ending with Controller, e.g: CustomerController");
            }
            var controllerShortenedName = controllerName.Substring(0, controllerName.Length - 10);

            // ~/App_Resource/Mike.MefAreas.AddIn.dll/Mike.MefAreas.AddIn/Views/Customer/
            var viewPath = string.Format("~/App_Resource/{0}/{1}/Views/{2}/", assemblyFileName, assmblyName, controllerShortenedName);

            return base.View(viewPath + viewName + ".aspx", masterName, model);
        }
    }
}