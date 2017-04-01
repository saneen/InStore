
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;

namespace Instore
{
	[Activity(Label = "Instore",Theme="@style/themenav")]
	public class newsActivity : AppCompatActivity
	{
		NavigationView navigationView;
		DrawerLayout drawerLayout;
		private Button food, mens, womens, mobiles;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.newsLayout);
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

			mens = FindViewById<Button>(Resource.Id.c_mensclothing);
			food = FindViewById<Button>(Resource.Id.c_food);
			womens = FindViewById<Button>(Resource.Id.c_womensclothing);
			mobiles = FindViewById<Button>(Resource.Id.c_mobiles);
			mens.Click += mens_Click;
			food.Click += food_Click;
			womens.Click += womens_Click;
			mobiles.Click += mobiles_Click;
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
		private void food_Click(object sender, EventArgs e)
		{
			var activity2 = new Intent(this, typeof(webviewActivity));
			activity2.PutExtra("id", "food");
			StartActivity(activity2);
		}
		private void mens_Click(object sender, EventArgs e)
		{
			var activity2 = new Intent(this, typeof(webviewActivity));
			activity2.PutExtra("id", "men");
			StartActivity(activity2);
		}
		private void womens_Click(object sender, EventArgs e)
		{
			var activity2 = new Intent(this, typeof(webviewActivity));
			activity2.PutExtra("id", "women");
			StartActivity(activity2);
		}
		private void mobiles_Click(object sender, EventArgs e)
		{
			var activity2 = new Intent(this, typeof(webviewActivity));
			activity2.PutExtra("id", "mobile");
			StartActivity(activity2);
		}

	}
}
