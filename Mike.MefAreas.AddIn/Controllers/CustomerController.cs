using System.Collections.Generic;
using System.Web.Mvc;
using Mike.MefAreas.AddIn.Views.Customer;
using Mike.MefAreas.Core.Controllers;

namespace Mike.MefAreas.AddIn.Controllers
{
    public class CustomerController : AddinControllerBase
    {
        public ViewResult Index()
        {
            var customers = new List<CustomerView>
            {
                new CustomerView { Name = "Mike Hadlow", Age = 45 },
                new CustomerView { Name = "Etsuko Hara", Age = 35 },
                new CustomerView { Name = "Leo Hadlow", Age = 8 },
                new CustomerView { Name = "Yuna Hadlow", Age = 2 },
            };

            return View("Index", customers);
        }
    }
}