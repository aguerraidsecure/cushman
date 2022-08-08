using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;

namespace wr_anit_cushman_one.Controllers
{
    public partial class AJAXController : Controller
    {
        public virtual int AJAXRequest(Terreno model)
        {
            return (model.tsg001_terreno.cd_terreno);
        }

        public virtual ActionResult Test()
        {
            var m = new Terreno();
            return View(m);
        }
    }
}
