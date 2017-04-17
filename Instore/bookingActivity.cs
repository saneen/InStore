
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Instore
{
	[Activity(Label = "Instore", Theme = "@style/theme")]
	public class bookingActivity : AppCompatActivity
	{
		EditText name, phone, email, address;
		string shopid;
		Button book;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.bookingLayout);
			name = FindViewById<EditText>(Resource.Id.bname);
			phone = FindViewById<EditText>(Resource.Id.bphone);
			email = FindViewById<EditText>(Resource.Id.bmail);
			address = FindViewById<EditText>(Resource.Id.baddr);
			book = FindViewById<Button>(Resource.Id.booker);
			shopid = Intent.GetStringExtra("shop") ?? "Data not available";
			book.Click += book_Click;
		}
		private async void book_Click(object sender, EventArgs e)
		{
			if (name.Text == "" | phone.Text == "" | email.Text == "" | address.Text == "")
			{
				Toast.MakeText(this, "Please Fill All The Fields", ToastLength.Long).Show();
			}
			else
			{
				ProgressDialog prog = new ProgressDialog(this);
				prog.SetTitle("Please wait......!!!");
				prog.Show();
				HttpClient client = new HttpClient();
				var url = "http://www.slashcode.ml/instoreapp/booking.php";
				MultipartFormDataContent parameter = new MultipartFormDataContent();
				parameter.Add(new StringContent(shopid), "shopid");
				parameter.Add(new StringContent("saneen"), "name");
				parameter.Add(new StringContent("saneens"), "email");
				parameter.Add(new StringContent("789"), "phone");
				parameter.Add(new StringContent("pname"), "pname");
				parameter.Add(new StringContent("pimage"), "pimage");
				parameter.Add(new StringContent("address"), "address");
				var resp = await client.PostAsync(url, parameter);
				if (resp.IsSuccessStatusCode)
				{
					var cont = await resp.Content.ReadAsStringAsync();
					var data = JsonConvert.DeserializeObject<ResponseModel>(cont);
					if (data.data)
					{
						Toast.MakeText(this, "Successfully Booked ! :)", ToastLength.Short).Show();
						prog.Dismiss();
					}
					else
					{
						Toast.MakeText(this, "Booking Failed Check Your connection", ToastLength.Long).Show();
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

