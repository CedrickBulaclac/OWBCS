using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWBCS.Controllers
{
    public class SavingsController : Controller
    {
        public ActionResult Savings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Savings(Contribution contribution,FormCollection fc)
        {
            bool status = false;
            List<Contribution> contr = new List<Contribution>();
            int id=contr.Count+1;
            int mid = Convert.ToInt32(fc["MemberId"]);
            string sid = "C" + id.ToString();
            Contribution model = new Contribution
            {
                ContributionId = sid,
                DateContributed = DateTime.Now,
                ContributionAmt = contribution.ContributionAmt,
                MemberId=mid
            };
            status = ContributionCotrollerSql.Insert(model);
            if(status==true)
            {
                Response.Write("<script>alert('You have successfully add the savings');</script>");
            }
            return View();
        }
    }
}