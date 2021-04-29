using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dari_Pi.Models
{
    public class Ad
    {
        public int idAd { get; set; }
        public float area { get; set; }
        public bool ascenseur { get; set; }
        public bool garden { get; set; }
        public bool heater { get; set; }
        public string stat  { get; set; }
        public bool terasse { get; set; }
        public string type { get; set; }
        public DateTime dateAchat  { get; set; }
        public DateTime dateAjout { get; set; }
        public string location { get; set; }
        public int nbBathroom { get; set; }
        public int nbGarage { get; set; }
        public int nbRoom { get; set; }
        public float price { get; set; }
        public String image { get; set; }
        public bool swimming_Pool { get; set; }
        public int? user_id_user { get; set; }
        public List<Comment> Com { get; set; }


    }
}