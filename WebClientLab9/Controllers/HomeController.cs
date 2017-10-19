using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;

namespace WebClientLab9.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        WebClient wc = new WebClient();

        private const string url = "http://localhost:54431/Service1.svc/";

        IEnumerable<Models.Account> Accounts;
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject
                        <IEnumerable<Models.DVD>>
                        (wc.DownloadString(url + "GetDVDs"));
            this.Accounts = JsonConvert.DeserializeObject
                        <IEnumerable<Models.Account>>
                        (wc.DownloadString(url + "GetAccounts"));
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.DVD newDVD)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Models.DVD));
                dcjs.WriteObject(ms, newDVD);
                wc.Headers["content-type"] = "application/json";
                wc.UploadData(url + "PostDVD", "POST", ms.ToArray());

                ModelState.AddModelError("", "Add ok");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "add error occured");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Search(string txtFrom, string txtTo)
        {

            if (string.IsNullOrEmpty(txtFrom) || string.IsNullOrEmpty(txtTo))
            {
                return View("Index", JsonConvert.DeserializeObject
                        <IEnumerable<Models.DVD>>
                        (wc.DownloadString(url + "GetDVDs")));
            }
            else
            {
                return View("Index", JsonConvert.DeserializeObject
                        <IEnumerable<Models.DVD>>
                        (wc.DownloadString(url + "GetByRange/" + txtFrom + "/" + txtTo)));
            }
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Models.Account newAccount)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Models.Account));
                dcjs.WriteObject(ms, newAccount);
                wc.Headers["content-type"] = "application/json";
                wc.UploadData(url + "PostAccount", "POST", ms.ToArray());

                ModelState.AddModelError("", "Add account ok");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "add account error occured");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            this.Accounts = JsonConvert.DeserializeObject
                        <IEnumerable<Models.Account>>
                        (wc.DownloadString(url + "GetAccounts"));
            var check = Accounts.SingleOrDefault(a => a.Username.Equals(Username) && a.Password.Equals(Password));
            if (check != null)
                return RedirectToAction("Index", "Home");
            else
            {
                ModelState.AddModelError("", "Wrong username or password");
                return View();

            }
        }


    }
}