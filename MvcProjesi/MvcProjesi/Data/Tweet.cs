using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Tweet
    {
        public int TweetID { get; set; }
   
        [Required(ErrorMessage = "Lütfen tweetin içeriğini giriniz.")]
        //Girilen metnin, html formatında girilmesini sağlıyoruz.
        [DataType(DataType.Html, ErrorMessage = "Lütfen tweet içeriğini html formatında giriniz.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Lütfen tweetin eklenme tarihini giriniz.")]
        [DataType(DataType.DateTime, ErrorMessage = "Lütfen tweetin eklenme tarihini, doğru bir şekilde giriniz.")]
        public DateTime TweetDate { get; set; }

        public int MemberID  { get; set; }

        public List<Tweet> Tweets { get; set; }
        

    }
}