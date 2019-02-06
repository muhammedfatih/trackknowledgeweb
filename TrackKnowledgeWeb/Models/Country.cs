using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackKnowledgeWeb.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> ContinentList { get; set; }
        public Continent Continent { get; set; }
        public Country()
        {
            ContinentList = new List<SelectListItem>();
            Continent = new Continent();
        }
    }
}