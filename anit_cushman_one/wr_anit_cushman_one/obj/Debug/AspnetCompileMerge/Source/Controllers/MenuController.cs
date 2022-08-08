using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;
using System.Xml;
using System.Xml.Linq;
namespace wr_anit_cushman_one.Controllers
{
    public class MenuController : Controller
    {
        private CushmanContext db = new CushmanContext();
        // GET: Menu
        public ActionResult Index()
        {
            ws_banxico.DgieWSPortClient banxico = new ws_banxico.DgieWSPortClient();
            string respuesta_ws = banxico.tiposDeCambioBanxico();
            XmlDocument xdoc = new XmlDocument();
            XNamespace bm = "http://www.banxico.org.mx/structure/key_families/dgie/sie/series/compact";
            xdoc.LoadXml(respuesta_ws);
            //XmlNodeList xnList = xdoc.SelectNodes("CompactData/"+bm+"DataSet/"+bm+"Series");
            XmlNodeList info = xdoc.GetElementsByTagName("CompactData");
            XmlNodeList lista =
                  ((XmlElement)info[0]).GetElementsByTagName("bm:DataSet");

            XmlNodeList lista2 =
            ((XmlElement)info[0]).GetElementsByTagName("bm:Series");
            string valor = null;
            foreach (XmlElement nodo in lista2)
            {

                XmlNodeList lista5 = nodo.GetElementsByTagName("bm:Obs");
                ///lista5[0].Attributes.Item
                valor = lista5[0].Attributes.GetNamedItem("OBS_VALUE").InnerText;
                break;
            }
            ViewBag.TipoCambio = valor;

            var tsg033_menu = db.tsg033_menu;
            return View(tsg033_menu.ToList());
        }
    }
}