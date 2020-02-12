using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using PetGrooming.Models.ViewModels;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();

        // GET: Species
        public ActionResult Index() //??????????????
        {
            return View();
        }

        // List
        public ActionResult List()
        {
            //same with: List<Species> myspecies = db.Species.SqlQuery("SELECT * FROM Species").ToList();
            var species = db.Species.SqlQuery("Select * from Species").ToList();
            return View(species);
        }

        // Add
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            //create query:
            string query = "insert into species (Name) values (@SpeciesName)";
            //each piece of information is a key and value pair
            SqlParameter sqlparams = new SqlParameter("@SpeciesName", SpeciesName);

            //db.Pets.SqlCommand will run a select statement, for example.
            //run query
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //run the list method to return to a list of pets 
            return RedirectToAction("List"); //or "Species/List"
        }
        public ActionResult Add()  //same with file name  .cshtml
        {
            List<Species> species = db.Species.SqlQuery("select * from Species").ToList();

            return View(species);
        }

        public ActionResult Show(int id)
        {
            string query = "SELECT * FROM species WHERE speciesid = @id";

            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault(); //only take the first item of the result

            return View(selectedspecies);
        }

        // Update
        [HttpPost]
        public ActionResult Edit(int id, string SpeciesName)
        {
            Debug.WriteLine("Trying to edit the species " + id);

            string query = "UPDATE species SET Name=@SpeciesName WHERE SpeciesID = @id";
            SqlParameter[] sqlparams = new SqlParameter[2]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName); //add all parameters here
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            string query = "SELECT * FROM species WHERE SpeciesID = @id";

            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault(); //only take the first item of the result(defult is result set), the system doesn't know there only be one result will show

            return View(selectedspecies);
        }

        // delete
        [HttpPost] //the method to grab the data from the form
        public ActionResult Remove(int id, string deletesubmit) //must same with the Show id!!!
        {
            Debug.WriteLine("Trying to edit the species " + id);

            string query = "DELETE FROM species WHERE speciesid = @id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@deletesubmit", deletesubmit);
            sqlparams[1] = new SqlParameter("@id", id);

            if (deletesubmit == "Yes")
            {
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }
            return RedirectToAction("List");
        }
        public ActionResult Remove(int id)
        {
            string query = "SELECT * FROM species WHERE SpeciesID = @id";

            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault(); //o

            return View(selectedspecies);
        }

    }


}