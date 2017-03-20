using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Instore
{
	[Activity(Label = "profileActivity",Theme="@style/theme")]
    public class profileActivity : AppCompatActivity
    {
		private TextView username, phone, email;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profile);
			username = FindViewById<TextView>(Resource.Id.pname_);
			phone = FindViewById<TextView>(Resource.Id.pphone_);
			email = FindViewById<TextView>(Resource.Id.email_);
           
        }
    }
}