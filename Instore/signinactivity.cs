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

namespace Instore
{
	[Activity(Label = "Instore",Theme="@style/theme")]
    public class signinactivity : AppCompatActivity
    {
        private EditText username, phone, email, password, confrm;
        private Button btn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signin);
            username = FindViewById<EditText>(Resource.Id.username_);
            phone = FindViewById<EditText>(Resource.Id.phone_);
            email = FindViewById<EditText>(Resource.Id.email_);
            password = FindViewById<EditText>(Resource.Id.password_);
            confrm = FindViewById<EditText>(Resource.Id.confrmpass_);
            btn = FindViewById<Button>(Resource.Id.signup_);
            btn.Click += Btn_Click;
            
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            if (username.Text =="" | phone.Text == "" | email.Text == ""| password.Text == "" | confrm.Text == "")

            {
                Toast.MakeText(this, "Please Fill All Fields", ToastLength.Short).Show();
            }
            else
            {

            }
        }
    }
}