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
using Android.Gms.Location.Places.UI;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Support.V7.Widget;

namespace Instore
{
    [Activity(Label = "Instore", MainLauncher = true, Icon = "@drawable/icon_shopping", Theme = "@style/themenav")]
    public class MainActivity : AppCompatActivity
    {
        private static readonly int PLACE_PICKER_REQUEST = 1;
        NavigationView navigationView;
        DrawerLayout drawerLayout;
        private Button pickplace,toolbarpickplace;
        ImageView pickloc;
       
        RecyclerView mRecyclerView;

        // Layout manager that lays out each card in the RecyclerView:
        RecyclerView.LayoutManager mLayoutManager, list;

        // Adapter that accesses the data set (a photo album):
        Adapter mAdapter;
		Spinner spun;
		string category;

		string lat, lng;
		protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            toolbar.Title = "Instore";
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            pickplace = FindViewById<Button>(Resource.Id.main_pickAPlaceButton);
            pickloc = FindViewById<ImageView>(Resource.Id.pickpic);
            toolbarpickplace = FindViewById<Button>(Resource.Id.toolbar_pickaplacebutton);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            pickplace.Click += OnPickAPlaceButtonTapped;
            toolbarpickplace.Click += OnPickAPlaceButtonTapped;
            spun = FindViewById<Spinner>(Resource.Id.spin);
			spun.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_itemselected);
			var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.catagory_array, Android.Resource.Layout.SimpleSpinnerItem);
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spun.Adapter = adapter;
            Button cngview = FindViewById<Button>(Resource.Id.changeview);
            cngview.Click += Cngview_Click;
		
        }

		private async void spinner_itemselected(object senders, AdapterView.ItemSelectedEventArgs s)
		{
			Spinner spinner = (Spinner)senders;
			category = string.Format("{0}", spinner.GetItemAtPosition(s.Position));
			ProgressDialog prog = new ProgressDialog(this);
			prog.SetTitle("Please wait......!!!");
			prog.Show();
			HttpClient client = new HttpClient();
			var url = "http://www.slashcode.ml/instoreapp/maps.php";
			MultipartFormDataContent parameter = new MultipartFormDataContent();

			parameter.Add(new StringContent(lat), "lattitude");
			parameter.Add(new StringContent(lng), "longitude");
			parameter.Add(new StringContent(category), "category");
			var resp = await client.PostAsync(url, parameter);


			Button cngview = FindViewById<Button>(Resource.Id.changeview);
			if (resp.IsSuccessStatusCode)
			{
				var cont = await resp.Content.ReadAsStringAsync();
				if (cont == "{\"status\":\"1B200\",\"data\":true}")
				{

					Toast.MakeText(this, "No shops in selected location ...Try another !!!", ToastLength.Long).Show();
					prog.Dismiss();
				}
				else
				{
					var datas = JsonConvert.DeserializeObject<RootObject>(cont);
					mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
					list = mLayoutManager = new LinearLayoutManager(this);
					cngview.Visibility = ViewStates.Visible;
					spun.Visibility = ViewStates.Visible;
                    toolbarpickplace.Visibility = ViewStates.Visible;
					mRecyclerView.SetLayoutManager(mLayoutManager);
					mAdapter = new Adapter(datas.data);
					mRecyclerView.SetAdapter(mAdapter);
					mAdapter.ItemClick += delegate (object sender, AdapterClickEventArgs e)
					 {
						 var activity2 = new Intent(this, typeof(ProductActivity));
						 activity2.PutExtra("shopid", datas.data[e.Position].shopId);
						 activity2.PutExtra("shopname", datas.data[e.Position].shopName);
						 activity2.PutExtra("shopdesc", datas.data[e.Position].shopDesc);
						 activity2.PutExtra("productid", datas.data[e.Position].productId);
						 activity2.PutExtra("productname", datas.data[e.Position].productName);
						 activity2.PutExtra("productcategory", datas.data[e.Position].productCategory);
						 activity2.PutExtra("productdescription", datas.data[e.Position].productDescription);
						 activity2.PutExtra("productprice", datas.data[e.Position].productPrice);
						 activity2.PutExtra("productoffer", datas.data[e.Position].productOffer);
						activity2.PutExtra("longitude", lng);
						 activity2.PutExtra("lattitude", lat.ToString());
						 activity2.PutExtra("productimage", datas.data[e.Position].productImage);
						 StartActivity(activity2);
					 };

					prog.Dismiss();
				}
			}

			else
			{
				var cont = resp.Content.ToString();
				prog.Dismiss();
			}
		}

		private void Cngview_Click(object sender, EventArgs e)
        {
            if (mLayoutManager == list)
                mLayoutManager = new GridLayoutManager
                    (this, 2, LinearLayoutManager.Vertical, false);
            else
                mLayoutManager = list;

            mRecyclerView.SetLayoutManager(mLayoutManager);
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
                /*case Resource.Id.nav_profile:
                    StartActivity(typeof(profileActivity));
                    break;*/
				case Resource.Id.nav_category:
					StartActivity(typeof(newsActivity));
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

        private void OnPickAPlaceButtonTapped(object sender, EventArgs eventArgs)
        {
            pickloc.Visibility = ViewStates.Gone;
            var builder = new PlacePicker.IntentBuilder();
            StartActivityForResult(builder.Build(this), PLACE_PICKER_REQUEST);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == PLACE_PICKER_REQUEST && resultCode == Result.Ok)
            {
                GetPlaceFromPicker(data);
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        private async void GetPlaceFromPicker(Intent data)
        {
            var placePicked = PlacePicker.GetPlace(this, data);
            var latitude = placePicked.LatLng.Latitude;
            var longitude = placePicked.LatLng.Longitude;
            ProgressDialog prog = new ProgressDialog(this);
            prog.SetTitle("Please wait......!!!");
            prog.Show();
            HttpClient client = new HttpClient();
            var url = "http://www.slashcode.ml/instoreapp/maps.php";
            MultipartFormDataContent parameter = new MultipartFormDataContent();
             lat = latitude.ToString();
             lng = longitude.ToString();

			parameter.Add(new StringContent(lat), "lattitude");
            parameter.Add(new StringContent(lng), "longitude");
	
				var resp = await client.PostAsync(url, parameter);


            Button cngview = FindViewById<Button>(Resource.Id.changeview);
            cngview.Visibility = ViewStates.Gone;
			spun.Visibility = ViewStates.Gone;
			if (resp.IsSuccessStatusCode)
			{
				var cont = await resp.Content.ReadAsStringAsync();
				if (cont == "{\"status\":\"1B200\",\"data\":true}")
				{

					Toast.MakeText(this, "No shops in selected location ...Try another !!!", ToastLength.Long).Show();
					prog.Dismiss();
				}
				else
				{
					var datas = JsonConvert.DeserializeObject<RootObject>(cont);
					mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
					list = mLayoutManager = new LinearLayoutManager(this);
					cngview.Visibility = ViewStates.Visible;
					spun.Visibility = ViewStates.Visible;
                    pickplace.Visibility = ViewStates.Gone;
                    toolbarpickplace.Visibility = ViewStates.Visible;
					mRecyclerView.SetLayoutManager(mLayoutManager);
					mAdapter = new Adapter(datas.data);
					mRecyclerView.SetAdapter(mAdapter);
		           mAdapter.ItemClick += delegate (object sender, AdapterClickEventArgs e)
					{
						var activity2 = new Intent(this, typeof(ProductActivity));
						activity2.PutExtra("shopid", datas.data[e.Position].shopId);
						activity2.PutExtra("shopname",datas.data[e.Position].shopName);
						activity2.PutExtra("shopdesc",datas.data[e.Position].shopDesc);
						activity2.PutExtra("productid", datas.data[e.Position].productId);
						activity2.PutExtra("productname", datas.data[e.Position].productName);
						activity2.PutExtra("productcategory", datas.data[e.Position].productCategory);
						activity2.PutExtra("productdescription", datas.data[e.Position].productDescription);
						activity2.PutExtra("productprice", datas.data[e.Position].productPrice);
						activity2.PutExtra("productoffer", datas.data[e.Position].productOffer);
						activity2.PutExtra("longitude", longitude.ToString());
						activity2.PutExtra("lattitude", latitude.ToString());
						activity2.PutExtra("productimage", datas.data[e.Position].productImage);
						StartActivity(activity2);
				   };

					prog.Dismiss();
				}
			}

			else
			{
				var cont = resp.Content.ToString();
				prog.Dismiss();
			}
        
		}

    }
}