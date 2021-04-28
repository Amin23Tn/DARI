using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dari_PI.Models
{
    public class WishList
    {
		public int idWish { get; set; }
		public float areaMin { get; set; }
		public float areaMax { get; set; }
		public float budgetMin { get; set; }
		public float budgetMax { get; set; }
		public String lieu { get; set; }
		public int nbRoom { get; set; }
		public int nbGarage { get; set; }
		public Boolean elevator { get; set; }
	}
}