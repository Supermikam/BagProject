using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagProject.Models;

namespace BagProject.Component
{
    public class MenuItem: ViewComponent
    {
        private ICategoryRepository _categoryRepo;
        public MenuItem(ICategoryRepository repo)
        {
            _categoryRepo = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_categoryRepo.Categories);           
        }
    }
}
