using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingHub.Models;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace WeddingHub.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            //Users user = new Users();
           // user.
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IsEmailVerified,ActivationCode")]User user)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                #region //Email is Already Exist
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);

                }
                #endregion

                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                user.IsEmailVerified = false;

                #region Save to Database
                using (Database1Entities1 dc = new Database1Entities1())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();
                    #endregion
                    //send Email To User
                    SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString() );
                    
                    ModelState.AddModelError("", "Registration successfully done Account activation link" +
                        "has been sent to your email ID   " + user.EmailID);
                    //message = ";
                    Status = true;
                }
            }
            else
            {
//message = "Invalid Request";
                ModelState.AddModelError("", "Invalid Request");

            }
            ViewBag.Message = message;
            ViewBag.Status= Status;
            return View(user);
        } 
        //verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (Database1Entities1 dc = new Database1Entities1())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;

                }
                else
                {
                    ModelState.AddModelError("", "Invalid Request");
                    //ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = true;
            return View();
        }

        //Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin userModel, string ReturnUrl = "")
        {
            using (Database1Entities1 db = new Database1Entities1())
            {
                var pass = Crypto.Hash(userModel.Password);
                var userDetails = db.Users.Where(x => x.EmailID == userModel.EmailID && x.Password ==pass).FirstOrDefault();
                if (userDetails != null)
                {
                    int timeout = userModel.RememberMe ? 525600 : 20;
                    var ticket = new FormsAuthenticationTicket(userModel.EmailID, userModel.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    Session["userID"] = userDetails.EmailID;
                    Session["ID"] = userDetails.UserId;
                    Session["userName"] = userDetails.EmailID;
                    return RedirectToAction("Dashboard");
                 
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username or password.");
                  
                }
            }
            return View();/*

            //string message = "";
            using (Database1Entities1 dc = new Database1Entities1())
            {
                var v = dc.Users.Where(a => a.EmailID == userModel.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        ModelState.AddModelError("", "Please verify your email first");
                      //  ViewBag.Message = "";
                        return View();
                    }

                    if (string.Compare(Crypto.Hash(userModel.Password), v.Password) == 0)
                    {
                        int timeout = userModel.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(userModel.EmailID, userModel.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Dashboard");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong username or password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username or password.");
                }
            }
            //ViewBag.Message = message;
            return View();
            */
        }
       //Logout
       [Authorize]
 
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = 0;
            return RedirectToAction("Login","User");
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using(Database1Entities1 dc =new Database1Entities1())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID,string activationCode )
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("gabrielmusodza@gmail.com", " Wedding Hub Asp.Net  ");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "gabelahh"; //Replace withactual password
            string subject = "Your Account has been created successfully";

            string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                  " successfully created. Please click on the below link to verify your account" +
                  " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)


            };



            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
                smtp.Send(message);
        }
        public ActionResult Dashboard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            /*  Database1Entities1 db = new Database1Entities1();
              int userid = Convert.ToInt32(Session["ID"]);
              if (userid != null)
              {
                  return View(db.Users.Find(userid));
              }

                  return RedirectToAction("Login");
             */
        }
        [HttpPost]
        public ActionResult Profile(Vendor vendor)
        {
            using (Database1Entities1 dc = new Database1Entities1())
            {
                dc.Vendors.Add(vendor);
                dc.SaveChanges();
            }
                return View();
        }

    }
}