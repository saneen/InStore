using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Instore
{
    public class ResponseModel
    {
        public string status { get; set; }
        public bool data { get; set; }
    }

public class Datum
	{
		public string shopId { get; set; }
		public string shopName { get; set; }
		public string shopDesc { get; set; }
		public string productId { get; set; }
		public string productImage { get; set; }
		public string productName { get; set; }
		public string productCategory { get; set; }
		public string productDescription { get; set; }
		public string productPrice { get; set; }
		public string productOffer { get; set; }
	}

	public class RootObject
	{
		public string status { get; set; }
		public List<Datum> data { get; set; }
	}

}