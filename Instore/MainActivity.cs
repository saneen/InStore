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
        private Button pickplace;
       
        // RecyclerView instance that displays the photo album:
        RecyclerView mRecyclerView;

        // Layout manager that lays out each card in the RecyclerView:
        RecyclerView.LayoutManager mLayoutManager, list;

        // Adapter that accesses the data set (a photo album):
        Adapter mAdapter;

        // Photo album that is managed by the adapter:

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
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            pickplace.Click += OnPickAPlaceButtonTapped;


            Button cngview = FindViewById<Button>(Resource.Id.changeview);

            cngview.Click += Cngview_Click;

        }

        //CHANGE VIEW BUTTON 

        private void Cngview_Click(object sender, EventArgs e)
        {
            if (mLayoutManager == list)
                mLayoutManager = new GridLayoutManager
                    (this, 2, LinearLayoutManager.Vertical, false);
            else
                mLayoutManager = list;

            mRecyclerView.SetLayoutManager(mLayoutManager);
        }

        // Handler for the item click event:
        void OnItemClick(object sender, int position)
        {
            // Display a toast that briefly shows the enumeration of the selected photo:
            int photoNum = position + 1;
            Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();
        }

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
			string lat = latitude.ToString();
			string lng = longitude.ToString();
			parameter.Add(new StringContent(lat), "lattitude");
			parameter.Add(new StringContent(lng), "longitude");
			var resp = await client.PostAsync(url, parameter);
			if (resp.IsSuccessStatusCode)
			{
				var cont = await resp.Content.ReadAsStringAsync();
				var datar = JsonConvert.DeserializeObject<RootObject>(cont);
				var shpId = datar.data[0].shopId;
				Toast.MakeText(this,shpId, ToastLength.Short).Show();
			
				if (shpId==null)
				{
					Toast.MakeText(this, "No shops in selected location ...Try another !!!", ToastLength.Long).Show();
					prog.Dismiss();
				}
				else
				{
					HttpClient clients = new HttpClient();
					var urls = "http://www.slashcode.ml/instoreapp/prodlist.php";
					MultipartFormDataContent parameters = new MultipartFormDataContent();
					parameters.Add(new StringContent("13"), "shop");
					var resps = await clients.PostAsync(urls, parameters);
					if (resps.IsSuccessStatusCode)
					{
						var conts = await resps.Content.ReadAsStringAsync();
						var datas = JsonConvert.DeserializeObject<RootObjectproduct>(conts);
						int i = 0;
						while (datas.data[i].image!=null)
						{
							var image = datas.data[i].image;
							if (image!=null)
							{
								//saneen
								//this image down below is source and title is caption for image make it to the recycler view that the job
								image = "http://www.http://slashcode.ml/instore/image/" + image;
								var title = datas.data[i].caption;
						//	Toast.MakeText(this, image, ToastLength.Short).Show();
								prog.Dismiss();
								i++;
							}	
													
							else
						{
							Toast.MakeText(this, "No products available...", ToastLength.Long).Show();
								prog.Dismiss();
							}
								
						}

					}

					else
					{
						var conts = resps.Content.ToString();
					}
							
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

    //----------------------------------------------------------------------
    // VIEW HOLDER
    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }

        // Get references to the views defined in the CardView layout.
        public PhotoViewHolder(View itemView, Action<int> listener)
            : base(itemView)
=======
        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
>>>>>>> refs/remotes/origin/mridul
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
                case Resource.Id.nav_categorys:
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
            Toast.MakeText(this, latitude.ToString(), ToastLength.Short).Show();

            ProgressDialog prog = new ProgressDialog(this);
            prog.SetTitle("Please wait......!!!");
            prog.Show();
            HttpClient client = new HttpClient();
            var url = "http://www.slashcode.ml/instoreapp/maps.php";
            MultipartFormDataContent parameter = new MultipartFormDataContent();
            string lat = latitude.ToString();
            string lng = longitude.ToString();
            parameter.Add(new StringContent(lat), "lattitude");
            parameter.Add(new StringContent(lng), "longitude");
            var resp = await client.PostAsync(url, parameter);

            Button cngview = FindViewById<Button>(Resource.Id.changeview);
            cngview.Visibility = ViewStates.Gone;
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
                    var datar = JsonConvert.DeserializeObject<RootObject>(cont);
                    var shpId = datar.data[0].shopId;
                    Toast.MakeText(this, shpId, ToastLength.Short).Show();

                    if (shpId == null)
                    {
                        Toast.MakeText(this, "No shops in selected location ...Try another !!!", ToastLength.Long).Show();
                        prog.Dismiss();
                    }
                    else
                    {
                        HttpClient clients = new HttpClient();
                        var urls = "http://www.slashcode.ml/instoreapp/prodlist.php";
                        MultipartFormDataContent parameters = new MultipartFormDataContent();
                        parameters.Add(new StringContent(shpId), "shop");
                        var resps = await clients.PostAsync(urls, parameters);
                        if (resps.IsSuccessStatusCode)
                        {
                            var conts = await resps.Content.ReadAsStringAsync();
                            var datas = JsonConvert.DeserializeObject<RootObjectproduct>(conts);
                            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
                            list = mLayoutManager = new LinearLayoutManager(this);
                            cngview.Visibility = ViewStates.Visible;
                            mRecyclerView.SetLayoutManager(mLayoutManager);
                            mAdapter = new Adapter(datas.data);
                            mRecyclerView.SetAdapter(mAdapter);
                            prog.Dismiss();
                        }
                        else
                        {
                            Toast.MakeText(this, "No products available...", ToastLength.Long).Show();
                            prog.Dismiss();
                        }
                    }
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