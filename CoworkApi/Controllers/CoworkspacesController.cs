using System.Collections.Generic;
using System.IO;
using CoworkApi.Models;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoworkApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoworkspacesController : ControllerBase
    {
        private readonly ILogger<CoworkspacesController> _logger;

        public CoworkspacesController(ILogger<CoworkspacesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CoworkSpace> Get()
        {
            return LoadJson();
        }

        [Route("getnearestspaces")]
        [HttpGet]
        public IEnumerable<CoworkSpace> GetNearestSpaces(double lat, double lng)
        {
            return GetNearest(lat, lng);
        }

        private static List<CoworkSpace> LoadJson()
        {
            using (StreamReader r = new StreamReader("coworking-spaces.json"))
            {
                return JsonConvert.DeserializeObject<List<CoworkSpace>>(r.ReadToEnd());
            }
        }

        private static List<CoworkSpace> GetNearest(double lat, double lng)
        {
            using (StreamReader r = new StreamReader("coworking-spaces.json"))
            {
                var items = JsonConvert.DeserializeObject<List<CoworkSpace>>(r.ReadToEnd());

                var nearest = new List<CoworkSpace>();

                foreach (var item in items)
                {
                    var sCoord = new GeoCoordinate(lat, lng);
                    var eCoord = new GeoCoordinate(item.latitude, item.longitude);

                    var distance = sCoord.GetDistanceTo(eCoord);

                    if (distance <= 1000)
                    {
                        nearest.Add(item);
                    }
                }

                //return first instance if no nearest locations found
                if (nearest.Count == 0)
                {
                    return new List<CoworkSpace>() { items[0] };
                }

                return nearest;
            }
        }
    }
}
