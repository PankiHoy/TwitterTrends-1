using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TwitterTrends.Controllers
{
    public class Default : Controller
    {
        public IActionResult DrawUsMap()
        {
            ViewBag.mapdata = GetUSMap();
            return View();
        }


        public object GetUSMap()
        {
            string allText = System.IO.File.ReadAllText("./wwwroot/us.json");
            return JsonConvert.DeserializeObject(allText);
        }
    }
}
