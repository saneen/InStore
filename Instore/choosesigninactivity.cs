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
    [Activity(Label = "Instore",Theme ="@style/theme")]
    public class choosesigninactivity : AppCompatActivity
    {
        private Button signin, signup;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.choosesignin);

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
    }
}