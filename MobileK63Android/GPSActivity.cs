using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileK63Android
{
    [Activity(Label = "GPSActivity", MainLauncher = false)]
    public class GPSActivity : Activity, ILocationListener
    {
        Button btGetGSP;
        TextView tvKinhDo, tvViDo, tvDoCao;
        Location viTri;
        LocationManager qlVitri;
        String nguonGPS;

        public void OnLocationChanged(Location location)
        {
            viTri = location;
            if (viTri == null)
                return;

            tvKinhDo.Text = "Kinh độ: " + viTri.Longitude.ToString();
            tvViDo.Text = "Vĩ độ: " + viTri.Latitude.ToString();
            tvDoCao.Text = "Độ cao: " + viTri.Altitude.ToString();
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_gps);

            btGetGSP = (Button)FindViewById(Resource.Id.btGetGPS);
            tvKinhDo = (TextView)FindViewById(Resource.Id.tvKinhDo);
            tvViDo = (TextView)FindViewById(Resource.Id.tvViDo);
            tvDoCao = (TextView)FindViewById(Resource.Id.tvDoCao);

            KhoiTaoGPS();
        }

        protected override void OnPause()
        {
            base.OnPause();
            qlVitri.RemoveUpdates(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            qlVitri.RequestLocationUpdates(nguonGPS, 0, 0, this);
        }

        private void KhoiTaoGPS()
        {
            qlVitri = (LocationManager)GetSystemService(LocationService);
            Criteria criteria = new Criteria()
            {
                Accuracy = Accuracy.Coarse
            };

            IList<String> dsViTri = qlVitri.GetProviders(criteria, true);
            if (dsViTri != null && dsViTri.Count > 0)
            {
                nguonGPS = dsViTri.First();
            }
            else
                nguonGPS = "";
        }
    }
}