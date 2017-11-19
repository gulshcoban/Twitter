using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProjesi.ServiceReference1;

namespace MvcProjesi.Controllers
{
    public class NewMembershipController : Controller
    {
        public ActionResult NewMembership()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMembership(string name, string username, string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ServiceReference1.Member member = new ServiceReference1.Member();
            member.Name = name;
            member.UserName = username;
            member.Email = email;
            member.Password = password;
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            if (a.isThereAnAccount(member) == true)
            {
                a.MemberInsert(member);
                TempData["alert"] = "<script>alert('You are successfully subscribed to the system.'); setTimeout(function(){window.location='/NewMembership/Login'},1000);</script>";

            }
            else
                TempData["alert"] = "<script>alert('Such a record exists. Please try again.');</script>";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public string Login(string username, string password)
        {
            int result = 0;
            if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
            {
                return "You did not enter your e-mail address and your password.";
            }
            else if (String.IsNullOrEmpty(username))
            {
                return "You did not enter your e-mail address.";
            }
            else if (string.IsNullOrEmpty(password))
            {
                return "You did not enter your password.";
            }
            else
            {
                ServiceReference1.Member member = new ServiceReference1.Member();
                member.UserName = username;
                member.Password = password;
                ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
                result = a.isRight(member);
                if (result == 0)
                    return "You have entered your username or password incorrect.";
                else
                {
                    Session["memberID"] = a.GetMemberID(member);
                    return "Successfully logged in.<script type='text/javascript'>setTimeout(function(){window.location='/'},1000);</script>";
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateInfo(ServiceReference1.Member m)
        {
            ServiceReference1.Service1Client a = new Service1Client();
            ServiceReference1.Member member = new ServiceReference1.Member();
            member.MemberID = (Convert.ToInt32(Session["memberID"]));
            member.Name = m.Name;
            member.UserName = m.UserName;
            member.Email = m.Email;
            member.Password = m.Password;
            a.MemberUpdate(member);
            TempData["alert"] = "<script>alert('Your information has been successfully updated');</script>";
            return View();
        }

        public ActionResult UpdateInfo()
        {
            return View();
        }
    }
}
