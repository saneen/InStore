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
using System.Net.Http;
using Newtonsoft.Json;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Preferences;

namespace Instore
{
    [Activity(Label = "Instore",Theme ="@style/themenav")]
    public class loginActivity : AppCompatActivity
    {
        private EditText username, password;
        Button signin;
		NavigationView navigationView;
		DrawerLayout drawerLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
			base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
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
            username = FindViewById<EditText>(Resource.Id.signusername);
            password = FindViewById<EditText>(Resource.Id.signpass);
            signin = FindViewById<Button>(Resource.Id.login);
            signin.Click += Signin_Click;
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
        private async void Signin_Click(object sender, EventArgs e)
        {
            if (username.Text == "" | password.Text == "")
            {
                Toast.MakeText(this, "Please Fill All The Fields", ToastLength.Long).Show();
            }
            else
            {
				ProgressDialog prog = new ProgressDialog(this);
				prog.SetTitle("Please wait......!!!");
				prog.Show();
                HttpClient client = new HttpClient();
                var url = "http://www.slashcode.ml/instoreapp/login.php";
                MultipartFormDataContent parameter = new MultipartFormDataContent();
                parameter.Add(new StringContent(username.Text), "username");
                parameter.Add(new StringContent(password.Text), "password");
                var resp = await client.PostAsync(url, parameter);
                if (resp.IsSuccessStatusCode)
                {
                    var cont = await resp.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ResponseModel>(cont);
                    if (data.data)
                    {
                        Toast.MakeText(this, "Login successfull",ToastLength.Short).Show();
						ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
						ISharedPreferencesEditor editor = prefs.Edit();
						editor.PutBoolean("LoggedIn",true);
						editor.Apply();
                StartActivity(typeof(MainActivity));
						prog.Dismiss();
					}
                    else
                    {
                        Toast.MakeText(this, "Login Failed ....! Invalid username or password", ToastLength.Long).Show();
						prog.Dismiss();
                    					}
                }

                else
                {
                    var cont = resp.Content.ToString();
                }
            }
        }
    }
}