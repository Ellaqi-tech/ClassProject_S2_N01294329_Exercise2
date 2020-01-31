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
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            //what data do we need?
            //List<Species> myspecies = db.Species.SqlQuery("SELECT * FROM Species").ToList();
            var species = db.Species.SqlQuery("Select * from Species").ToList();
            return View(species);
        }

        // Add
        [HttpPost]
        public ActionResult Add(string Name)
        {
            //STEP 1: PULL DATA! The data is access as arguments to the method. Make sure the datatype is correct!
            //The variable name  MUST match the name attribute described in Views/Pet/Add.cshtml

            //Tests are very useul to determining if you are pulling data correctly!
            //Debug.WriteLine("Want to create a pet with name " + PetName + " and weight " + PetWeight.ToString()) ;

            //STEP 2: CREATE QUERY! the query will look something like "insert into () values ()"...
            string query = "insert into species (Name) values (@SpeciesName)";
     
            //each piece of information is a key and value pair
            SqlParameter sqlparams = new SqlParameter("@SpeciesName", Name);

            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.Pets.SqlCommand will run a select statement, for example.
            //STEP 3: RUN QUERY!
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //run the list method to return to a list of pets so we can see our new one!
            return RedirectToAction("List"); //or "Species/List"
        }


        public ActionResult Add()  //must same with file name Add.cshtml
        {
            //STEP 1: PUSH DATA!
            //What data does the Add.cshtml page need to display the interface?
            //A list of species to choose for a pet

            //alternative way of writing SQL -- will learn more about this week 4
            //List<Species> Species = db.Species.ToList();

            List<Species> species = db.Species.SqlQuery("select * from Species").ToList();

            return View(species);
        }

        // (optional) delete
        // [HttpPost] Delete

        public ActionResult Delete(int id)
        {
            string query = "DELETE FROM species WHERE speciesid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            string query = "SELECT * FROM species WHERE speciesid = @id";

            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault(); //only take the first item of the result

            return View(selectedspecies);
        }

        // Update
        // [HttpPost] Update
        public ActionResult Edit(int id)
        {
            string query = "SELECT * FROM species WHERE speciesid = @id";

            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault(); //only take the first item of the result(defult is result set), the system doesn't know there only be one result will show

            return View(selectedspecies);
        }

        [HttpPost]
        public ActionResult Edit(int id, string Name)
        {
            string query = "UPDATE species SET Name=@Name WHERE species = @id";
            SqlParameter[] sqlparams = new SqlParameter[2]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@Name", Name);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
    }
}