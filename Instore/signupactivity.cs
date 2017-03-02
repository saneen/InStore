using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace Instore
{
	[Activity(Label = "Instore",Theme="@style/theme")]
    public class signupactivity : AppCompatActivity
    {
        private EditText username, phone, email, password, confrm;
        private Button btn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signup);
            username = FindViewById<EditText>(Resource.Id.username_);
            phone = FindViewById<EditText>(Resource.Id.phone_);
            email = FindViewById<EditText>(Resource.Id.email_);
            password = FindViewById<EditText>(Resource.Id.password_);
            confrm = FindViewById<EditText>(Resource.Id.confrmpass_);
            btn = FindViewById<Button>(Resource.Id.signup_);
            btn.Click += Btn_Click;
            
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
                        }
                        else
                        {
                            Toast.MakeText(this, data.status, ToastLength.Long).Show();
                            Toast.MakeText(this, "Error Signing up.....", ToastLength.Long).Show();
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