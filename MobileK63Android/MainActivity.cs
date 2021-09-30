using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Widget;
using Android.Content;

namespace MobileK63Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : Activity
    {
        Button btSendSMS;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
            var btTinh = (Button)FindViewById(Resource.Id.btTinhTong);
            btTinh.Click += BtTinh_Click;

            var btSendSMS = (Button)FindViewById(Resource.Id.btSendSMS);
            btSendSMS.Click += BtSendSMS_Click;
        }

        private void BtSendSMS_Click(object sender, EventArgs e)
        {
            Intent smsIntent = new Intent(this, typeof(SmsActivity));
            Intent[] ds = new Intent[1];
            ds[0] = smsIntent;
            StartActivities(ds);
        }

        private void BtTinh_Click(object sender, EventArgs e)
        {
            var etNum1 = (EditText)FindViewById(Resource.Id.etNum1);
            var etNum2 = (EditText)FindViewById(Resource.Id.etNum2);
            var tvketQua = (TextView)FindViewById(Resource.Id.tvKetQua);
            float num1 = float.Parse(etNum1.Text);
            float num2 = float.Parse(etNum2.Text);
            tvketQua.Text = (num1 + num2).ToString();
        }        
	}
}
