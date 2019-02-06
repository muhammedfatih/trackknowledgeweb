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
    public class NationalityController : Controller
    {
        public ActionResult Index()
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities");
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            List<Nationality> Model = JsonConvert.DeserializeObject<List<Nationality>>(response.Content);
            return View(Model);
        }
        public ActionResult Details(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Nationality Model = JsonConvert.DeserializeObject<Nationality>(response.Content);
            return View(Model);
        }
        public ActionResult Edit(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Nationality Model = JsonConvert.DeserializeObject<Nationality>(response.Content);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Edit(Continent continent)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities/{0}", continent.Id);
            var request = new RestRequest(uri, Method.PUT);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", continent.Name);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Nationality");
        }
        public ActionResult Create()
        {
            Nationality Model = new Nationality();
            return View(Model);
        }
        [HttpPost]
        public ActionResult Create(Continent continent)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities");
            var request = new RestRequest(uri, Method.POST);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            request.AddParameter("name", continent.Name);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Nationality");
        }
        public ActionResult Delete(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities/{0}", id);
            var request = new RestRequest(uri, Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            Nationality Model = JsonConvert.DeserializeObject<Nationality>(response.Content);
            return View(Model);
        }
        [HttpPost]
        public ActionResult Delete(Continent continent)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_CONTENT"]);
            var uri = string.Format("/nationalities/{0}", continent.Id);
            var request = new RestRequest(uri, Method.DELETE);
            request.AddParameter("Authorization", string.Format("Bearer " + SessionManager.GetSession("api_key")), ParameterType.HttpHeader);
            IRestResponse response = Client.Execute(request);
            return RedirectToAction("Index", "Nationality");
        }
    }
}