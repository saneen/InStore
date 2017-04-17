
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Preferences;
using System.Net.Http;
using Newtonsoft.Json;

namespace Instore
{
	[Activity(Label = "ProductActivity",Theme="@style/themenav")]
	public class ProductActivity : AppCompatActivity
	{
		NavigationView navigationView;
		DrawerLayout drawerLayout;
		Button getdirections;
		TextView productname, price, shopname, descriptionshop, productdescription, offer;
		ImageView image;
		string lattitude, longitude;
		Button book;
		string shopid;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
SetContentView(Resource.Layout.productbookingLayout);
		//	string shopname= Intent.GetStringExtra("shopname") ?? "Data not available";
			productname = FindViewById<TextView>(Resource.Id.b_productname);
			price = FindViewById<TextView>(Resource.Id.b_price);
			shopname = FindViewById<TextView>(Resource.Id.b_shopname);
			productdescription= FindViewById<TextView>(Resource.Id.b_productdescrip);
			descriptionshop = FindViewById<TextView>(Resource.Id.b_descrip);
			image = FindViewById<ImageView>(Resource.Id.prodimage);
			book = FindViewById<Button>(Resource.Id.b_booking);
			offer = FindViewById<TextView>(Resource.Id.b_offprice);
			productname.Text= Intent.GetStringExtra("productname") ?? "Data not available";
			price.Text="Price"+Intent.GetStringExtra("productprice") ?? "Data not available";
			shopname.Text="Shop   :"+Intent.GetStringExtra("shopname") ?? "Data not available";
			descriptionshop.Text="About Shop   :"+Intent.GetStringExtra("shopdesc") ?? "Data not available";
			productdescription.Text="Description   :"+Intent.GetStringExtra("productdescription") ?? "Data not available";
			offer.Text="Offer Price"+Intent.GetStringExtra("productoffer") ?? "No Offer Available For this product";
			 lattitude = Intent.GetStringExtra("lattitude") ?? "Data not available";
			longitude = Intent.GetStringExtra("longitude") ?? "Data not available";
			string images= Intent.GetStringExtra("productimage") ?? "Data not available";
			shopid=Intent.GetStringExtra("shopid") ?? "Data not available";
			Koush.UrlImageViewHelper.SetUrlDrawable(image, "http://slashcode.ml/instore/image/" + images);
			var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowTitleEnabled(false);
			SupportActionBar.SetHomeButtonEnabled(true);
			toolbar.Title = "Instore";
			SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
			getdirections = FindViewById<Button>(Resource.Id.p_getdirection);
			getdirections.Click += getdirection_Click;
			book.Click += book_Click;
		}
		private async void book_Click(object sender, EventArgs e)
		{
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
			var log = prefs.GetBoolean("LoggedIn", false);
			if (log)
			{
				var activity2 = new Intent(this, typeof(bookingActivity));
				activity2.PutExtra("shop", shopid);
				StartActivity(activity2);


		}
			else
			{
				Toast.MakeText(this, "You are not Signed In .Please SignIn and comeback", ToastLength.Long).Show();
				StartActivity(typeof(choosesigninactivity));
			}
		}
		private void getdirection_Click(object sender, EventArgs e)
		{

			var geoUri = Android.Net.Uri.Parse("geo:"+lattitude+","+longitude);
			                                   var mapintent = new Intent(Intent.ActionView, geoUri);
			StartActivity(mapintent);

		}
        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			var menuitem = e.MenuItem;
			switch (menuitem.ItemId)
			{
				case Resource.Id.nav_home:
					StartActivity(typeof(MainActivity));
					break;
				case Resource.Id.nav_sign:
					StartActivity(typeof(choosesigninactivity));
					break;
				case Resource.Id.nav_about:
					StartActivity(typeof(abourActivity));
					break;
				case Resource.Id.nav_FeedBack:
					StartActivity(typeof(feedbackActivity));
					break;
				case Resource.Id.nav_profile:
					StartActivity(typeof(profileActivity));
					break;
				case Resource.Id.nav_category:
					StartActivity(typeof(newsActivity));
					break;
			}
		}
	}

}
