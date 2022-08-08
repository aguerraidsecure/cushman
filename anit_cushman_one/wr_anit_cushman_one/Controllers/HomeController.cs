using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helper;
using wr_anit_cushman_one.Tags;
using System.Xml;
using System.Xml.Linq;

namespace wr_anit_cushman_one.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string valor = null;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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
                
                foreach (XmlElement nodo in lista2)
                {

                    XmlNodeList lista5 = nodo.GetElementsByTagName("bm:Obs");
                    ///lista5[0].Attributes.Item
                    valor = lista5[0].Attributes.GetNamedItem("OBS_VALUE").InnerText;
                    break;
                }
            }
            catch (Exception e)
            { 
            
            }
            ViewBag.TipoCambio = valor;
            //var lv1s = xdoc.Root.Descendants("bm:DataSet");
            ViewBag.UsuarioActivo = true;
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
        public ActionResult Salir()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }
    }
}