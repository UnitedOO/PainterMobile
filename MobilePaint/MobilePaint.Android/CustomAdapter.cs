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

namespace MobilePaint.Droid
{
    public class CustomAdapter : BaseAdapter<KeyValuePair<string, string[]>>
    {

        Dictionary<string, string[]> items;
        Activity context;
        public CustomAdapter(Activity context, Dictionary<string,string[]> items) : base() {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }

       
        public override KeyValuePair<string, string[]> this[int position]
        {
            get { return items.ElementAt(position); }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.PopUp, null);
            view.FindViewById<Button>(Resource.Id.pop_btn).Text = this[position].Key;
             view.FindViewById<Button>(Resource.Id.pop_btn).Click += (s, arg) =>
              {
                  PopupMenu menu = new PopupMenu(context, view.FindViewById<Button>(Resource.Id.pop_btn));
                  //menu.Inflate(Resource.Menu.popup_menu);
                  foreach(string name in this[position].Value)
                  {
                      menu.Menu.Add(name);
                  }
                  menu.MenuItemClick += (s1, arg1) => {
                      Toast.MakeText(context, arg1.Item.ToString(), ToastLength.Long).Show();
                  };

                  menu.Show();
              };
            return view;
        }
    }
}