using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace iDisciple.Controllers
{
    public class ModelFolk
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthLocation { get; set; }
        public string Bio { get; set; }
        public string Gig { get; set; }
    }
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public JsonResult GetList()
        {

            var cstring = ConfigurationManager.ConnectionStrings["iDisciple"].ConnectionString;
            var conn = new SqlConnection(cstring);

            try
            {
                conn.Open();

            }
            catch (Exception ex)
            {
                var wut = ex.Message;
            }

            string onlyPols = "";
            if (Request.Params["onlyPols"] == "1")
            {
                onlyPols = " where b.gig='Politician' ";
            }

            var statement = String.Format("select firstname,lastname,birthlocation,bio,isnull(b.gig,'') gig from folks a left join folksgig b on b.gigid = a.gigid{0} order by a.lastname",onlyPols);

            var command = new SqlCommand(statement,conn);
            
            var reader = command.ExecuteReader();

            var retVal = new List<ModelFolk>();

            while (reader.Read())
            {

                var folk = new ModelFolk();

                folk.FirstName = (string)reader["firstname"];
                folk.LastName = (string)reader["lastname"];
                folk.BirthLocation = (string)reader["birthlocation"];
                folk.Bio = (string)reader["bio"];
                folk.Gig = (string)reader["gig"];

                retVal.Add(folk);

            }

            return Json(retVal, JsonRequestBehavior.AllowGet);

        }
    }
}