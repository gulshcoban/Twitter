using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProjesi.ServiceReference1;

namespace MvcProjesi.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tweet(string Tweet, DateTime Time)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ServiceReference1.Tweet tweet = new ServiceReference1.Tweet();
            tweet.Content = Tweet;
            tweet.TweetDate = Time;
            tweet.MemberID = Convert.ToInt32(Session["memberID"]);
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            a.TweetInsert(tweet);

            return View();
        }

        public ActionResult AllTweets()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            MvcProjesi.ServiceReference1.Tweet[] tweets = a.GetTweet(Convert.ToInt32(Session["memberID"]));
            return View(tweets);
        }

        public ActionResult NumberOfFav(int id)
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            List<int> tweets = new List<int>();
            tweets.Add(a.NumberOfFav(id));
            return View(tweets);
        }

        public ActionResult WhoseIsProfile()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            ServiceReference1.Member member = new ServiceReference1.Member();
            member = a.GetMember(Convert.ToInt32(Session["memberID"]));
            Session["memberName"] = member.UserName;
            List<ServiceReference1.Member> members = new List<ServiceReference1.Member>();
            members.Add(member);
            return View(members);
        }

        public ActionResult FavOfTweets(int id)
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            a.FavInsert(id, Convert.ToInt32(Session["memberID"]));
            return RedirectToAction("Index");
        }

        public ActionResult TweetDelete(int id)
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            a.TweetDelete(id);
            return RedirectToAction("Profile");
        }

        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult MemberTweets()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            MvcProjesi.ServiceReference1.Tweet[] tweets = a.GetMemberTweet(Convert.ToInt32(Session["memberID"]));
            return View(tweets);
        }

        public ActionResult WhoAreInStaniel()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            MvcProjesi.ServiceReference1.Member[] members = a.GetOtherMember(Convert.ToInt32(Session["memberID"]));
            return View(members);
        }
        public ActionResult FollowOfMember(int id)
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            a.FollowedInsert(Convert.ToInt32(Session["memberID"]), id);
            a.FollowerInsert(id, Convert.ToInt32(Session["memberID"]));
            return RedirectToAction("Index");
        }

        public ActionResult FollowedMember()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            MvcProjesi.ServiceReference1.Member[] members = a.GetFollowed(Convert.ToInt32(Session["memberID"]));
            return View(members);
        }
        public ActionResult FollowedOfMember(int id)
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            a.DeleteFollowed(Convert.ToInt32(Session["memberID"]), id);
            a.DeleteFollower(id, Convert.ToInt32(Session["memberID"]));
            return RedirectToAction("Index");
        }
        public ActionResult FollowerMember()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            MvcProjesi.ServiceReference1.Member[] members = a.GetFollower(Convert.ToInt32(Session["memberID"]));
            return View(members);
        }
        public ActionResult NumberOfTweet()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            List<int> members = new List<int>();
            members.Add(a.NumberOfTweet(Convert.ToInt32(Session["memberID"])));
            return View(members);
        }
        public ActionResult NumberOfFollowed()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            List<int> members = new List<int>();
            members.Add(a.NumberOfFollowed(Convert.ToInt32(Session["memberID"])));
            return View(members);
        }
        public ActionResult NumberOfFollower()
        {
            ServiceReference1.Service1Client a = new ServiceReference1.Service1Client();
            List<int> members = new List<int>();
            members.Add(a.NumberOfFollowers(Convert.ToInt32(Session["memberID"])));
            return View(members);
        }
    }
}