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
using System.Net.Http;
using Newtonsoft.Json;

namespace Instore
{
    [Activity(Label = "Instore",Theme ="@style/theme")]
    public class loginActivity : AppCompatActivity
    {
        private EditText username, password;
        Button signin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.login);
            username = FindViewById<EditText>(Resource.Id.signusername);
            password = FindViewById<EditText>(Resource.Id.signpass);
            signin = FindViewById<Button>(Resource.Id.login);
            signin.Click += Signin_Click;
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