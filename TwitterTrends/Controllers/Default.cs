using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core;
using DataProcessing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Syncfusion.EJ2.Maps;

namespace TwitterTrends.Controllers
{
    public class Default : Controller
    {
        IWebHostEnvironment _appEnvironment;

        public Default(IWebHostEnvironment appEnvironment)
        {
           
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        public IActionResult DrawUsMap()
        {
            List<FileModel> files = new List<FileModel>();
            int id = 0;
            foreach (var file in Directory.EnumerateFiles("wwwroot\\Files"))
            {
                FileModel model = new FileModel();
                model.Name = Path.GetFileName(file);
                model.Path = file;
                model.Id = id++;
                files.Add(model);
            }

            ViewBag.Files = new SelectList(files, "Path", "Name");

           
            ViewBag.usmap = GetUSMap();
            ViewBag.sentimentdata = GetSentimentData();
            List<MapsColorMapping> dataColor = new List<MapsColorMapping>
            {
                new MapsColorMapping {From = -10000, To = -3000, Color = "rgb(0, 96, 128)"},
                new MapsColorMapping {From = -3000, To = -2000, Color = "rgb(0, 153, 204)"},
                new MapsColorMapping {From = -2000, To = -1000, Color = "rgb(77, 210, 255)"},
                new MapsColorMapping {From = -1000, To = -0.1, Color = "rgb(179, 236, 255)"},
                new MapsColorMapping {From = 0, To = 0, Color = "rgb(255, 255, 255)"},
                new MapsColorMapping {From = 0.1, To = 500, Color = "rgb(255, 214, 204)"},
                new MapsColorMapping {From = 500, To = 1500, Color = "rgb(255, 133, 102)"},
                new MapsColorMapping {From = 1500, To = 2500, Color = "rgb(255, 51, 0)"},
                new MapsColorMapping {From = 2500, To = 5000, Color = "rgb(179, 36, 0)"}
            };
            ViewBag.colorData = dataColor;

            return View();
        }
        [HttpPost]
        public IActionResult DrawUsMap(string Path)
        {
            List<FileModel> files = new List<FileModel>();
            int id = 0;
            foreach (var file in Directory.EnumerateFiles("wwwroot\\Files"))
            {
                FileModel model = new FileModel();
                model.Path = file;
                model.Name = System.IO.Path.GetFileName(file);
                model.Id = id++;
                files.Add(model);
            }

            ViewBag.Files = new SelectList(files, "Path", "Name");
            var path = Path;
            ViewBag.usmap = GetUSMap();
            ViewBag.sentimentdata = GetSentimentData(path);
            List<MapsColorMapping> dataColor = new List<MapsColorMapping>
            {
                 new MapsColorMapping {From = -10000, To = -3000, Color = "rgb(0, 96, 128)"},
                new MapsColorMapping {From = -3000, To = -2000, Color = "rgb(0, 153, 204)"},
                new MapsColorMapping {From = -2000, To = -1000, Color = "rgb(77, 210, 255)"},
                new MapsColorMapping {From = -1000, To = -0.1, Color = "rgb(179, 236, 255)"},
                new MapsColorMapping {From = 0, To = 0, Color = "rgb(255, 255, 255)"},
                new MapsColorMapping {From = 0.1, To = 500, Color = "rgb(255, 214, 204)"},
                new MapsColorMapping {From = 500, To = 1500, Color = "rgb(255, 133, 102)"},
                new MapsColorMapping {From = 1500, To = 2500, Color = "rgb(255, 51, 0)"},
                new MapsColorMapping {From = 2500, To = 5000, Color = "rgb(179, 36, 0)"}
            };
            ViewBag.colorData = dataColor;

            return View();
        }

        public static object GetUSMap()
        {
            string allText = System.IO.File.ReadAllText("./wwwroot/us.json");
            return JsonConvert.DeserializeObject(allText);
        }

        public static object GetSentimentData()
        {
            var tweetBuilder = new TweetBuilder();
            tweetBuilder.BuildTweets(@"..\DataProcessing\DataToProcess\cali_tweets2014.txt");

            return tweetBuilder.StatesForDisplay;
        }
        public static object GetSentimentData(string path)
        {
            var tweetBuilder = new TweetBuilder();
            tweetBuilder.BuildTweets(path);

            return tweetBuilder.StatesForDisplay;
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
               
            }
            return RedirectToAction("DrawUsMap");
        }
    }
}
