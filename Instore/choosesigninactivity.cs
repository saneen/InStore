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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;

namespace Instore
{
    [Activity(Label = "Instore",Theme ="@style/themenav")]
    public class choosesigninactivity : AppCompatActivity
    {
        private Button signin, signup;
		NavigationView navigationView;
		DrawerLayout drawerLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.choosesignin);
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
            signin = FindViewById<Button>(Resource.Id.sigincho);
            signup = FindViewById<Button>(Resource.Id.signupcho);
            signin.Click += Signin_Click;
            signup.Click += Signup_Click;
        }

        private void Signup_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(signupactivity));
        }

        private void Signin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(loginActivity));   
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