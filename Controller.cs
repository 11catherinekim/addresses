using Datalus.Web.Models.ViewModels;
using Datalus.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Datalus.Web.Controllers
{
    [RoutePrefix("Addresses")]
    public class AddressesController : BaseController
    {
        [Route("Create")]
        [Route("{id:int}/edit")]
        public ActionResult Create(int id = 0)
        {
            AddressesViewModel model = new AddressesViewModel();
            model = DecorateViewModel(model);
            model.Id = id;
            model.Countries = AddressesService.CountriesGetAll();
            return View(model);
        }

        [Route, HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
