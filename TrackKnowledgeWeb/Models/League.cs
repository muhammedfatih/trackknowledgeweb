using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackKnowledgeWeb.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Country_Order { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public Country Country { get; set; }
        public League()
        {
            CountryList = new List<SelectListItem>();
            Country = new Country();
        }
    }
}