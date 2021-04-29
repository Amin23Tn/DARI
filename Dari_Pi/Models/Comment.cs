using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dari_Pi.Models
{
    public class Comment
    {
        public int idComment { get; set; }
        public string descriptionComment { get; set; }
        public bool isBlocked { get; set; }
        public int numberLikes { get; set; }
        public int ads_id_ad { get; set; }
       
        

    }
}