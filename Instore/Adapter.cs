using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Graphics;

namespace Instore
{
    public class Adapter : RecyclerView.Adapter
    {
        public event EventHandler<AdapterClickEventArgs> ItemClick;
        public event EventHandler<AdapterClickEventArgs> ItemLongClick;
		List<Datum> products;

		public Adapter(List<Datum> products)
        {
            this.products = products;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.Photocardview;
            itemView = LayoutInflater.From(parent.Context).
                  Inflate(id, parent, false);

            var vh = new AdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = products[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as AdapterViewHolder;
			holder.Caption.Text = products[position].productName;
			Koush.UrlImageViewHelper.SetUrlDrawable(holder.Image, "http://slashcode.ml/instore/image/"+products[position].productImage);
        }
 
        public override int ItemCount => products.Count;

        void OnClick(AdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(AdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class AdapterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }

        public AdapterViewHolder(View itemView, Action<AdapterClickEventArgs> clickListener,
                            Action<AdapterClickEventArgs> longClickListener) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            itemView.Click += (sender, e) => clickListener(new AdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new AdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class AdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}