using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWBCS.Controllers
{
    public class SavingsController : Controller
    {
        public ActionResult ViewSavings()
        {
            return View();
        }
        
        public ActionResult Savings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Savings(Savings contribution,FormCollection fc)
        {
            bool status = false;
            List<Savings> contr = new List<Savings>();
            contr = SavingsCotrollerSql.GetAll();
            int id=contr.Count+1;
            int mid = Convert.ToInt32(fc["MemberId"]);
            string name = Convert.ToString(Session["Fullname"]);
            string sid = "C" + id.ToString();
            Savings model = new Savings
            {
                SavingsId = sid,
                SavingsDate = DateTime.Now,
                SavingsAmt = contribution.SavingsAmt,
                MemberId=mid,
                Deleted=0,
                CreatedBy=name
            };
            status = SavingsCotrollerSql.Insert(model);
            if(status==true)
            {
                Response.Write("<script>alert('You have successfully add the savings');</script>");
            }
            return View();
        }
        public ActionResult GetSavings()
        {
            int id = Convert.ToInt32(Session["MemberId"]);
            List<Savings> ret = new List<Savings>();
            ret = SavingsCotrollerSql.GetAll(id);
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SavingsAdmin(Savings data)
        {          
            List<Savings> ret = new List<Savings>();
            ret = SavingsCotrollerSql.GetAll(data.MemberId);
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(Savings data)
        {
            bool status = false;
            status = SavingsCotrollerSql.UpdateStatus(data.Id);
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}