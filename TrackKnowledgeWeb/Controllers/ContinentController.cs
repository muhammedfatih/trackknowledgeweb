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
    public class ContinentController : Controller
    {
        public ActionResult Index()
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            List<Continent> Model = JsonConvert.DeserializeObject<List<Continent>>(response.Content);
            return View(Model);
        }
        public ActionResult Details(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Continent Model = JsonConvert.DeserializeObject<Continent>(response.Content);
            return View(Model);
        }
        public ActionResult Edit(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Continent Model = JsonConvert.DeserializeObject<Continent>(response.Content);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Edit(Continent continent)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents/{0}", continent.Id);
            var request = new RestRequest(uri, Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", continent.Name);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Continent");
        }
        public ActionResult Create()
        {
            Continent Model = new Continent();
            return View(Model);
        }
        [HttpPost]
        public ActionResult Create(Continent continent)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents");
            var request = new RestRequest(uri, Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", continent.Name);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Continent");
        }
        public ActionResult Delete(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Continent Model = JsonConvert.DeserializeObject<Continent>(response.Content);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Delete(Continent continent)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents/{0}", continent.Id);
            var request = new RestRequest(uri, Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Continent");
        }
    }
}