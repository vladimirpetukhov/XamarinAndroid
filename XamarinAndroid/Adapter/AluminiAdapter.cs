using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using XamarinAndroid.Models;
using Android.Graphics;

namespace XamarinAndroid.Adapter
{
    class AluminiAdapter : RecyclerView.Adapter
    {
        public event EventHandler<AluminiAdapterClickEventArgs> ItemClick;
        public event EventHandler<AluminiAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<AluminiAdapterClickEventArgs> DeleteItemClick;
        List<Alumini> Items;

        public AluminiAdapter(List<Alumini> data)
        {
            Items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.aluminirow, parent, false);

            var vh = new AluminiAdapterViewHolder(itemView, OnClick, OnLongClick, OnDeleteClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as AluminiAdapterViewHolder;
            holder.nameText.Text = Items[position].FullName;
            holder.statusText.Text = Items[position].Status;
            holder.setText.Text ="SET "+ Items[position].Set;
            holder.departmentText.Text = Items[position].Department;
            switch (Items[position].Status)
            {
                case "Graduated":
                    holder.statusText.SetTextColor(Color.Rgb(9, 155, 11));
                    break;
                case "Failed":
                    holder.statusText.SetTextColor(Color.Red);
                    break;
                case "Dropped Out":
                    holder.statusText.SetTextColor(Color.Maroon);
                    break;
                default:
                    holder.statusText.SetTextColor(Color.Rgb(238, 134, 31));
                    break;
            }


        }

        public override int ItemCount => Items.Count;

        void OnClick(AluminiAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(AluminiAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnDeleteClick(AluminiAdapterClickEventArgs args) => DeleteItemClick(this, args);

    }

    public class AluminiAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView nameText { get; set; }
        public TextView statusText { get; set; }
        public TextView setText { get; set; }
        public TextView departmentText { get; set; }
        public ImageView deleteButton { get; set; }


        public AluminiAdapterViewHolder(View itemView, Action<AluminiAdapterClickEventArgs> clickListener,
                            Action<AluminiAdapterClickEventArgs> longClickListener, Action<AluminiAdapterClickEventArgs> deleteClickListener) : base(itemView)
        {
            nameText = (TextView)itemView.FindViewById(Resource.Id.nameTxt);
            statusText = (TextView)itemView.FindViewById(Resource.Id.statusTxt);
            setText = (TextView)itemView.FindViewById(Resource.Id.setTxt);
            departmentText = (TextView)itemView.FindViewById(Resource.Id.departmentTxt);
            deleteButton = (ImageView)itemView.FindViewById(Resource.Id.deleteBtn);

            itemView.Click += (sender, e) => clickListener(new AluminiAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new AluminiAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            deleteButton.Click += (sender, e) => deleteClickListener(new AluminiAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class AluminiAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}