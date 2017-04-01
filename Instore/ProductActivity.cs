
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

namespace Instore
{
	[Activity(Label = "ProductActivity",Theme="@style/themenav")]
	public class ProductActivity : AppCompatActivity
	{
		NavigationView navigationView;
		DrawerLayout drawerLayout;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.productbookingLayout);

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
