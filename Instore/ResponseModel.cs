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
	}

	public class RootObject
	{
		public string status { get; set; }
		public List<Datum> data { get; set; }
	}

public class product
	{
		public string image { get; set; }
		public string caption { get; set; }
	}

	public class RootObjectproduct
	{
		public string status { get; set; }
		public List<product> data { get; set; }
	}
}