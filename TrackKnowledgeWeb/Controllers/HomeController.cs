using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrackKnowledgeWeb.Helpers;

namespace TrackKnowledgeWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection formCollection)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_USER"]);
            var uri = string.Format("/users");
            var request = new RestRequest(uri, Method.POST);
            request.AddParameter("firstname", formCollection["txtSignUpFirstName"]);
            request.AddParameter("lastname", formCollection["txtSignUpLastName"]);
            request.AddParameter("email", formCollection["txtSignUpEmail"]);
            request.AddParameter("password", formCollection["txtSignUpPassword"]);
            IRestResponse response = Client.Execute(request);
            if(response.StatusCode == HttpStatusCode.Created)
            {
                return RedirectToAction("ActivationRequired", "Home");
            }
            else
            {
                TempData["Error"] = "Something went wrong.";
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ActivationRequired()
        {
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Error = TempData["Error"]!=null? TempData["Error"] : "Something went wrong." ;
            return View();
        }

        public ActionResult Activation(string id)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_USER"]);
            var uri = string.Format("/users/{0}/activate", id);
            var request = new RestRequest(uri, Method.GET);
            IRestResponse response = Client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return View();
            }
            else
            {
                TempData["Error"] = "Something went wrong.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult SignIn(FormCollection formCollection)
        {
            var Client = new RestClient(ConfigurationManager.AppSettings["SERVICE_ADDRESS_USER"]);
            var uri = string.Format("/users/authenticate");
            var request = new RestRequest(uri, Method.POST);
            request.AddParameter("email", formCollection["txtSignInEmail"]);
            request.AddParameter("password", formCollection["txtSignInPassword"]);
            IRestResponse response = Client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic responseObject = JsonConvert.DeserializeObject(response.Content);
                string api_key = responseObject.api_key;
                SessionManager.Login(api_key);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData["Error"] = "Something went wrong.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}