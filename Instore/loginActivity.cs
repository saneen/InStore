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
    [Activity(Label = "logibActivity",Theme ="@style/theme")]
    public class loginActivity : AppCompatActivity
    {
        private EditText username, password;
        Button signin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            username = FindViewById<EditText>(Resource.Id.signusername);
            password = FindViewById<EditText>(Resource.Id.signpass);
            signin = FindViewById<Button>(Resource.Id.login);
            signin.Click += Signin_Click;
        }

        private void Signin_Click(object sender, EventArgs e)
        {
            if(username.Text==""|password.Text=="")
            {
                Toast.MakeText(this, "Please Fill All The Fields", ToastLength.Long).Show();
            }
            else
            {

            }
        }
    }
}