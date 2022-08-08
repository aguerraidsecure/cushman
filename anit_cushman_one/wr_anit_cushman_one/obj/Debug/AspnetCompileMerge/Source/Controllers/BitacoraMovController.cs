using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;
using Model;
using System.Dynamic;
using PagedList;
namespace wr_anit_cushman_one.Controllers
{
    public class BitacoraMovController : Controller
    {
        private wr_anit_cushman_one.Models.CushmanContext db = new wr_anit_cushman_one.Models.CushmanContext();
        private Model.CushmanContext db2 = new Model.CushmanContext();

        // GET: BitacoraMov
        public ActionResult Index()
        {

            //var idUduarios = (from u in db2.Usuario
            //    select new {u.id, u.Nombre}).ToArray();

            //var query = (from tsg029 in db.tsg029_bitacora_mov
            //             select new
            //             {
            //                 usuario = tsg029.cd_usuario,
            //                 //nombre = usu.Nombre,
            //                 tabla = tsg029.nb_tabla,
            //                 id = tsg029.nb_id_modif,
            //                 fecha = tsg029.fh_modif_reg
            //             }).ToList();

            //var query = (from tsg029 in db.tsg029_bitacora_mov
            //    join usu in db2.Usuario
            //        on tsg029.cd_usuario equals usu.id
            //    select new
            //    {
            //        usuario = tsg029.cd_usuario,
            //        usu.Nombre,
            //        tabla = tsg029.nb_tabla,
            //        id = tsg029.nb_id_modif,
            //        fecha = tsg029.fh_modif_reg
            //    }).AsEnumerable().Select(c => c.ToExpando());

            var bitacora = new List<tsg029_bitacora_mov>(db.tsg029_bitacora_mov);

            var usuarios = new List<Usuario>(db2.Usuario);

            var results =
                            (from c in bitacora
                            join d in usuarios on c.cd_usuario equals d.id
                            select new
                            { usuario = c.cd_usuario,
                              nombre =  d.Nombre,
                                tabla = c.nb_tabla,
                                id = c.nb_id_modif,
                                fecha = c.fh_modif_reg
                            }).AsEnumerable().Select(c => c.ToExpando()); 

            return View(results);
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

public static class impFunctions
{
    public static ExpandoObject ToExpando(this object anonymousObject)
    {
        IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
        IDictionary<string, object> expando = new ExpandoObject();
        foreach (var item in anonymousDictionary)
            expando.Add(item);
        return (ExpandoObject)expando;
    }
}