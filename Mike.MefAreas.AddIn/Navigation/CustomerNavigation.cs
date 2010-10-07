using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.AddIn.Install
{
    public class CustomerNavigation : INavigation
    {
        public string Text
        {
            get { return "Customers"; }
        }

        public string Action
        {
            get { return "Index"; }
        }

        public string Controller
        {
            get { return "Customer"; }
        }
    }
}