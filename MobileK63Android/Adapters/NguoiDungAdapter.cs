using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileK63Android.Adapters
{
    public class NguoiDungAdapter : BaseAdapter<String>
    {
        List<String> Emails;
        List<String> Actives;
        Activity Context;
        public NguoiDungAdapter(Activity context, List<String> emails, List<String> actives)
        {
            Context = context;
            Emails = emails;
            Actives = actives;
        }

        public override string this[int position]
        {
            get { return Emails[position]; }
        }

        public override int Count
        {
            get { return Emails.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = Context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
            view.FindViewById<TextView>(Resource.Id.tvEmail).Text = Emails[position];
            view.FindViewById<TextView>(Resource.Id.tvActive).Text = Actives[position];
            return view;
        }
    }
}