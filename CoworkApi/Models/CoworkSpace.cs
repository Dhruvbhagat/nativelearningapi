using System;
namespace CoworkApi.Models
{
    public class Geopoint
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class CoworkSpace
    {
        public string organisation { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Geopoint geopoint { get; set; }
    }
}