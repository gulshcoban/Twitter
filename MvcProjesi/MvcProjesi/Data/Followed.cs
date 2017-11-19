using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Followed
    {
        public int FollowedID { get; set; }
        public int MemberID { get; set; }
    }
}