using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsurancesGAP.Data;
using InsurancesGAP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InsurancesGAP.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new PolicyViewModel();
            model.RiskTypes = _context.RiskTypes.Get().ToList();
            model.CoverageTypes = _context.CoverageTypes.Get().ToList();
            model.Customers = _context.Customers.Get().ToList();
            return View(model);
        }
    }
}
