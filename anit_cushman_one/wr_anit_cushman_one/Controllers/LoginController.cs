﻿using Helper;
using wr_anit_cushman_one.Tags;
using wr_anit_cushman_one.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wr_anit_cushman_one.Controllers
{
    [NoLogin]
    public class LoginController : Controller
    {
        private Usuario um = new Usuario();

        public ActionResult Index()
        {

            LoginViewModel login = new LoginViewModel(); 

            return View(login);
        }

        public JsonResult Autenticar(LoginViewModel model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                this.um.Correo = model.Correo;
                this.um.Password = model.Password;

                rm = um.Autenticarse();

                if (rm.response)
                {
                    rm.href = Url.Content("~/home");
                }
            }
            else
            {
                rm.SetResponse(false, "Debe llenar los campos para poder autenticarse.");
            }

            return Json(rm);
        }
    }
}