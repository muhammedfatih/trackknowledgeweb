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
    public class PlayerController : Controller
    {
        public ActionResult Index()
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_PLAYER"]);
            var uri = string.Format("/players");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            List<Player> Model = JsonConvert.DeserializeObject<List<Player>>(response.Content);
            return View(Model);
        }
        public ActionResult Details(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_PLAYER"]);
            var uri = string.Format("/players/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Player Model = JsonConvert.DeserializeObject<Player>(response.Content);
            return View(Model);
        }
        public ActionResult Edit(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_PLAYER"]);
            var uri = string.Format("/players/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Player Model = JsonConvert.DeserializeObject<Player>(response.Content);

            Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            uri = string.Format("/nationalities");
            request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            response = Client.Execute(request);
            Model.NationalityList = JsonConvert.DeserializeObject<IEnumerable<Nationality>>(response.Content).Select(x=> new SelectListItem() {Text=x.Name, Value=x.Id.ToString() });

            return View(Model);
        }
        [HttpPost]
        public ActionResult Edit(Player player)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_PLAYER"]);
            var uri = string.Format("/players/{0}", player.Id);
            var request = new RestRequest(uri, Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("firstname", player.Firstname);
            request.AddParameter("lastname", player.Lastname);
            request.AddParameter("birthday", player.Birthday.ToString("yyyy-MM-dd"));
            request.AddParameter("nationality_id", player.Nationality.Id);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Player");
        }
        public ActionResult Create()
        {
            Player Model = new Player();
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            var response = Client.Execute(request);
            Model.NationalityList = JsonConvert.DeserializeObject<IEnumerable<Nationality>>(response.Content).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });

            return View(Model);
        }
        [HttpPost]
        public ActionResult Create(Player player)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_PLAYER"]);
            var uri = string.Format("/players");
            var request = new RestRequest(uri, Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("firstname", player.Firstname);
            request.AddParameter("lastname", player.Lastname);
            request.AddParameter("birthday", player.Birthday.ToString("yyyy-MM-dd"));
            request.AddParameter("nationality_id", player.Nationality.Id);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Player");
        }
        public ActionResult Delete(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_PLAYER"]);
            var uri = string.Format("/players/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Player Model = JsonConvert.DeserializeObject<Player>(response.Content);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Delete(Player player)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_PLAYER"]);
            var uri = string.Format("/players/{0}", player.Id);
            var request = new RestRequest(uri, Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Player");
        }
    }
}