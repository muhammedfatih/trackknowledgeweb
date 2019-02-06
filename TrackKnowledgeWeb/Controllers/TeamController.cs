using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackKnowledgeWeb.Helpers;
using TrackKnowledgeWeb.Models;

namespace TrackKnowledgeWeb.Controllers
{
    [AFAuthorization]
    public class TeamController : Controller
    {
        public ActionResult Index()
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_TEAM"]);
            var uri = string.Format("/teams");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            List<Team> Model = JsonConvert.DeserializeObject<List<Team>>(response.Content);
            return View(Model);
        }
        public ActionResult Details(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_TEAM"]);
            var uri = string.Format("/teams/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Team Model = JsonConvert.DeserializeObject<Team>(response.Content);
            return View(Model);
        }
        public ActionResult Edit(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_TEAM"]);
            var uri = string.Format("/teams/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Team Model = JsonConvert.DeserializeObject<Team>(response.Content);

            Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            uri = string.Format("/leagues");
            request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            response = Client.Execute(request);
            Model.LeagueList = JsonConvert.DeserializeObject<IEnumerable<League>>(response.Content).Select(x=> new SelectListItem() {Text=x.Name, Value=x.Id.ToString() });

            return View(Model);
        }
        [HttpPost]
        public ActionResult Edit(Team team)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_TEAM"]);
            var uri = string.Format("/teams/{0}", team.Id);
            var request = new RestRequest(uri, Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", team.Name);
            request.AddParameter("league_id", team.League.Id);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Team");
        }
        public ActionResult Create()
        {
            Team Model = new Team();
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/leagues");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            var response = Client.Execute(request);
            Model.LeagueList = JsonConvert.DeserializeObject<IEnumerable<Continent>>(response.Content).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });

            return View(Model);
        }
        [HttpPost]
        public ActionResult Create(Team team)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_TEAM"]);
            var uri = string.Format("/teams");
            var request = new RestRequest(uri, Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", team.Name);
            request.AddParameter("LeagueId", team.League.Id);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Team");
        }
        public ActionResult Delete(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_TEAM"]);
            var uri = string.Format("/teams/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Team Model = JsonConvert.DeserializeObject<Team>(response.Content);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Delete(Team team)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_TEAM"]);
            var uri = string.Format("/teams/{0}", team.Id);
            var request = new RestRequest(uri, Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Team");
        }
    }
}