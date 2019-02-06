using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackKnowledgeWeb.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public IEnumerable<SelectListItem> NationalityList { get; set; }
        public Nationality Nationality { get; set; }
        public Player()
        {
            NationalityList = new List<SelectListItem>();
            Nationality = new Nationality();
        }
    }
}