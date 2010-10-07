using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.Web.Navigation
{
    public class HomeNavigation : INavigation
    {
        public string Text
        {
            get { return "Home"; }
        }

        public string Action
        {
            get { return "Index"; }
        }

        public string Controller
        {
            get { return "Home"; }
        }
    }
}