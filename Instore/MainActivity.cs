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
    [Activity(Label = "Instore", MainLauncher = true, Icon = "@drawable/icon_shopping",Theme ="@style/themenav")]
    public class MainActivity :AppCompatActivity
    {
		private static readonly int PLACE_PICKER_REQUEST=1;
        NavigationView navigationView;
        DrawerLayout drawerLayout;
		private Button pickplace;

        // RecyclerView instance that displays the photo album:
        RecyclerView mRecyclerView;

        // Layout manager that lays out each card in the RecyclerView:
        RecyclerView.LayoutManager mLayoutManager, list;

        // Adapter that accesses the data set (a photo album):
        PhotoAlbumAdapter mAdapter;

        // Photo album that is managed by the adapter:
        PhotoAlbum mPhotoAlbum;


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
			navigationView.NavigationItemSelected+=NavigationView_NavigationItemSelected;
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
                        Photo[] mdatabasePhotos= { };
                        mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

                        while (datas.data[i].image!=null)
						{
							var image = datas.data[i].image;
							if (image!=null)
							{
								//saneen
								//this image down below is source and title is caption for image make it to the recycler view that the job
								image = "http://www.http://slashcode.ml/instore/image/" + image;
								var title = datas.data[i].caption;
                                //		Toast.MakeText(this, image, ToastLength.Short).Show();
                                Photo[] t = 
                                    {
                                    new Photo { mPhotoID = Resource.Drawable.app_splashcreen,
                                    mCaption = title },
                                    };
                                    var z = new Photo[mdatabasePhotos.Length + t.Length];
                                    mdatabasePhotos.CopyTo(z, 0);
                                    t.CopyTo(z, i);
                                    mdatabasePhotos = z;

                                // Instantiate the photo album:
                                mPhotoAlbum = new PhotoAlbum(mdatabasePhotos);


                                //............................................................
                                // Layout Manager Setup:

                                mLayoutManager = new LinearLayoutManager(this);
                                list = mLayoutManager;

                                // Plug the layout manager into the RecyclerView:
                                mRecyclerView.SetLayoutManager(mLayoutManager);

                                //............................................................
                                // Adapter Setup:

                                // Create an adapter for the RecyclerView, and pass it the
                                // data set (the photo album) to manage:
                                mAdapter = new PhotoAlbumAdapter(mPhotoAlbum);

                                // Register the item click handler (below) with the adapter:
                                mAdapter.ItemClick += OnItemClick;

                                // Plug the adapter into the RecyclerView:
                                mRecyclerView.SetAdapter(mAdapter);

                                //............................................................


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
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);

            // Detect user clicks on the item view and report which item
            // was clicked (by position) to the listener:
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }


    //----------------------------------------------------------------------
    // ADAPTER
    public class PhotoAlbumAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        // Underlying data set (a photo album):
        public PhotoAlbum mPhotoAlbum;

        // Load the adapter with the data set (photo album) at construction time:
        public PhotoAlbumAdapter(PhotoAlbum photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.Photocardview, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder vh = holder as PhotoViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the photo album:
            vh.Image.SetImageResource(mPhotoAlbum[position].PhotoID);
            vh.Caption.Text = mPhotoAlbum[position].Caption;
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return mPhotoAlbum.NumPhotos; }
        }

        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

    // Photo: contains image resource ID and caption:
    public class Photo
    {
        // Photo ID for this photo:
        public int mPhotoID;

        // Caption text for this photo:
        public string mCaption;

        // Return the ID of the photo:
        public int PhotoID
        {
            get { return mPhotoID; }
        }

        // Return the Caption of the photo:
        public string Caption
        {
            get { return mCaption; }
        }
    }

    // Photo album: holds image resource IDs and caption:
    public class PhotoAlbum
    {

        // Array of photos that make up the album:
        private Photo[] mPhotos;

        // Create an instance copy of the built-in photo list
        public PhotoAlbum(Photo[] databasephotos)
        {
            mPhotos = databasephotos;
        }

        // Return the number of photos in the photo album:
        public int NumPhotos
        {
            get { return mPhotos.Length; }
        }

        // Indexer (read only) for accessing a photo:
        public Photo this[int i]
        {
            get { return mPhotos[i]; }
        }
    } 


}