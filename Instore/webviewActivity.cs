
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace Instore
{
	[Activity(Label = "Instore",Theme="@style/theme")]
	public class webviewActivity :AppCompatActivity
	{
		WebView webView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.webviewLayout);
			string	id = Intent.GetStringExtra("id") ?? "Data not available";
			webView = FindViewById<WebView>(Resource.Id.webview);
			webView.SetWebViewClient(new WebViewClient());
			webView.LoadUrl("http://www.niyamasabha.org/codes/cmin.htm");
			      

			switch (id)
			{
				case "food":
					webView.LoadUrl("http://www.foodbusinessnews.net/");
					break;
				case "men":
					webView.LoadUrl("http://www.esquire.com/style/");
					break;
					case "women":
					webView.LoadUrl("http://www.elle.com/fashion/");
					break;
					case "mobile":
					webView.LoadUrl("http://gadgets.ndtv.com/mobiles/news");
					break;
			}
			webView.Settings.BuiltInZoomControls = true;
			webView.Settings.SetSupportZoom(true);

			// scrollbar stuff            
			webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
			// so there's no 'white line'            
			webView.ScrollbarFadingEnabled = false;
		}
	}
}
