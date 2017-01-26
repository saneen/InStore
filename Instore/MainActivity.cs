using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace Instore
{
    [Activity(Label = "Instore", MainLauncher = true, Icon = "@drawable/icon_shopping",Theme ="@style/themenav")]
    public class MainActivity :AppCompatActivity
    {
        NavigationView navigationView;
        DrawerLayout drawerLayout;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;        
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var menuitem = e.MenuItem;
            switch(menuitem.ItemId)
            {
                case Resource.Id.nav_home:
                    StartActivity(typeof(MainActivity));
                    break;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
               case Android.Resource.Id.Home:
                   drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        }
}

