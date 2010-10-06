using System.Web.Mvc;
using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.Web.Controllers
{
    public class NavigationController : Controller
    {
        readonly INavigation[] navigations;

        public NavigationController(INavigation[] navigations)
        {
            this.navigations = navigations;
        }

        public ViewResult Index()
        {
            return View("Index", navigations);
        }
    }
}