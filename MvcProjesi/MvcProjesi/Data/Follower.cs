using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Follower
    {
        public int FollowerID { get; set; }
        public int MemberID { get; set; }
    }
}