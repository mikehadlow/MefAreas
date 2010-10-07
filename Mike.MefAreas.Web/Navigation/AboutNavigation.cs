using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.Web.Navigation
{
    public class AboutNavigation : INavigation
    {
        public string Text
        {
            get { return "About"; }
        }

        public string Action
        {
            get { return "About"; }
        }

        public string Controller
        {
            get { return "Home"; }
        }
    }
}