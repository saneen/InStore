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
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;

namespace Instore
{
	[Activity(Label = "Instore",Theme="@style/themenav")]
    public class signupactivity : AppCompatActivity
    {
        private EditText username, phone, email, password, confrm;
        private Button btn;
		NavigationView navigationView;
		DrawerLayout drawerLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signup);
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
            username = FindViewById<EditText>(Resource.Id.username_);
            phone = FindViewById<EditText>(Resource.Id.phone_);
            email = FindViewById<EditText>(Resource.Id.email_);
            password = FindViewById<EditText>(Resource.Id.password_);
            confrm = FindViewById<EditText>(Resource.Id.confrmpass_);
            btn = FindViewById<Button>(Resource.Id.signup_);
            btn.Click += Btn_Click;
            
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
        async private void Btn_Click(object sender, EventArgs e)
        {
            if (username.Text =="" | phone.Text == "" | email.Text == ""| password.Text == "" | confrm.Text == "")

            {
                Toast.MakeText(this, "Please Fill All Fields", ToastLength.Short).Show();
            }
            else
            {
                if(password.Text!=confrm.Text)
                {
                    Toast.MakeText(this, "Passwords doesnnot match", ToastLength.Long).Show();
                }
                else
                {
					ProgressDialog prog = new ProgressDialog(this);
					prog.SetTitle("Please wait......!!!");
					prog.Show();
                    HttpClient cient = new HttpClient();
                    var url = "http://www.slashcode.ml/instoreapp/reg.php";
           
                    MultipartFormDataContent parameters = new MultipartFormDataContent();
                    parameters.Add(new StringContent(username.Text),"username");
                    parameters.Add(new StringContent(password.Text), "password");
                    parameters.Add(new StringContent(phone.Text), "phone");
                    parameters.Add(new StringContent(email.Text), "email");
                    var resp = await cient.PostAsync(url, parameters);
                    if (resp.IsSuccessStatusCode)
                    {
                        var cont = await resp.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ResponseModel>(cont);
                        if (data.data)
                        {
                            Toast.MakeText(this, "Successfully signed in.....!", ToastLength.Long).Show();
                            StartActivity(typeof(loginActivity));
							prog.Dismiss();
                        }
                        else
                        {
                            Toast.MakeText(this, data.status, ToastLength.Long).Show();
                            Toast.MakeText(this, "Error Signing up.....", ToastLength.Long).Show();
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
}