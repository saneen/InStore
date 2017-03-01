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

namespace Instore
{
    public class ResponseModel
    {
        public string status { get; set; }
        public bool data { get; set; }
    }
}