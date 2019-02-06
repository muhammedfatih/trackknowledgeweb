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
    public class CountryController : Controller
    {
        public ActionResult Index()
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/countries");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            List<Country> Model = JsonConvert.DeserializeObject<List<Country>>(response.Content);
            return View(Model);
        }
        public ActionResult Details(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/countries/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Country Model = JsonConvert.DeserializeObject<Country>(response.Content);
            return View(Model);
        }
        public ActionResult Edit(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/countries/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Country Model = JsonConvert.DeserializeObject<Country>(response.Content);

            Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            uri = string.Format("/continents");
            request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            response = Client.Execute(request);
            Model.ContinentList = JsonConvert.DeserializeObject<IEnumerable<Continent>>(response.Content).Select(x=> new SelectListItem() {Text=x.Name, Value=x.Id.ToString() });

            return View(Model);
        }
        [HttpPost]
        public ActionResult Edit(Country country)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/countries/{0}", country.Id);
            var request = new RestRequest(uri, Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", country.Name);
            request.AddParameter("continent_id", country.Continent.Id);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Country");
        }
        public ActionResult Create()
        {
            Country Model = new Country();
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/continents");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            var response = Client.Execute(request);
            Model.ContinentList = JsonConvert.DeserializeObject<IEnumerable<Continent>>(response.Content).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });

            return View(Model);
        }
        [HttpPost]
        public ActionResult Create(Country country)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/countries");
            var request = new RestRequest(uri, Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", country.Name);
            request.AddParameter("continent_id", country.Continent.Id);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Country");
        }
        public ActionResult Delete(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/countries/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Country Model = JsonConvert.DeserializeObject<Country>(response.Content);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Delete(Country country)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/countries/{0}", country.Id);
            var request = new RestRequest(uri, Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Country");
        }
    }
}