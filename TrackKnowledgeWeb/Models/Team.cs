using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackKnowledgeWeb.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> LeagueList { get; set; }
        public League League { get; set; }
        public Team()
        {
            LeagueList = new List<SelectListItem>();
            League = new League();
        }
    }
}