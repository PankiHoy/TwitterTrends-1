using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using DataProcessing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2.Maps;

namespace TwitterTrends.Controllers
{
    public class Default : Controller
    {
        public IActionResult DrawUsMap()
        {
            //MapDataSetting MapShapeData = new MapDataSetting
            //{
            //    async = true,
            //    type = "GET",
            //    dataOptions = "./wwwroot/us.json"
            //};

            ViewBag.usmap = GetUSMap();
            //ViewBag.mapdata = MapShapeData;
            ViewBag.sentimentdata = GetSentimentData();
            List<MapsColorMapping> dataColor = new List<MapsColorMapping>
            {
                new MapsColorMapping {From = -10000, To = -1000, Color = "rgb(153,174,214)"},
                new MapsColorMapping {From = -1000, To = -500, Color = "rgb(115,143,199)"},
                new MapsColorMapping {From = -500, To = 0, Color = "161, 183, 227"},
                new MapsColorMapping {From = 0, To = 500, Color = "rgb(247, 158, 131)"},
                new MapsColorMapping {From = 500, To = 1000, Color = "rgb(232, 104, 65)"},
                new MapsColorMapping {From = 1000, To = 10000, Color = "rgb(247, 62, 5)"}
            };
            ViewBag.colorData = dataColor;

            return View();
            }

        public object GetUSMap()
        {
            string allText = System.IO.File.ReadAllText("./wwwroot/us.json");
            return JsonConvert.DeserializeObject(allText);
        }

        public object GetSentimentData()
        {
            var tweetBuilder = new TweetBuilder();
            tweetBuilder.BuildTweets(@"D:\programming\TwitterTrends\DataProcessing\DataToProcess\snow_tweets2014.txt");

            return tweetBuilder.StatesForDisplay;
        }

        public class MapDataSetting
        {
            public Boolean async { get; set; }
            public String dataOptions { get; set; }
            public String type { get; set; }
        }

    }
}
